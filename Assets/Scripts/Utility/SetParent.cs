using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SetParent : MonoBehaviour
    {
        private Dictionary<GameObject, Transform> prevoiusParent = new Dictionary<GameObject, Transform>();

        public Transform parentingObject;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Monster"))
            {
                // If player or a Character Enters
                if (prevoiusParent.ContainsKey(other.gameObject))
                    return;
                prevoiusParent.Add(other.gameObject, other.transform.parent);
                other.transform.SetParent(parentingObject);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            // If player or a Character Enters
            if(prevoiusParent.ContainsKey(other.gameObject) == false)
                return;
            other.transform.SetParent(prevoiusParent[other.gameObject]);
            prevoiusParent.Remove(other.gameObject);
        }
    }
}
