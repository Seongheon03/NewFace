using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using TNetwork.Data;

namespace NewFace.Controls
{
    /// <summary>
    /// Interaction logic for WritePostControl.xaml
    /// </summary>
    public partial class WritePostControl : UserControl
    {
        JObject jObject = new JObject();
        NetworkManager networkManager = new NetworkManager();
        public WritePostControl()
        {
            InitializeComponent();
        }
        private async void MakePost()
        {
            jObject["title"] = tbTitle.Text;
            jObject["content"] = tbContent.Text;
            var resp = await networkManager.GetResponse<Nothing>("/post", RestSharp.Method.POST, jObject.ToString());
            //MessageBox.Show(resp.Message);
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MakePost();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("취소되었습니다.");
            this.Visibility = Visibility.Collapsed;
        }
    }
}
