using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [CreateAssetMenu(fileName = "DialogueOption")]
    public class DialougeOption: ScriptableObject
    {
        public Hierogplyph Glyph;

        public LeadingTo[] Responses;
        
        public class LeadingTo
        {
            public Hierogplyph RecivedGlyph;
            public Hierogplyph ResponseGlyph;
            public UnityEvent OnResponse = new UnityEvent();
        }
        
    }
}