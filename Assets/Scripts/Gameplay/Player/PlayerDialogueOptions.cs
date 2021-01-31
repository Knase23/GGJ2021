using System;
using System.Collections.Generic;
using Game.Core;
using Game.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Gameplay.Player
{
    public class PlayerDialogueOptions : MonoBehaviour, IHearing, ITalker
    {
        public List<HieroGlyph> knownGlyphs = new List<HieroGlyph>();

        public SpeechSFX speech;


        private LearnedGlyphPreview previewGlyph;

        public InputActionReference CheatForLearningGlyphsActionReference;

        public float talkRange = 3;
        public HieroGlyph latestTalk;

        public UnityEvent<PlayerDialogueOptions> onLearnNewGlyph = new UnityEvent<PlayerDialogueOptions>();

        public HieroglyphBubble talkBubble;


        public List<GameObject> interestedSources = new List<GameObject>();
        private event Action OnSpeechComplete;


        private bool secondWord;
        private HieroGlyph previousWord;

        private void Start()
        {
            previewGlyph = FindObjectOfType<LearnedGlyphPreview>();
            CheatForLearningGlyphsActionReference.action.performed += context => LearnAllGlyphs();
            talkBubble.OnBubbleEnd += SpeeachCompleteCheck;
        }

        private void OnEnable()
        {
            DialogueSystem.AddIHearingToList(this);
            CheatForLearningGlyphsActionReference.action.Enable();
        }

        private void OnDisable()
        {
            DialogueSystem.RemoveIHearingToList(this);
            CheatForLearningGlyphsActionReference.action.Disable();
        }

        private void LearnNewGlyph(HieroGlyph hieroglyphic)
        {
            if (knownGlyphs.Contains(hieroglyphic))
                return;

            knownGlyphs.Add(hieroglyphic);
            previewGlyph?.PreviewHieroglyph(hieroglyphic);
            onLearnNewGlyph?.Invoke(this);
        }

        public void Talk()
        {
            Talk(latestTalk);
        }

        public void Talk(Glyph glyph)
        {
            if (glyph is HieroGlyph hieroglyph)
            {
                latestTalk = hieroglyph;
                if (previousWord)
                {
                    talkBubble.ShowWords(previousWord, latestTalk);
                    ResetSecondWord();
                }
                else
                {
                    WaitingForSecondWord(false);
                    talkBubble.ShowWords(latestTalk);
                }

                DialogueSystem.Talking(this);
                if (speech != null) speech.Speak();
            }
        }

        public string GetName()
        {
            return "Player";
        }

        public void WaitingForSecondWord(bool state)
        {
            secondWord = state;
            previousWord = state ? latestTalk : null;
            talkBubble.SetExpectedSecond(secondWord);
        }


        public void SubscribeToOnSpeechComplete(Action action)
        {
            OnSpeechComplete += action;
        }

        public Vector2 GetLocation()
        {
            return transform.position;
        }

        private void SetupSecondWord()
        {
            talkBubble.BubbleEnd();
            talkBubble.SetSecondWordAnimation(true);
            talkBubble.OnBubbleStartComplete -= SetupSecondWord;
        }

        public void OnHearing(ITalker talker)
        {
            Glyph glyph = talker.GetLatestGlyph();

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

            if (glyph is LogicGlyph logicGlyph)
            {
                //Do Logic with it!
                WaitingForSecondWord(true);
                talkBubble.OnBubbleStartComplete += SetupSecondWord;
            }
            else
            {
                //Reset the logic that needs to be reset!
                WaitingForSecondWord(false);
            }

            if (glyph is HieroGlyph hieroglyph)
            {
                LearnNewGlyph(hieroglyph);
            }
        }

       

        public void AddInterestedSource(GameObject source)
        {
            if (interestedSources.Contains(source)) return;
            interestedSources.Add(source);
        }

        public void RemoveInterestedSource(GameObject source)
        {
            if (interestedSources.Contains(source))
                interestedSources.Remove(source);
        }

        public void SpeeachCompleteCheck()
        {
            if (talkBubble.gameObject.activeInHierarchy == false)
            {
                OnSpeechComplete?.Invoke();
                OnSpeechComplete = null;
            }
        }

        private void ResetSecondWord()
        {
            secondWord = false;
            previousWord = null;
            talkBubble.SetExpectedSecond(secondWord);
        }

        public GameObject GetSource()
        {
            return gameObject;
        }

        public Glyph GetLatestGlyph()
        {
            return latestTalk;
        }

        public float GetTalkRange()
        {
            return talkRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, talkRange);
        }

        private void LearnAllGlyphs()
        {
            HieroGlyph[] glyphs = Resources.LoadAll<HieroGlyph>("HieroGlyph\\");
            foreach (HieroGlyph hieroGlyph in glyphs)
            {
                LearnNewGlyph(hieroGlyph);
            }
        }
    }
}