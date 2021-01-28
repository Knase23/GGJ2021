using System;
using System.Collections.Generic;
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
            if (knownGlyphs.Contains(hieroglyphic))
                return;
            
            knownGlyphs.Add(hieroglyphic);
            onLearnNewGlyph?.Invoke(this);
            Debug.Log("Learned new Glyph");
        }

        public void Talk()
        {
            Talk(latestTalk);
        }

        public void Talk(Hieroglyph hieroglyphic)
        {
            
            latestTalk = hieroglyphic;
            DialogueSystem.Talking(this);
        }

        public string GetName()
        {
            return "Player";
        }

        public Vector2 GetLocation()
        {
            return transform.position;
        }

        public void OnHearing(ITalker talker)
        {
            LearnNewGlyph(talker.GetLatestWord());
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