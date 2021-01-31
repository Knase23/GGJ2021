using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using Game.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Gameplay.AI
{
    public class AIDialogue : MonoBehaviour, IHearing, ITalker
    {
        public Glyph _latestGlyph = null;
        public Glyph _latestPrevGlyph = null;
        private bool _secondWord = false;
        private bool _AlredySaidSecondWord = false;
        public float talkRange = 10;

        public SpeechSFX speech;

        public HieroglyphBubble talkBubble;
        public ExpressionBubble expressionBubble;

        public AiBehaviour firstBehaviour;

        public List<GameObject> interestedSources = new List<GameObject>();

        [FormerlySerializedAs("onAiTalkComplete")] public List<NameToActions> AiActions = new List<NameToActions>();

        public AiBehaviour currentAiBehaviour;

        private event Action OnSpeechComplete;


        [Serializable]
        public class NameToActions
        {
            public string name;
            public UnityEvent actions = new UnityEvent();
        }


        private void Start()
        {
            currentAiBehaviour = firstBehaviour;
            talkBubble.OnBubbleEnd += OnAiTalkComplete;
        }

        public void OnAiTalkComplete()
        {
            if (talkBubble.gameObject.activeInHierarchy == false)
            {
                OnSpeechComplete?.Invoke();
                OnSpeechComplete = null;
            }

            foreach (string action in currentAiBehaviour.onAiTalkComplete)
            {
                GoThroughActionList(AiActions, action);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void GoThroughActionList(IEnumerable<NameToActions> actionsList, string action)
        {
            bool foundAction = false;
            foreach (NameToActions nameToActions in actionsList)
            {
                if (nameToActions.name == action)
                {
                    nameToActions.actions?.Invoke();
                    Debug.Log($"AI called: {action} action", gameObject);
                    foundAction = true;
                    break;
                }
            }

            if (foundAction == false)
                Debug.Log($"AI don't have a Action called: {action}", gameObject);
        }


        private void OnEnable()
        {
            DialogueSystem.AddIHearingToList(this);
        }

        private void OnDisable()
        {
            DialogueSystem.RemoveIHearingToList(this);
        }

        public Vector2 GetLocation()
        {
            return transform.position;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void OnHearing(ITalker talker)
        {
            //Check if the Talker is someone i want to listen to!
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

            //Process the word
            Glyph glyph = talker.GetLatestGlyph(); // Get what it said

            if (glyph is HieroGlyph hieroGlyph && currentAiBehaviour != null)
            {
                AiBehaviour.BehaviourChange
                    changeInBehaviour = currentAiBehaviour.GetResponse(hieroGlyph); // Check how we react
                currentAiBehaviour = changeInBehaviour.nextBehaviour; // Change our behavior to next one!

                //Go through potential actions the
                foreach (string action in currentAiBehaviour.onHearingActions)
                {
                    GoThroughActionList(AiActions, action);
                }

                //If AI:s Glyphs are LogicGlyphs they should be sent Directly
                if (currentAiBehaviour.firstGlyph is LogicGlyph)
                {
                    Talk(currentAiBehaviour.firstGlyph);
                }
                if (currentAiBehaviour.secondGlyph is LogicGlyph)
                {
                    Talk(currentAiBehaviour.secondGlyph);
                }

                talker.SubscribeToOnSpeechComplete(OnTalkerSpeechComplete); //Subscribe 
            }

            if (glyph is LogicGlyph logicGlyph)
            {
                //Send second word
                WaitingForSecondWord(true);
                Talk(currentAiBehaviour.secondGlyph);
            }
            else
            {
                WaitingForSecondWord(false);
            }
        }

        public void OnTalkerSpeechComplete()
        {
            if (_latestPrevGlyph == null)
            {
                //Debug.Log($"{GetName()}: After Talker Complete Talk", gameObject);
                Talk();
            }

            foreach (string action in currentAiBehaviour.onTalkerCompleteActions)
            {
                GoThroughActionList(AiActions, action);
            }
        }

        public void Talk()
        {
            Talk(currentAiBehaviour.firstGlyph);
            //Invoke the second one after a few seconds
            Invoke(nameof(TalkSecondGlyph),0.5f);
        }

        private void TalkSecondGlyph()
        {
            if (_AlredySaidSecondWord)
            {
                _AlredySaidSecondWord = false;
                return;
            }
            
            if(currentAiBehaviour.secondGlyph is HieroGlyph)
                WaitingForSecondWord(true);

            Talk(currentAiBehaviour.secondGlyph);
        }

        public void Talk(Glyph glyph)
        {
            if (glyph == null) return;

            if (glyph is HieroGlyph hieroglyph)
            {
                _latestGlyph = glyph;
                if (_latestPrevGlyph is HieroGlyph latestPrevGlyph)
                {
                    _AlredySaidSecondWord = true;
                    talkBubble.ShowWords(latestPrevGlyph,hieroglyph);
                    ResetSecondWord();
                }
                else
                {
                    talkBubble.ShowWords(hieroglyph);
                }

                DialogueSystem.Talking(this);
                if (speech != null) speech.Speak(3,6);
            }

            if (glyph is ExpressionGlyph expressionGlyph)
            {
                _latestGlyph = glyph;
                expressionBubble.DisplayExpression(expressionGlyph);
                if(speech != null) speech.Speak(1,1);
            }

            if (glyph is LogicGlyph logicGlyph)
            {
                _latestGlyph = glyph;
                DialogueSystem.Talking(this);
            }
        }

        public string GetName()
        {
            return gameObject.name;
        }

        public void WaitingForSecondWord(bool state)
        {
            _secondWord = state;
            _latestPrevGlyph = state ? _latestGlyph : null;
            talkBubble.SetExpectedSecond(_secondWord);
        }

        private void ResetSecondWord()
        {
            _secondWord = false;
            _latestPrevGlyph = null;
            talkBubble.SetExpectedSecond(_secondWord);
        }

        public void SubscribeToOnSpeechComplete(Action action)
        {
            OnSpeechComplete += action;
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

        public void SetCurrentBehaviour(AiBehaviour newBehaviour)
        {
            currentAiBehaviour = newBehaviour;
        }

        public void AddInterestedSource(GameObject gameObject)
        {
            if (interestedSources.Contains(gameObject) == false)
                interestedSources.Add(gameObject);
        }

        public void RemoveInterestedSource(GameObject gameObject)
        {
            if (interestedSources.Contains(gameObject))
                interestedSources.Remove(gameObject);
        }
    }
}