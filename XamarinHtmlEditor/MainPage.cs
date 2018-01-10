using System;
using Plugin.HtmlLabel;
using Xamarin.Forms;

namespace XamarinHtmlEditor
{
    public class MainPage : ContentPage
    {
        public static readonly BindableProperty HtmlProperty = BindableProperty.Create(
             propertyName: "Html",
             returnType: typeof(string),
             declaringType: typeof(MainPage),
             defaultValue: default(string));

        public string Html
        {
            get => (string)GetValue(HtmlProperty);
            set => SetValue(HtmlProperty, value);
        }

        public MainPage()
        {
            BindingContext = this;

            lblResponse.SetBinding(Label.TextProperty, new Binding() { Path = "Html" });

            Content = new StackLayout
            {
                Children = {
                    lblResponse,
                    btnEditor
                }
            };
            btnEditor.Clicked += (sender, args) =>
            {
                Navigation.PushAsync(new EditorPage(){html=lblResponse.Text,responsePage=this});
            };
        }
        Button btnEditor = new Button()
        {
            Text = "Go to Editor Page",
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.DarkCyan,
            TextColor = Color.White,
            BorderRadius = 0,
            HeightRequest = 40
        };

        Label lblResponse = new Label()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Color.Black,
            LineBreakMode = LineBreakMode.WordWrap,
            HeightRequest = 150
        };
    }
}

