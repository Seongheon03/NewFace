using TNetwork.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNetwork.Common;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TNetwork
{
    public partial class NetworkManager /*: IServerProtocol*/
    {
        
        public void SetHTTPRequestURL(string serverUrl, string loginUrl, string logoutUrl, string tokenRefreshUrl, int timeOut = 30000)
        {
            Options.serverUrl = serverUrl;
            Options.loginUrl = loginUrl;
            Options.logoutUrl = logoutUrl;
            Options.tokenRefreshUrl = tokenRefreshUrl;
            Options.tokenInfo = new TokenInfo();
            Options.timeOut = timeOut;
        }

        /// <summary>
        /// RestRequest를 생성합니다.
        /// </summary>
        /// <param name="resource">리소스 url</param>
        /// <param name="method">요청 메서드</param>
        /// <param name="parameterJson">기본 null. POST,PUT,DELETE때 Body</param>
        /// <param name="token">기본 null. 토큰</param>
        /// <param name="urlSegments">기본 null. url파라미터</param>
        /// <param name="headers">기본 null. 헤더</param>
        /// <returns>RestRequest</returns>
        private static RestRequest CreateRequest(string resource, Method method, string parameterJson, TokenInfo token = null, UrlSegment[] urlSegments = null, Header[] headers = null)
        {
            var restRequest = new RestRequest(resource, method) { Timeout = Options.timeOut };
            restRequest = AddToRequest(restRequest, token, parameterJson, urlSegments, headers);

            return restRequest;
        }

        /// <summary>
        /// token, json,urlsegments, headers를 restrequest에 손쉽게 넣어줍니다.
        /// </summary>
        /// <param name="restRequest">목표 RestRequest객체</param>
        /// <param name="token">토큰</param>
        /// <param name="json">JSON</param>
        /// <param name="urlSegments">UrlSegement</param>
        /// <param name="headers">Header</param>
        /// <returns></returns>
        private static RestRequest AddToRequest(RestRequest restRequest, TokenInfo token, string json = null, UrlSegment[] urlSegments = null, Header[] headers = null)
        {
            if (urlSegments != null)
            {
                foreach (var urlSegment in urlSegments)
                {
                    restRequest.AddUrlSegment(urlSegment.Name, urlSegment.Value);
                }
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    restRequest.AddHeader(header.Name, header.Value);
                }
            }

            if (!string.IsNullOrEmpty(json))
            {
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            }

            if (token != null)
            {
                restRequest.AddHeader("x-access-token", token.Token);
            }
            return restRequest;
        }

        /// <summary>
        /// snake_case로 이뤄진 Json을 PascalCase로 역직렬화.
        /// </summary>
        /// <typeparam name="T">반환 형태</typeparam>
        /// <param name="json">목표 Json</param>
        /// <returns>역직렬화된 Json</returns>
        private static T DeserializeSnakeCase<T>(string json)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            try
            {
                var resp = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented,
                });
                return resp;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return default(T);
        }

        /// <summary>
        /// RestClient를 만듭니다.
        /// </summary>
        /// <returns>RestClient</returns>
        private static RestClient CreateClient(string resource)
        {
            var restClient = new RestClient(resource) { Timeout = Options.timeOut };
            return restClient;
        }


        //<T> T써놓은건 클래스 형식 상관없이 파싱하려는거
        //일단 하나만 테스트로 받아오면 되니까 <T> 필요없을것 같아
        //그리고 바인드에서 쓰는 JSon형식이 아니니까 TResponse도 쓰면 값이 안받아와져
        //일단 클래스 가져왓어 넵 나중에 다른 기능도 더 넣으려면 Task<T> GetResponse<T>로 하면 되죠?엉 StoreSaleResult대신 T쓰면 돼 네네
        //지금 오류나지 않아? 네 오류나요
        //그 멤버랑 로그인 Service쪽에 GetResponse가 있는데 이걸 건드려서 그런거같거든?
        //다 주석처리 해놓을게
        public async Task<T> GetResponse<T>(string resource, Method method, string parameterJson = null, UrlSegment[] urlSegments = null, Header[] headers = null)
        {
            var client = CreateClient(resource);
            var restRequest = CreateRequest(resource, method, parameterJson, Options.tokenInfo, urlSegments, headers);
            var response = await client.ExecuteTaskAsync(restRequest);
            //if (CheckTokenExpired(response))
            //{
            //    await TokenRefresh();
            //    return await GetResponse<T>(resource, method, parameterJson, LoginType.TEACHER, urlSegments, headers);
            //}
            var resp = DeserializeSnakeCase<T>(response.Content);

            return resp;
        }

        //private bool ValidCheckResp<T> (TResponse<T> resp)
        //{
        //    if (resp != null && resp.Status == 200)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public async Task<IRestResponse> PostFormAsync(string formatFileUrl, string contentType, byte[] formData, string extension)
        {

            var restClient = new RestClient(Options.serverUrl);

            var request = new RestRequest(Options.serverUrl+formatFileUrl, Method.POST);
            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            request.AddHeader("Content-Type", contentType);

            request.AddHeader("x-access-token", Options.tokenInfo.Token);
            //사용자 토큰 넣으면됨
            request.AddHeader("Content-Length", Convert.ToString(formData.Length));
            byte[] byte1 = formData;

            request.AddFileBytes(extension, byte1, extension);

            var dt = await restClient.ExecuteTaskAsync(request);

            return dt;
        }

        public class UrlSegment
        {
            public string Name { get; set; }
            public object Value { get; set; }

            public UrlSegment(string name, object value)
            {
                Name = name;
                Value = value;
            }
        }

        public class Header
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public Header(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }
    }
}

