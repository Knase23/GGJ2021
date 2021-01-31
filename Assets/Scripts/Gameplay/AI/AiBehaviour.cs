using System;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay.AI
{
    [CreateAssetMenu(fileName = "AiBehaviour", menuName = "AI", order = 0)]
    public class AiBehaviour : ScriptableObject
    {
        public Glyph firstGlyph;
        public Glyph secondGlyph;

        [Tooltip("Runs Actions, when deciding right when changing Behaviour")]
        public List<string> onHearingActions = new List<string>();
        [Tooltip("Runs Actions, after Talkers bubble is gone")]
        public List<string> onTalkerCompleteActions = new List<string>();
        [Tooltip("Runs Actions, after the AIs Talkers bubble is gone")]
        public List<string> onAiTalkComplete = new List<string>();
        
        
        // Ai knows how to respond
        public List<BehaviourChange> expectedResponse = new List<BehaviourChange>();
        public BehaviourChange unexpectedResponse = new BehaviourChange();

        [Serializable]
        public class BehaviourChange
        {
            public Glyph responseWord;
            public AiBehaviour nextBehaviour;
        }

        public BehaviourChange GetResponse(HieroGlyph word)
        {

            foreach (BehaviourChange behaviourChange in expectedResponse)
            {
                if (behaviourChange.responseWord == word)
                {
                    return behaviourChange;
                }
            }
            return unexpectedResponse;
        }

    }
}