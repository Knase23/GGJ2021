using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public InputAction ChangeViewMode;
        public InputAction QuitGameButton;
        
        private void Awake()
        {
            Screen.SetResolution(960,540,FullScreenMode.Windowed);
            BorderlessWindow.InitializeOnLoad();
        }

        public void Start()
        {
            // Force Resolution
#if UNITY_STANDALONE_WIN
            BorderlessWindow.SetFramelessWindow();
#endif
            ChangeViewMode.performed += ChangeViewModeOnPerformed;
            QuitGameButton.performed += QuitGameButtonOnPerformed;
        }

        private void QuitGameButtonOnPerformed(InputAction.CallbackContext obj)
        {
#if UNITY_STANDALONE
            if (obj.ReadValueAsButton())
            {
                Application.Quit();
            }
#endif
        }

        private void ChangeViewModeOnPerformed(InputAction.CallbackContext obj)
        {
            if(obj.ReadValueAsButton())
            {
                if (Screen.fullScreen)
                {
                    Screen.SetResolution(960,540,FullScreenMode.Windowed);
                }
                else
                {
                    Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
                }
            }
        }

        private void OnEnable()
        {
            ChangeViewMode.Enable();
            QuitGameButton.Enable();
        }

        private void OnDisable()
        {
            ChangeViewMode.Disable();
            QuitGameButton.Enable();
        }

//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//         private void MakeFrameLess()
//         {
// #if UNITY_STANDALONE_WIN
//             
//             //BorderlessWindow.SetFramelessWindow();
//             
// #endif
//         }
        
        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        // private void SetResolution()
        // {
        //     Screen.SetResolution(960,540,FullScreenMode.Windowed);
        //     
        //     // Not relaiable!
        //     //Invoke(nameof(MakeFrameLess),1f);
        //     //When MakeFrameLess is executed the resolution of the window so wrong. 
        //     // It looks like it adds resolution for the missing bar!
        // }
        
    }


    public abstract class AIState
    {
        private protected readonly AI Controller;

        protected AIState(AI controller)
        {
            Controller = controller;
        }

        public abstract void OnStateEnter();
        public abstract void Update();
    }

    public class AIUnawareAIState : AIState
    {
        public AIUnawareAIState(AI controller) : base(controller)
        {
            
        }        

        public override void OnStateEnter()
        {
            Debug.Log("AI says: Nothing here!");
        }

        public override void Update()
        {
            if (Controller.seeingPlayer)
            {
                Controller.SetState(new AIAlertState(Controller));
                return;
            }
            
            Debug.Log("AI: Moves around Unaware of the player!");
            
        }
    }
    public class AIAlertState : AIState
    {
        public AIAlertState(AI controller) : base(controller)
        {
            
        }
        
        public override void OnStateEnter()
        {
            Debug.Log("AI says: Where are you?");
        }

        public override void Update()
        {
            if (Controller.chasePlayer)
            {
                Controller.SetState(new AIAggressiveState(Controller));
                return;
            }
            if (Controller.seeingPlayer == false)
            {
                Controller.SetState(new AIUnawareAIState(Controller));
                return;
            }
            
            Debug.Log("AI: Moves around Alert looking for the player!");
        }
    }
    public class AIAggressiveState : AIState
    {
        
        public override void OnStateEnter()
        {
            // Plays Audio: "Found You!"
            Debug.Log("AI says: Found You!");
        }

        public override void Update()
        {
            //Check if we should go to another state. Logic
            if (Controller.chasePlayer == false)
            {
                Controller.SetState(new AIAlertState(Controller));
                return;
            }
            
            Debug.Log("AI: Moves Aggressively to the player!");
            // If we did not change state then!
            //      Chasing player, and Attacks when near Logic

        }
        public AIAggressiveState(AI controller) : base(controller)
        {
            
        }
    }
    
    public class AI : MonoBehaviour
    {
        private AIState _currentState;
        
        //Variables that the states can use!
        public GameObject player;
        public bool seeingPlayer;
        public bool chasePlayer;
        private void Start()
        {
            _currentState = new AIUnawareAIState(this);
        }

        public void SetState(AIState state)
        {
            _currentState = state;
            _currentState.OnStateEnter();
        }
        
        private void Update()
        {
            _currentState.Update();
        }
    }
    
    
    
}