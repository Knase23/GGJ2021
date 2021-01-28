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

        public AiBehaviour currentAiBehaviour;

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
            Debug.Log($"Heard {talker.GetName()} say: {talker.GetLatestWord()}");
            
            Hieroglyph talkerWord = talker.GetLatestWord(); // Get what it said
            AiBehaviour.BehaviourChange changeInBehaviour = currentAiBehaviour.GetResponse(talkerWord); // Check how we react
            
            if(changeInBehaviour.onResponse > -1 && changeInBehaviour.onResponse < actions.Count) // Check if their is a action relate
                actions[changeInBehaviour.onResponse]?.Invoke();

            currentAiBehaviour = changeInBehaviour.nextBehaviour;// Change our behavoior to next one!
            Talk();
        }

        public void Talk()
        {
            Talk(currentAiBehaviour.myWord);
        }
        
        public void Talk(Hieroglyph word)
        {
            _latestGlyph = word;
            DialogueSystem.Talking(this);
        }

        public string GetName()
        {
            return gameObject.name;
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