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
using HtmlAgilityPack;
using Windows.Web.Http;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace HelloWorld
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlankPage2 : Page
    {
        string Url = "";
        public BlankPage2()
        {
            Task task = get_ba_name();
            this.InitializeComponent();

            //var mp = new MainPage();
            //Ba_Name.Text = mp.baname;
        }
        private async Task get_ba_name()
        {
            TextBlock textBlock = new TextBlock();

            // 创建一个 HttpClient 实例
            HttpClient httpClient = new HttpClient();

            // 发送 GET 请求并获取网页内容
            
            string html = await httpClient.GetStringAsync(new Uri(Url));

            // 使用 HtmlAgilityPack 解析 HTML
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // 根据 XPath 表达式选择 HTML 节点
            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div/a[6]");

            // 获取节点中的文本内容
            string content = htmlNode.InnerText;

            // 将内容赋值给 TextBlock 的 Text 属性
            textBlock.Text = content;
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Url = "https://tieba.baidu.com/mo/q---F01A6832500E30E2BE965B337275AA09%3AFG%3D1--1-3-0--2--wapp_1608480082014_787/" + e.Parameter.ToString();
            post_view.Source = new Uri(Url);
        }

    }
}
