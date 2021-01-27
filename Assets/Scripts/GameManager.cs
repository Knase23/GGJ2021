using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private Resolution _screenSize;

        private bool _stateChange = true;

        private void Awake()
        {
            BorderlessWindow.InitializeOnLoad();
            BorderlessWindow.MoveWindowPos(Vector2.zero, Screen.currentResolution.width,Screen.currentResolution.height);
            BorderlessWindow.CenterWindow();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _screenSize = Screen.currentResolution;
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
            BorderlessWindow.MoveWindowPos(Vector2.zero, _screenSize.width,_screenSize.height);
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