using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution(960,540,FullScreenMode.Windowed);
            BorderlessWindow.InitializeOnLoad();
        }

        public void Start()
        {
            // Force Resolution
            BorderlessWindow.SetFramelessWindow();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private void MakeFrameLess()
        {
            BorderlessWindow.SetFramelessWindow();
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private void SetResolution()
        {
            Screen.SetResolution(960,540,FullScreenMode.Windowed);
        }
        
        
    }
}