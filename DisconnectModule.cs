using UnityEngine;

namespace HSLibrary
{
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

}
