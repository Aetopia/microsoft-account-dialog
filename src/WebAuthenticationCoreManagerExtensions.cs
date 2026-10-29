using System.Runtime.InteropServices;
using Account.Dialog.Microsoft.Extensions;
using Windows.Foundation;
using Windows.Security.Authentication.Web.Core;
using Windows.Win32.Foundation;
using Windows.Win32.System.WinRT;
using static System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal;

unsafe static class WebAuthenticationCoreManagerExtensions
{
    static readonly IWebAuthenticationCoreManagerInterop s_class;

    static WebAuthenticationCoreManagerExtensions()
    {
        var type = typeof(WebAuthenticationCoreManager);
        s_class = (IWebAuthenticationCoreManagerInterop)GetActivationFactory(type);
    }

    extension(WebAuthenticationCoreManager)
    {
        internal static IAsyncOperation<WebTokenRequestResult> RequestTokenForWindowAsync(nint hwnd, WebTokenRequest request)
        {
            var @class = (IInspectable)(object)request;
            var iid = typeof(IAsyncOperation<WebTokenRequestResult>).GUID;

            s_class.RequestTokenForWindowAsync((HWND)hwnd, @class, &iid, out var ptr);
            return Marshal.GetObjectForIUnknown<IAsyncOperation<WebTokenRequestResult>>(ptr);
        }
    }
}