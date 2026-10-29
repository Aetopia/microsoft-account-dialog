using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.PointOfService;
using Windows.Security.Authentication.Web.Core;
using Windows.UI.ApplicationSettings;
using Windows.Win32.System.WinRT;

sealed class MainForm : Form
{
    internal MainForm()
    {
        Size = new(0, 0);
        Location = new(0, 0);
        Text = "Microsoft Account Dialog";

        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
    }

    protected override async void OnShown(EventArgs args)
    {
        Size = new(0, 0); try
        {
            AccountsSettingsPane.GetForWindow(Handle).AccountCommandsRequested += OnAccountCommandsRequested;
            await AccountsSettingsPane.ShowManageAccountsForWindowAsync(Handle);
        }
        finally { Close(); }
    }

    async void OnAccountCommandsRequested(object sender, AccountsSettingsPaneCommandsRequestedEventArgs args)
    {
        var deferral = args.GetDeferral(); try
        {
            var provider = await WebAuthenticationCoreManager.FindAccountProviderAsync("https://login.microsoft.com", "consumers");
            args.WebAccountProviderCommands.Add(new(provider, OnWebAccountProviderCommandInvoked));
        }
        finally { deferral.Complete(); }
    }

    async void OnWebAccountProviderCommandInvoked(WebAccountProviderCommand sender)
    {
        WebTokenRequest request = new(sender.WebAccountProvider, "wl.basic", "none");
        await WebAuthenticationCoreManager.RequestTokenForWindowAsync(Handle, request);
    }
}