using System;
using UnityEngine;

namespace Core
{
    [Flags]
    public enum ELogCategory
    {
        None = 0,
        TitlePopup = 1 << 0,
        AssetManager = 2 << 1,
    }

    [CreateAssetMenu(fileName = "LogManagerSetting", menuName = "Scriptable Objects/LogManagerSetting")]
    public class LogManagerSettingSO : ScriptableObject
    {
        public ELogMessageType ShowingMessageType;
        public ELogCategory ShowingCategory;

#if UNITY_EDITOR
        public void OnEnable()
        {
            LogManager.SetLogFilter(ShowingMessageType, ShowingCategory);
        }

        public void OnValidate()
        {
            LogManager.SetLogFilter(ShowingMessageType, ShowingCategory);
        }
#endif
    }
}
