using System;
using System.Threading;
using UnityEngine;

namespace Doorstop
{
    public class Entrypoint
    {
        public static void Start()
        {
            try
            {
                var workerThread = new Thread(StartInner);
                workerThread.IsBackground = true;
                workerThread.Start();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage("Exception occured in Doorstop.Entrypoint", ex.ToString());
            }
        }

        public static void StartInner()
        {
            try
            {
                new FileLogger("E:\\Logs\\doorstop-debug.log").LogInfo("Hello world!");

                Thread.Sleep(10000);

                HSLibrary.ModLoader.Load();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage("Exception occured in Doorstop.StartInner", ex.ToString());
            }
        }
    }
}

