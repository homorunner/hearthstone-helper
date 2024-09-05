using UnityEngine;

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
}
