using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Hierogplyph
    {
        /// <summary>
        /// The word, for us developers.
        /// </summary>
        public string Word;

        /// <summary>
        /// Slot for a player guess of the word
        /// </summary>
        public string PlayerGuess;

        /// <summary>
        /// Image displayed when talking
        /// </summary>
        public Sprite Image;

        /// <summary>
        /// Animation for the image
        /// </summary>
        public Sprite[] Animation;
    }
}