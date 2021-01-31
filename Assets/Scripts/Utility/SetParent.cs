using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SetParent : MonoBehaviour
    {
        private Dictionary<GameObject, StateOnEnter> prevoiusParent = new Dictionary<GameObject, StateOnEnter>();

        public Transform parentingObject;

        class StateOnEnter
        {
            public Transform realParent;
            public bool wasKinimatic;

            public StateOnEnter(Transform realParent, bool wasKinimatic)
            {
                this.realParent = realParent;
                this.wasKinimatic = wasKinimatic;
            }
        }
        
        public void Lock()
        {
            foreach (KeyValuePair<GameObject,StateOnEnter> stateOnEnter in prevoiusParent)
            {
                Lock(stateOnEnter.Key);
            }
        }

        public void UnLock()
        {
            foreach (KeyValuePair<GameObject,StateOnEnter> stateOnEnter in prevoiusParent)
            {
                Unlock(stateOnEnter.Key);
            }
        }

        private void Unlock(GameObject gameObject)
        {
            gameObject.transform.SetParent(prevoiusParent[gameObject].realParent);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = prevoiusParent[gameObject].wasKinimatic;
        }

        private void Lock(GameObject gameObject)
        {
            gameObject.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.transform.SetParent(parentingObject);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Monster"))
            {
                // If player or a Character Enters
                if (prevoiusParent.ContainsKey(other.gameObject))
                    return;
                
                //Add to List
                prevoiusParent.Add(other.gameObject, new StateOnEnter(other.transform.parent,other.attachedRigidbody.isKinematic));
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            // If player or a Character Enters
            if(prevoiusParent.ContainsKey(other.gameObject) == false)
                return;
            Unlock(other.gameObject);
            prevoiusParent.Remove(other.gameObject);
        }
    }
}
