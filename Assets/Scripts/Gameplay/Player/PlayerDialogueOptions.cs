using System;
using System.Collections.Generic;
using Game.Core;
using Game.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Gameplay.Player
{
    public class PlayerDialogueOptions : MonoBehaviour, IHearing, ITalker
    {
        public List<Hieroglyph> knownGlyphs = new List<Hieroglyph>();

        public float talkRange = 3;
        public Hieroglyph latestTalk;

        public UnityEvent<PlayerDialogueOptions> onLearnNewGlyph = new UnityEvent<PlayerDialogueOptions>();

        public HieroglyphBubble talkBubble;

        private bool secondWord;
        private Hieroglyph previousWord;

        private void OnEnable()
        {
            DialogueSystem.AddIHearingToList(this);
        }

        private void OnDisable()
        {
            DialogueSystem.RemoveIHearingToList(this);
        }

        private void LearnNewGlyph(Hieroglyph hieroglyphic)
        {
            if (knownGlyphs.Contains(hieroglyphic) || hieroglyphic == null || hieroglyphic is LogicGlyph)
                return;
            knownGlyphs.Add(hieroglyphic);
            onLearnNewGlyph?.Invoke(this);
            //Debug.Log("Learned new Glyph");
        }

        public void Talk()
        {
            Talk(latestTalk);
        }

        public void Talk(Hieroglyph hieroglyphic)
        {
            if(hieroglyphic is LogicGlyph)
                return;
            latestTalk = hieroglyphic;

            DialogueSystem.Talking(this);
            
            if (talkBubble)
            {
                talkBubble.UpdateView(latestTalk, previousWord);
            }
            previousWord = latestTalk;
        }

        public string GetName()
        {
            return "Player";
        }

        public void WaitingForSecondWord(bool state)
        {
            secondWord = state;
            talkBubble.SetExpectedSecond(secondWord);
        }

        public Vector2 GetLocation()
        {
            return transform.position;
        }

        public void OnHearing(ITalker talker)
        {
            Hieroglyph talkerWord = talker.GetLatestWord();

            if (talkerWord is LogicGlyph logicGlyph)
            {
                
            }
            
            
            LearnNewGlyph(talkerWord);
        }

        public GameObject GetSource()
        {
            return gameObject;
        }

        public Hieroglyph GetLatestWord()
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
    }
}