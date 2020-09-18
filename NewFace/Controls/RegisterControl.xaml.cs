using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Windows;
using System.Windows.Controls;
using TNetwork;
using TNetwork.Data;

namespace NewFace.Controls
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        public RegisterControl()
        {
            InitializeComponent();
        }
        JObject jObj = new JObject();
        NetworkManager networkManager = new NetworkManager();
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }
        public class User
        {
            [JsonProperty("x-access-token")]
            public string token{ get; set; }
        }
        public async void Register()
        {
            jObj["id"] = tbId.Text;
            jObj["pw"] = tbPassword.Text;
            jObj["name"] = tbName.Text;
            jObj["email"] = tbEmail.Text;
            jObj["intro"] = tbIntro.Text;
            jObj["kakao"] = tbKakaoId.Text;
            var resp = await networkManager.GetResponse<User>("/auth/register", Method.POST, jObj.ToString());
            //MessageBox.Show(resp.Message);
            this.Visibility = Visibility.Collapsed;
        }
    }
}