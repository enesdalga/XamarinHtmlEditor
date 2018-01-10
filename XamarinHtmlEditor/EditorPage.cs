using System;

using Xamarin.Forms;

namespace XamarinHtmlEditor
{
    public class EditorPage : ContentPage
    {
        public MainPage responsePage;
        public string html { get; set; }
        public bool CancelsTouchesInView = true;
        public EditorPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new HtmlEditor()
                     {
                         HorizontalOptions = LayoutOptions.FillAndExpand,
                         VerticalOptions = LayoutOptions.FillAndExpand
                     }
                }
            };
            this.Appearing += async (sender, args) =>
            {
                if (html != null)
                {
                    await HtmlEditor.SetHtml(html);
                }
            };

            var save = new ToolbarItem
            {
                Name = "Save",
                Priority = 0,
                Order = ToolbarItemOrder.Primary,
            };
            save.Clicked += async (sender, args) =>
            {
                await HtmlEditor.GetHtml();
                responsePage.Html = ((App)Application.Current).cevapHtml;
                await Navigation.PopAsync();
            };
            ToolbarItems.Add(save);
        }
    }
}

