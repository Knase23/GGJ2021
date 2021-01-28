using UnityEngine;

namespace Game.Gameplay.AI
{
    public class AIDialogue : MonoBehaviour, IHearing, ITalker
    {
        private Hierogplyph _latestGlyph = null;
        public float talkRange = 10;
        
        public Vector2 GetLocation()
        {
            return transform.position;
        }

        public void OnHearing(ITalker talker)
        {
            //Check what the response should be based on our latestGlyph and what we heard
            
        }

        public void Talk()
        {
            
            _latestGlyph = null;
        }
        
        public Hierogplyph GetLatest()
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