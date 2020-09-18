using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNetwork.Data
{
    public class TResponse<T>
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }

        public static implicit operator TResponse<T>(TResponse<string> v)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class Nothing
    {
        public static Nothing AtAll => null;
    }
}
