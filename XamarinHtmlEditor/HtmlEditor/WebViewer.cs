using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinHtmlEditor
{
    public class WebViewer : WebView
    {
        public static BindableProperty EvaluateJavascriptProperty = BindableProperty.Create(nameof(EvaluateJavascript), typeof(Func<string, Task<string>>), typeof(WebViewer), null, BindingMode.OneWayToSource);
        public Func<string, Task<string>> EvaluateJavascript
        {
            get => (Func<string, Task<string>>)GetValue(EvaluateJavascriptProperty);
            set => SetValue(EvaluateJavascriptProperty, value);
        }


    }
}

