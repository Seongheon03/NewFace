using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TNetwork;
using TNetwork.Common;
using TNetwork.Data;
using static NewFace.Controls.RegisterControl;

namespace NewFace.Controls
{
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }
        JObject jObj = new JObject();
        NetworkManager networkManager = new NetworkManager();
        public async void Login()
        {
            JObject jObject = new JObject();
            jObj["id"] = tbId.Text;
            jObj["pw"] = tbPassword.Text;
            var resp = await networkManager.GetResponse<User>("/auth/login", Method.POST, jObj.ToString());
            //MessageBox.Show(resp.Message);
            //if (resp.Message.Equals("로그인 성공."))
            //{
            //    this.Visibility = Visibility.Collapsed;
            //    Options.tokenInfo = new TokenInfo(resp.Data.token, null);
            //}
        }
        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            ctrlRegister.Visibility = Visibility.Visible;
        }
    }
}
