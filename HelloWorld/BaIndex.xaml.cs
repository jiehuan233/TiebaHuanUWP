using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace HelloWorld
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        String url = "https://tieba.baidu.com/mo/q---F01A6832500E30E2BE965B337275AA09%3AFG%3D1--1-3-0--2--wapp_1608480082014_787/m?kw=";
        public BlankPage1()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                Ba_Name.Text = e.Parameter.ToString()+" 吧";
                url = url + e.Parameter.ToString();
                Ba_Page.Source = new Uri(url);
            }
            else
            {
                Ba_Name.Text = "百度贴吧 吧";
                url = url + "百度贴吧";
                Ba_Page.Source = new Uri(url);
            }
            base.OnNavigatedTo(e);

        }
    }
}
