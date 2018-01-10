using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plugin.Clipboard;
using Xamarin.Forms;

namespace XamarinHtmlEditor
{
    public class HtmlEditor : ContentView
    {
        private static WebViewer wb;


        public HtmlEditor()
        {
            var model = new WebViewModel { };
            BindingContext = model;


            var btn = new Button();

            wb = new WebViewer
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            wb.SetBinding(WebView.SourceProperty, new Binding() { Path = "WebViewSource" });
            wb.SetBinding(WebViewer.EvaluateJavascriptProperty, new Binding() { Path = "EvaluateJavascript", Mode = BindingMode.OneWayToSource });


            var altKomutlar = new ScrollView()
            {
                Orientation = ScrollOrientation.Horizontal,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.FromHex("e5e7e7"),
                HeightRequest = 50,
                Content = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        link,
                        bold,
                        italic,
                        underline,
                        justify,
                        right,
                        left,
                        strike,
                        center,
                        list_bullet,
                        list_number,
                        indent_right,
                        indent_left,
                        quote
                    }
                }
            };

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    wb,
                    altKomutlar
                }
            };

            btn.Clicked += async (sender, args) =>
            {
                if (wb.EvaluateJavascript != null)
                {
                    var aa = await wb.EvaluateJavascript("getHtmlData()");
                }
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                var komut = ((Image)s).AutomationId;
                switch (komut)
                {
                    case "link":
                        var metin = await CrossClipboard.Current.GetTextAsync();
                        metin = WebUtility.HtmlDecode(metin);
                        metin = metin.Replace("'", @"\'");
                        metin = metin.Replace("\n", "");
                        metin = metin.Replace("\r", "");
                        var js = $"htmlEkle('{metin}');";
                        await wb.EvaluateJavascript(js);
                        break;
                    case "bold":
                        await wb.EvaluateJavascript("komut('bold');");
                        break;
                    case "italic":
                        await wb.EvaluateJavascript("komut('italic');");
                        break;
                    case "underline":
                        await wb.EvaluateJavascript("komut('underline');");
                        break;
                    case "justify":
                        await wb.EvaluateJavascript("komut('justify');");
                        break;
                    case "right":
                        await wb.EvaluateJavascript("komut('right');");
                        break;
                    case "left":
                        await wb.EvaluateJavascript("komut('left');");
                        break;
                    case "strike":
                        await wb.EvaluateJavascript("komut('strike');");
                        break;
                    case "center":
                        await wb.EvaluateJavascript("komut('center');");
                        break;
                    case "list_bullet":
                        await wb.EvaluateJavascript("komut('list_bullet');");
                        break;
                    case "list_number":
                        await wb.EvaluateJavascript("komut('list_number');");
                        break;
                    case "indent_right":
                        await wb.EvaluateJavascript("komut('indent_right');");
                        break;
                    case "indent_left":
                        await wb.EvaluateJavascript("komut('indent_left');");
                        break;
                    case "quote":
                        await wb.EvaluateJavascript("komut('quote');");
                        break;
                    case "copy":
                        await wb.EvaluateJavascript("komut('copy');");
                        break;
                    case "paste":
                        await wb.EvaluateJavascript("komut('paste');");
                        break;
                    case "font_back":
                        await wb.EvaluateJavascript("komut('font_back');");
                        break;
                    case "font_color":
                        await wb.EvaluateJavascript("komut('font_color');");
                        break;
                    case "highlight":
                        await wb.EvaluateJavascript("komut('highlight');");
                        break;
                    case "h1":
                        await wb.EvaluateJavascript("komut('h1');");
                        break;
                }
            };
            bold.GestureRecognizers.Add(tapGestureRecognizer);
            italic.GestureRecognizers.Add(tapGestureRecognizer);
            underline.GestureRecognizers.Add(tapGestureRecognizer);
            justify.GestureRecognizers.Add(tapGestureRecognizer);
            right.GestureRecognizers.Add(tapGestureRecognizer);
            left.GestureRecognizers.Add(tapGestureRecognizer);
            strike.GestureRecognizers.Add(tapGestureRecognizer);
            center.GestureRecognizers.Add(tapGestureRecognizer);
            list_bullet.GestureRecognizers.Add(tapGestureRecognizer);
            list_number.GestureRecognizers.Add(tapGestureRecognizer);
            indent_right.GestureRecognizers.Add(tapGestureRecognizer);
            indent_left.GestureRecognizers.Add(tapGestureRecognizer);
            quote.GestureRecognizers.Add(tapGestureRecognizer);
            link.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public static async Task GetHtml()
        {
            if (wb.EvaluateJavascript != null)
            {
                var aa = await wb.EvaluateJavascript("getHtmlData()");
                var yeni = aa.Replace(@"\u003C", "<");
                yeni = Regex.Unescape(yeni);
                yeni = yeni.Trim('"');
                ((App)Application.Current).cevapHtml = yeni;
            }
        }

        public static async Task SetHtml(string html)
        {
            html = html.Trim();
            html = html.Replace("\n", "<br/>");
            html = html.Replace("\r", "<br/>");

            html = html.Replace("'", @"\'");
            html = html.Replace('"', '\"');

            for (var i = 0; i < 5; i++)
            {
                var js = "setHtmlData('" + html + "');";
                var gelen = await wb.EvaluateJavascript(js);
                if (gelen == "\"tamam\"")
                {
                    break;
                }
                await Task.Delay(1000);
            }
        }



        readonly Image bold = new Image() { Source = "bold.png", AutomationId = "bold" };
        readonly Image italic = new Image() { Source = "italic.png", AutomationId = "italic" };
        readonly Image underline = new Image() { Source = "underline.png", AutomationId = "underline" };
        readonly Image justify = new Image() { Source = "justify.png", AutomationId = "justify" };
        readonly Image right = new Image() { Source = "right.png", AutomationId = "right" };
        readonly Image left = new Image() { Source = "left.png", AutomationId = "left" };
        readonly Image strike = new Image() { Source = "strike.png", AutomationId = "strike" };
        readonly Image center = new Image() { Source = "center.png", AutomationId = "center" };
        readonly Image list_bullet = new Image() { Source = "list_bullet.png", AutomationId = "list_bullet" };
        readonly Image list_number = new Image() { Source = "list_number.png", AutomationId = "list_number" };
        readonly Image indent_right = new Image() { Source = "indent_right.png", AutomationId = "indent_right" };
        readonly Image indent_left = new Image() { Source = "indent_left.png", AutomationId = "indent_left" };
        readonly Image quote = new Image() { Source = "quote.png", AutomationId = "quote" };
        readonly Image link = new Image() { Source = "link.png", AutomationId = "link" };
    }


}
