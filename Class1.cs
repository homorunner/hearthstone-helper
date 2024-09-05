using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MsgBox
{
    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
    public static extern int MessageBox(IntPtr handle, string message, string title, int type);

    public static void ShowMessage(string title, string message)
    {
        MessageBox(IntPtr.Zero, message, title, 0);
    }
}

public class FileLogger
{
    private string path;

    public FileLogger(string path_)
    {
        path = path_;
    }

    public void LogInfo(string message)
    {
        using (StreamWriter sw = new StreamWriter(path, append: true, encoding: System.Text.Encoding.UTF8))
        {
            sw.Write($"{DateTime.Now} [INFO] {message}\n");
        }
    }
}

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



namespace HSLibrary
{
    public class ModLoader
    {
        // entrance of the whole Dva instance.
        private static GameObject selfGameObj;

        public static void Load()
        {
            selfGameObj = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(selfGameObj);

            selfGameObj.AddComponent<HSDontDestroyOnLoad>();
            selfGameObj.AddComponent<DisconnectModule>();
            selfGameObj.AddComponent<AccelerateModule>();
        }
    }
    class DisconnectModule : MonoBehaviour
    {
        public void Start()
        {
            new FileLogger("E:\\Logs\\mod-debug.log").LogInfo("DisconnectModule started!");
        }

        public void OnGUI()
        {
            if (GUI.Button(new Rect(30, 10, 120, 50), ("拔线")))
            {
                Network.Get()?.DisconnectFromGameServer();
            };
        }
    }

    class AccelerateModule : MonoBehaviour
    {
        public static float globalAcc = 1;

        public void Start()
        {
            new FileLogger("E:\\Logs\\mod-debug.log").LogInfo("AccelerateModule started!");
        }

        public void OnGUI()
        {
            if (GUI.Button(new Rect(30, 80, 120, 50), ($"游戏加速：{globalAcc}倍")))
            {
                if (globalAcc == 4) globalAcc = 1;
                else globalAcc *= 2;
            };
        }

        public void Update()
        {
            Time.timeScale = globalAcc;
        }
    }
}
