using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerDialogueOptions : MonoBehaviour, IHearing, ITalker
    {
        public List<Hierogplyph> knownGlyphs = new List<Hierogplyph>();

        public float talkRange = 3;
        public Hierogplyph latestTalk;

        private void OnEnable()
        {
            DialogueSystem.AddIHearingToList(this);
        }

        private void OnDisable()
        {
            DialogueSystem.RemoveIHearingToList(this);
        }

        private void LearnNewGlyph(Hierogplyph hieroglyphic)
        {
            if (knownGlyphs.Contains(hieroglyphic))
                return;
            knownGlyphs.Add(hieroglyphic);
        }

        public void Talk(Hierogplyph hieroglyphic)
        {
            latestTalk = hieroglyphic;
            DialogueSystem.Talking(this);
        }

        public Vector2 GetLocation()
        {
            return transform.position;
        }

        public void OnHearing(ITalker talker)
        {
            LearnNewGlyph(talker.GetLatest());
        }

        public Hierogplyph GetLatest()
        {
            return latestTalk;
        }

        public float GetTalkRange()
        {
            return talkRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,talkRange);
        }
    }
}