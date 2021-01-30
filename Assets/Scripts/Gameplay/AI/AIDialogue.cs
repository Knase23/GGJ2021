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
        private Glyph _latestGlyph = null;
        public float talkRange = 10;

        public SpeechSFX speech;

        public HieroglyphBubble talkBubble;
        public ExpressionBubble ExpressionBubble;

        public AiBehaviour FirstBehaviour;

        public List<UnityEvent> actions = new List<UnityEvent>();

        public AiBehaviour currentAiBehaviour;

        public GameObject[] interestedSources;


        private void Start()
        {
            currentAiBehaviour = FirstBehaviour;
            speech = GetComponent<SpeechSFX>();
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
            Glyph glyph = talker.GetLatestGlyph(); // Get what it said

            bool isInterested = false;
            foreach (GameObject interestedSource in interestedSources)
            {
                if (talker.GetSource() == interestedSource)
                {
                    isInterested = true;
                    break;
                }
            }

            if (isInterested == false) return;

            HieroGlyph hieroGlyph = null;

            hieroGlyph = glyph as HieroGlyph;

            AiBehaviour.BehaviourChange
                changeInBehaviour = currentAiBehaviour.GetResponse(hieroGlyph); // Check how we react

            if (changeInBehaviour.onResponse > -1 && changeInBehaviour.onResponse < actions.Count
            ) // Check if their is a action relate
                actions[changeInBehaviour.onResponse]?.Invoke();

            currentAiBehaviour = changeInBehaviour.nextBehaviour; // Change our behavior to next one!
            
            
            if (currentAiBehaviour.firstGlyph is LogicGlyph)
            {
                Talk();
            }
            if(currentAiBehaviour.firstGlyph is HieroGlyph)
            {
                Invoke(nameof(Talk), 0.5f);
            }
            if (currentAiBehaviour.firstGlyph is ExpressionGlyph expressionGlyph)
            {
                ExpressionBubble.DisplayExpression(expressionGlyph);
            }
            
        }

        public void Talk()
        {
            Talk(currentAiBehaviour.firstGlyph,currentAiBehaviour.secondGlyph);
        }

        public void Talk(Glyph glyph,Glyph glyph2 = null)
        {
            
            if(speech != null) speech.Speak();
            
            if (glyph == null) return;
            _latestGlyph = glyph;
            if (glyph is HieroGlyph hieroglyph)
            {
                HieroGlyph hieroGlyph2 = glyph2 as HieroGlyph;

                talkBubble.SetExpectedSecond(hieroGlyph2 != null);
                talkBubble.ShowWords(hieroglyph, hieroGlyph2);
                DialogueSystem.Talking(this);
            }

            if (glyph is ExpressionGlyph expressionGlyph)
            {
                ExpressionBubble.DisplayExpression(expressionGlyph);
            }
                
            if (glyph is LogicGlyph logicGlyph)
            {
                DialogueSystem.Talking(this);
            }
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

        public Glyph GetLatestGlyph()
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