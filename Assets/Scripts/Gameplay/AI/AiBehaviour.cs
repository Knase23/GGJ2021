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
        public List<BehaviourChange> expectedResponse = new List<BehaviourChange>();

        // Ai knows how to respond
        public BehaviourChange unexpectedResponse = new BehaviourChange();

        [Serializable]
        public class BehaviourChange
        {
            public Glyph responseWord;
            public AiBehaviour nextBehaviour;
            public int onResponse = -1; // It is connected to unityEvents that are stored in AIDialogue
        }

        public BehaviourChange GetResponse(HieroGlyph word)
        {
            //Go through expectedResponse
            foreach (BehaviourChange behaviourChange in expectedResponse)
            {
                if (behaviourChange.responseWord == word)
                {
                    //Debug.Log("Expected Response");
                    return behaviourChange;
                }
            }
            //Debug.Log("No Expected Response found");
            return unexpectedResponse;
        }

    }
}