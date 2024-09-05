using System;
using System.Runtime.InteropServices;

public class MsgBox
{
    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
    public static extern int MessageBox(IntPtr handle, string message, string title, int type);

    public static void ShowMessage(string title, string message)
    {
        MessageBox(IntPtr.Zero, message, title, 0);
    }
}
