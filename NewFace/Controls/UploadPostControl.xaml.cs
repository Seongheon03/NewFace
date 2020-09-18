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

namespace NewFace.Controls
{
    /// <summary>
    /// Interaction logic for UploadPostControl.xaml
    /// </summary>
    public partial class UploadPostControl : UserControl
    {
        NetworkManager networkManager = new NetworkManager();
        JObject jObject = new JObject();
        public UploadPostControl()
        {
            InitializeComponent();
            ShowPost();
        }
        private async void ShowPost()
        {
            //var resp = await networkManager.GetResponse<PostList>("/post", RestSharp.Method.GET);
            //List<Post> postLists = new List<Post>();
            //foreach (Post list in resp.Data.listPosts)
            //{
            //    postLists.Add(list);
            //}
            //lvPost.ItemsSource = postLists;
            //MessageBox.Show(resp.Message);
        }
        public class PostList
        {
            [JsonProperty("posts")]
            public List<Post> listPosts { get; set; }
        }
        public class Post
        {
            [JsonProperty("idx")]
            public int idx { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("content")]
            public string content { get; set; }

            [JsonProperty("memberId")]
            public string memberId { get; set; }
        }
        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            ctrlWritePost.Visibility = Visibility.Visible;
        }

        private void lvPost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
