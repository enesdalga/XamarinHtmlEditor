using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinHtmlEditor;
using XamarinHtmlEditor.iOS;

[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace XamarinHtmlEditor.iOS
{
    public class WebViewRender : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var webView = e.NewElement as WebViewer;
            if (webView != null)
                webView.EvaluateJavascript = (js) =>
                {
                    return Task.FromResult(this.EvaluateJavascript(js));
                };
        }
    }
}