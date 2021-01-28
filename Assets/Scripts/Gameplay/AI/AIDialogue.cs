using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.AI
{
    public class AIDialogue : MonoBehaviour, IHearing, ITalker
    {
        private Hieroglyph _latestGlyph = null;
        public float talkRange = 10;


        public AiBehaviour FirstBehaviour;

        public List<UnityEvent> actions = new List<UnityEvent>();

        private AiBehaviour currentAiBehaviour;

        private void Start()
        {
            currentAiBehaviour = FirstBehaviour;
        }

        public Vector2 GetLocation()
        {
            return transform.position;
        }

        public void OnHearing(ITalker talker)
        {
            Hieroglyph talkerWord = talker.GetLatestWord();
            AiBehaviour.BehaviourChange changeInBehaviour = currentAiBehaviour.GetResponse(talkerWord);
            if(changeInBehaviour.OnResponse > 0 && changeInBehaviour.OnResponse < actions.Count)
                actions[changeInBehaviour.OnResponse]?.Invoke();

            currentAiBehaviour = changeInBehaviour.nextBehaviour;
        }

        public void Talk()
        {
            Talk(currentAiBehaviour.myWord);
        }
        
        public void Talk(Hieroglyph word)
        {
            _latestGlyph = word;
            DialogueSystem.Talking(this);
            Debug.Log($"{name} says {_latestGlyph.Word}");
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