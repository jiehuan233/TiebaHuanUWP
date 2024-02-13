using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using HtmlAgilityPack;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Devices.Enumeration;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace HelloWorld
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //public string baname = "百度贴吧";
        private async void FetchAndDisplayImage()
        {
            // 使用 HttpClient 获取 HTML 内容
            using (HttpClient client = new HttpClient())
            {
                string htmlContent = await client.GetStringAsync(new Uri("https://tieba.baidu.com/mo/q---F01A6832500E30E2BE965B337275AA09%3AFG%3D1--1-3-0--2--wapp_1608480082014_787/m?tn=bdIndex&lp=5014")); // 替换为你要抓取的网页 URL

                // 使用 HtmlAgilityPack 解析 HTML
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                // 使用 XPath 选择图片节点
                HtmlNode imgNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/img");

                // 提取图片 URL
                string imageUrl = "https:"+imgNode?.GetAttributeValue("src", "");
                Debug.WriteLine("imageUrl");

                // 显示图片
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    
                    ImageView.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(imageUrl));//imageUrl
                }
            }
        }
        public MainPage()
        {
            this.InitializeComponent();
            FetchAndDisplayImage();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //baname = inputBa.Text;
            string[] input= { inputBa.Text, "index" };
            Frame.Navigate(typeof(BlankPage1),input);
            //LoadWebPage();
            //button.Visibility = Visibility.Collapsed;
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage3), "login");
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage3), "reg");
        }
    }
}
