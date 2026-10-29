using System;
using System.Runtime.InteropServices;
using Account.Dialog.Microsoft.Extensions;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.Win32.System.WinRT;
using static System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal;

unsafe static class AccountsSettingsPaneExtensions
{
    static readonly Guid s_iid;
    static readonly IAccountsSettingsPaneInterop s_class;

    static AccountsSettingsPaneExtensions()
    {
        var type = typeof(AccountsSettingsPane);
        var @class = GetActivationFactory(type);

        s_iid = type.GetInterfaces()[0].GUID;
        s_class = (IAccountsSettingsPaneInterop)@class;
    }

    extension(AccountsSettingsPane)
    {
        internal static AccountsSettingsPane GetForWindow(nint hwnd)
        {
            fixed (Guid* iid = &s_iid)
            {
                s_class.GetForWindow(new(hwnd), iid, out var ptr);
                return Marshal.GetObjectForIUnknown<AccountsSettingsPane>(ptr);
            }
        }

        internal static IAsyncAction ShowManageAccountsForWindowAsync(nint hwnd)
        {
            var iid = typeof(IAsyncAction).GUID;
            s_class.ShowManageAccountsForWindowAsync(new(hwnd), &iid, out var ptr);
            return Marshal.GetObjectForIUnknown<IAsyncAction>(ptr);
        }
    }
}