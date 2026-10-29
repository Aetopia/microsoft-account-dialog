using System;
using System.Runtime.InteropServices;

namespace Account.Dialog.Microsoft.Extensions;

unsafe static class MarshalExtensions
{
    extension(Marshal)
    {
        internal static T GetObjectForIUnknown<T>(void* ptr)
        {
            try { return (T)Marshal.GetObjectForIUnknown((nint)ptr); }
            finally { Marshal.Release((nint)ptr); }
        }
    }
}