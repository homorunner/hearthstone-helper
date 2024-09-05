using UnityEngine;

namespace HSLibrary
{
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
