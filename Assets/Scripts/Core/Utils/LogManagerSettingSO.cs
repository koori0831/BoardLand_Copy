using System;
using UnityEngine;

namespace Core
{
    [Flags]
    public enum ELogCategory
    {
        None = 0,
        TitlePopup = 1 << 0,
        AssetManager = 1 << 1,
        GameSystem = 1 << 2,
    }

    [CreateAssetMenu(fileName = "LogManagerSetting", menuName = "Scriptable Objects/LogManagerSetting")]
    public class LogManagerSettingSO : ScriptableObject
    {
        public ELogLevel ShowingLogLevel;
        public ELogCategory ShowingCategory;

#if UNITY_EDITOR
        public void OnEnable()
        {
            LogManager.SetLogFilter(ShowingLogLevel, ShowingCategory);
        }

        public void OnValidate()
        {
            LogManager.SetLogFilter(ShowingLogLevel, ShowingCategory);
        }
#endif
    }
}
