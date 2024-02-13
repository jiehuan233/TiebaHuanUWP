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
    public sealed partial class BlankPage3 : Page
    {
        public BlankPage3()
        {
            this.InitializeComponent();
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
            string log_url = "https://wappass.baidu.com/passport/?login&u=http%3A%2F%2Ftieba.baidu.com%2Fmo%2Fq---F01A6832500E30E2BE965B337275AA09%3AFG%3D1--1-3-0--2--wapp_1608480082014_787%2Fm%3Ftn%3DbdIndex%26lp%3D6013%26pinf%3D1_2_0&ssid=&from=&uid=F01A6832500E30E2BE965B337275AA09%3AFG%3D1&pu=&auth=&originid=2&mo_device=1&bd_page_type=1&tn=bdIndex&regtype=1&tpl=tb#/insert_account";
            string reg_url = "https://wappass.baidu.com/passport/?reg&u=http%3A%2F%2Ftieba.baidu.com%2Fmo%2Fq---F01A6832500E30E2BE965B337275AA09%3AFG%3D1--1-3-0--2--wapp_1608480082014_787%2Fm%3Ftn%3DbdIndex%26lp%3D6013%26pinf%3D1_2_0&ssid=&from=&uid=F01A6832500E30E2BE965B337275AA09%3AFG%3D1&pu=&auth=&originid=2&mo_device=1&bd_page_type=1&tn=bdIndex&regtype=1&tpl=tb#/insert_account";
            if (e.Parameter.ToString().Equals("login"))
            {
                log_page.Source = new Uri(log_url);
            }else if (e.Parameter.ToString().Equals("reg"))
            {
                log_page.Source = new Uri(reg_url);
            }
            else
            {
                log_page.Source= new Uri("https://www.microsoft.com/zh-cn/error");
            }
            //post_view.Source = new Uri(Url);
        }
    }
}
