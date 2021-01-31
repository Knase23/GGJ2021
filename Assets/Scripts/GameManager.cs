using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public void Start()
        {
            // Force Resolution
            Screen.SetResolution(960,540,FullScreenMode.Windowed);
        }
    }
}