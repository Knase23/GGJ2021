using System;
using System.Collections.Generic;
using Game.Core;
using Game.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.AI
{
    public class AIDialogue : MonoBehaviour, IHearing, ITalker
    {
        private Hieroglyph _latestGlyph = null;
        public float talkRange = 10;


        public HieroglyphBubble talkBubble;

        public AiBehaviour FirstBehaviour;

        public List<UnityEvent> actions = new List<UnityEvent>();

        public AiBehaviour currentAiBehaviour;

        public GameObject[] interestedSources;
        
        
        private void Start()
        {
            currentAiBehaviour = FirstBehaviour;
        }

        private void OnEnable()
        {
            DialogueSystem.AddIHearingToList(this);
        }

        private void OnDisable()
        {
            DialogueSystem.AddIHearingToList(this);
        }

        public Vector2 GetLocation()
        {
            return transform.position;
        }

        public void OnHearing(ITalker talker)
        {
            //Debug.Log($"Heard {talker.GetName()} say: {talker.GetLatestWord()}");
            Hieroglyph talkerWord = talker.GetLatestWord(); // Get what it said

            bool isInterested = false;
            foreach (GameObject interestedSource in interestedSources)
            {
                if (talker.GetSource() == interestedSource)
                {
                    isInterested = true;
                    break;
                }
            }
            
            if(isInterested == false) return;

            AiBehaviour.BehaviourChange
                changeInBehaviour = currentAiBehaviour.GetResponse(talkerWord); // Check how we react

            if (changeInBehaviour.onResponse > -1 && changeInBehaviour.onResponse < actions.Count
            ) // Check if their is a action relate
                actions[changeInBehaviour.onResponse]?.Invoke();
            
            
            talker.WaitingForSecondWord(changeInBehaviour.nextBehaviour.myWord is LogicGlyph); // Is the Ai expecting a second word

            currentAiBehaviour = changeInBehaviour.nextBehaviour; // Change our behavior to next one!
            if (currentAiBehaviour.myWord is LogicGlyph)
                return;
            
            Talk();
        }

        public void Talk()
        {
            Talk(currentAiBehaviour.myWord);
        }

        public void Talk(Hieroglyph word)
        {
            if (word == null) return;

            _latestGlyph = word;

            talkBubble.SetExpectedSecond(false);
            talkBubble.UpdateView(word, null);
            DialogueSystem.Talking(this);
        }

        public string GetName()
        {
            return gameObject.name;
        }

        public void WaitingForSecondWord(bool state)
        {
        }

        public GameObject GetSource()
        {
            return gameObject;
        }

        public Hieroglyph GetLatestWord()
        {
            return _latestGlyph;
        }

        public float GetTalkRange()
        {
            return talkRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, talkRange);
        }
    }
}