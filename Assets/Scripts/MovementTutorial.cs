using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MovementTutorial : MonoBehaviour
    {
        public bool isVisible;
        public SpriteRenderer rend;
        // Start is called before the first frame update
        void Start()
        {
            isVisible = true;
           
        }

        public void RemoveMe()
        {
            isVisible = false;
            rend.color = Color.clear;
        }
        
        // Update is called once per frame
        void Update()
        {
            this.transform.localPosition += Vector3.up * (Mathf.Sin(Time.realtimeSinceStartup) * 0.1f);
        }
    }
}
