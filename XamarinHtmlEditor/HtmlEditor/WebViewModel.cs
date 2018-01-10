using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinHtmlEditor
{
    public class WebViewModel : BindableObject
    {
        public WebViewModel()
        {
            if ((Device.RuntimePlatform == Device.iOS))
            WebViewSource=System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), "webiOS/index.html");
            else if ((Device.RuntimePlatform == Device.Android))
                WebViewSource = "file:///android_asset/web/index.html";
        }

        public static readonly BindableProperty WebViewSourceProperty = BindableProperty.Create(
             propertyName: "WebViewSource",
             returnType: typeof(string),
             declaringType: typeof(WebViewer),
             defaultValue: default(string));

        public string WebViewSource
        {
            get => (string)GetValue(WebViewSourceProperty);
            set => SetValue(WebViewSourceProperty, value);
        }

        public Func<string, Task<string>> EvaluateJavascript { get; set; }

        public ICommand KaydetCommand => new Command((async () =>
        {
            var oku = await EvaluateJavascript("komut();");
        }));

    }
}

