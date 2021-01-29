using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "Word",menuName = "Hieroglyph")]
    public class HieroGlyph: Glyph
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
        /// Image displayed when talking, Set by some Manager of HieroglyphManager
        /// </summary>
        public Sprite Image; 

        /// <summary>
        /// Animation for the image, Set by some Manager of Images
        /// </summary>
        public Sprite talkImage;
    }
}