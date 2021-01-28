using UnityEngine;

namespace Game
{
    public class WindowResizer : MonoBehaviour
    {
        private bool _stateChange = true;
        private Resolution _screenSize;

        private void Awake()
        {
            _screenSize = Screen.currentResolution;
            BorderlessWindow.InitializeOnLoad();
            BorderlessWindow.MoveWindowPos(Vector2.zero, Screen.currentResolution.width,
                Screen.currentResolution.height);
            BorderlessWindow.CenterWindow();
        }

        public void ChangeToHalfSize()
        {
            _stateChange = !_stateChange;
            if (_stateChange)
            {
                _screenSize.height *= 2;
                _screenSize.width *= 2;
            }
            else
            {
                _screenSize.height /= 2;
                _screenSize.width /= 2;
            }
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            BorderlessWindow.MoveWindowPos(Vector2.zero, _screenSize.width, _screenSize.height);
            BorderlessWindow.CenterWindow();
        }

        public void SetHeight(int height)
        {
            _screenSize.height = height;
        }

        public void SetWidth(int width)
        {
            _screenSize.width = width;
        }
    }
}