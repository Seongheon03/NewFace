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

namespace NewFace.Controls
{
    public partial class NavigationControl : UserControl
    {
        public NavigationControl()
        {
            InitializeComponent();
        }
        private void btnUploadPost_Click(object sender, RoutedEventArgs e)
        {
            ctrlTeamBuilding.Visibility = Visibility.Collapsed;
            ctrlCodeReview.Visibility = Visibility.Collapsed;
            ctrlProfile.Visibility = Visibility.Collapsed;
            ctrlUploadPost.Visibility = Visibility.Visible;
        }

        private void btnTeamBuilding_Click(object sender, RoutedEventArgs e)
        {
            ctrlUploadPost.Visibility = Visibility.Collapsed;
            ctrlCodeReview.Visibility = Visibility.Collapsed;
            ctrlProfile.Visibility = Visibility.Collapsed;
            ctrlTeamBuilding.Visibility = Visibility.Visible;
        }

        private void btnCodeReview_Click(object sender, RoutedEventArgs e)
        {
            ctrlUploadPost.Visibility = Visibility.Collapsed;
            ctrlTeamBuilding.Visibility = Visibility.Collapsed;
            ctrlProfile.Visibility = Visibility.Collapsed;
            ctrlCodeReview.Visibility = Visibility.Visible;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ctrlUploadPost.Visibility = Visibility.Collapsed;
            ctrlTeamBuilding.Visibility = Visibility.Collapsed;
            ctrlCodeReview.Visibility = Visibility.Collapsed;
            ctrlProfile.Visibility = Visibility.Visible;
        }
    }
}
