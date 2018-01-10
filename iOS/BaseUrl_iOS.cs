using System;
using Foundation;
using Xamarin.Forms;
using XamarinHtmlEditor.iOS;

[assembly: Dependency (typeof (BaseUrl_iOS))]
namespace XamarinHtmlEditor.iOS{
  public class BaseUrl_iOS : IBaseUrl {
    public string Get() {
      return NSBundle.MainBundle.BundlePath;
    }
  }
}
