using System;
using System.IO;
using Xamarin.Forms;

namespace XamarinHtmlEditor
{
    public interface IBaseUrl { string Get(); }
    public class web : ContentPage
    {
        public web()
        {
            var sayfa = new WebView
            {
                Source = "enesdalga.com",
                HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand
            };

            sayfa.Source = System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), "webiOS/index.html");

            Content = new StackLayout
            {
                Children={
                    new Label{Text="Hebele hubele"},
                    sayfa
                }
            };
        }
    }
}

