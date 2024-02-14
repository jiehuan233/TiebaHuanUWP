using HtmlAgilityPack;
using System;
using System.Windows;
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
using System.Net.Http;
using System.Diagnostics;
using Windows.Devices.Enumeration;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace HelloWorld
{
    
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        int page = 1;
        int pn = 0;
        List<string> links = new List<string>();
        List<string> texts = new List<string>();
        public string ba_name = "";
        static String url = "https://tieba.baidu.com/mo/q---F01A6832500E30E2BE965B337275AA09%3AFG%3D1--1-3-0--2--wapp_1608480082014_787/m?kw=";
        string urlall = url + "&pn=0";
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
            base.OnNavigatedTo(e);
                if (e.Parameter != null)
                {
                    string[] input = (string[])e.Parameter;
                    if (input[0] is string && !string.IsNullOrWhiteSpace(input[0]) && input[1].Equals("index"))
                    {
                        Ba_Name.Content = input[0] + " 吧";
                        urlall=url+ input[0]+ "&pn=0";
                        ba_name = input[0];
                        //Ba_Page.Source = new Uri(url);
                    }
                    else if(input[1].Equals("index"))
                    {
                       Ba_Name.Content = "百度贴吧 吧";
                        urlall = url+ "百度贴吧" + "&pn=0";
                        ba_name = "百度贴吧";
                        //Ba_Page.Source = new Uri(url);
                    }
                
                    get_posts(urlall);
                }
                
            
            
            
        }

        private async void get_posts(string aurl)
        {
            HttpClient httpClient = new HttpClient();

            // 发送 GET 请求并获取网页内容
            string html = await httpClient.GetStringAsync(aurl);

            // 使用 HtmlAgilityPack 解析 HTML
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // 查找所有 class 为 "i" 的 div 标签
            IEnumerable<HtmlNode> divNodes = htmlDocument.DocumentNode.Descendants("div")
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "i");

            texts.Add("上一页");
            links.Add("before");

            // 遍历每个 div 标签，获取第一个 a 标签的链接
            foreach (HtmlNode divNode in divNodes)
            {
                HtmlNode aNode = divNode.Descendants("a").FirstOrDefault();
                if (aNode != null)
                {
                    string link = aNode.GetAttributeValue("href", "");
                    texts.Add(aNode.InnerText.Replace("&#160;"," "));
                    links.Add(link);
                }
            }
            texts.Add("下一页");
            links.Add("next");

            listView.ItemsSource = null;
            listView.ItemsSource = texts;

            listView.UpdateLayout();
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //this.Frame.Navigate(typeof(Page1));
            //try
           // {
                if (e.ClickedItem.ToString().Equals("上一页"))
                {
                    if (page > 1)
                    {
                        page-=2;
                        pn -= 20;
                        Dialog_NextButtonClick();
                    }
                }else if (e.ClickedItem.ToString().Equals("下一页"))
                {
                    Dialog_NextButtonClick();
                }
                else
                {
                try
                {
                    this.Frame.Navigate(typeof(BlankPage2), links[texts.BinarySearch(e.ClickedItem.ToString())]);
                }
                catch(Exception ex)
                {
                    bool is_Run = false;
                    for(int i = 1; i <= 10; i++)
                    {
                        if (e.ClickedItem.ToString().Equals(texts[i]))
                        {
                            this.Frame.Navigate(typeof(BlankPage2), links[i]);
                            is_Run = true; break;
                        }
                    }
                    if (!is_Run)
                    {
                        throw ex;
                    }
                        
                    
                }
                    
                }
                
           // }
           /* catch (Exception)
            {
                this.Frame.Navigate(typeof(BlankPage2), links[10]);
            }*/
            
        }
        private async void Refresh_Page(object sender, RoutedEventArgs e)
        {
            links.Clear();
            texts.Clear();
            get_posts(url + ba_name + "&pn=" + pn.ToString());
        }
        private async void Choose_Page(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();

            // 创建一个StackPanel对象
            StackPanel panel = new StackPanel();

            // 创建一个TextBox对象
            TextBox textBox = new TextBox();
            
            TextBlock textBlock = new TextBlock();

            // 设置TextBox的属性
            textBox.Width = 200;
            textBox.Height = 40;
            textBox.PlaceholderText = "页面切换";

            textBlock.Text = "请输入页码  当前页面："+page;

            // 将TextBox和Button添加到StackPanel中
            panel.Children.Add(textBlock);
            panel.Children.Add(textBox);


            // 将StackPanel设置为ContentDialog的内容
            dialog.Content = panel;

            // 设置ContentDialog的命令按钮的文本
            dialog.PrimaryButtonText = "取消";
            dialog.SecondaryButtonText = "下一页";
            dialog.CloseButtonText = "确定";

            // 为ContentDialog的命令按钮添加事件处理程序
            dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick;
            dialog.SecondaryButtonClick += Dialog_NextButtonClick;
            dialog.CloseButtonClick += Dialog_YesButtonClick;

            // 显示ContentDialog
            await dialog.ShowAsync();
        }
        private void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            sender.Hide();
        }

        private void Dialog_NextButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Dialog_NextButtonClick();
        }
        private void Dialog_NextButtonClick()
        {
            page++;
            pn += 10;
            urlall = url + ba_name + "&pn=" + pn.ToString();
            links.Clear();
            texts.Clear();
            get_posts(urlall);
        }
        private async void Dialog_YesButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            StackPanel panel = sender.Content as StackPanel;
            TextBox textBox = panel.Children[1] as TextBox;
            string input = textBox.Text;
            int tmp;
            string t1 = input;
            if (!int.TryParse(t1, out tmp))
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "您输入的不是数字",
                    CloseButtonText = "OK"
                };
            }

            else
            {
                if (Int32.Parse(input) > 0)
                {
                    pn=(Int32.Parse(input)-1)*10;
                    page = Int32.Parse(input);
                }
                else
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "您的输入有误",
                        CloseButtonText = "OK"
                    };
                }
                
            }
            urlall = url +ba_name+ "&pn=" + pn.ToString();
            links.Clear();
            texts.Clear();
            get_posts(urlall);
        }
    }
}
