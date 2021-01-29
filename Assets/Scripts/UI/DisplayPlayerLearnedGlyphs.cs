using System.Collections.Generic;
using Game.Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class DisplayPlayerLearnedGlyphs : MonoBehaviour
    {
        public GameObject glyphButtonPrefab;

        private readonly Dictionary<Hieroglyph, GlyphButton> _displayedGlyphs = new Dictionary<Hieroglyph, GlyphButton>();

        public void UpdateDisplayedGlyphs(PlayerDialogueOptions playerDialogueOptions)
        {
            if (_displayedGlyphs.Count < playerDialogueOptions.knownGlyphs.Count)
            {
                foreach (Hieroglyph hieroglyph in playerDialogueOptions.knownGlyphs)
                {
                    if (_displayedGlyphs.ContainsKey(hieroglyph)) continue;
                
                    _displayedGlyphs.Add(hieroglyph,CreateButton(hieroglyph,playerDialogueOptions));
                }
            }
        }
        private GlyphButton CreateButton(Hieroglyph glyph, ITalker talker)
        {
            GlyphButton glyphButton = new GlyphButton() {Hieroglyph = glyph, Talker = talker};

            GameObject buttonObject = Instantiate(glyphButtonPrefab, Vector3.zero, Quaternion.identity, transform);
            buttonObject.GetComponent<Image>().sprite = glyph.Image;
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(()=> talker.Talk(glyph));
            glyphButton.References = buttonObject;
            return glyphButton;
        }

        private class GlyphButton
        {
            public Hieroglyph Hieroglyph;
            public ITalker Talker;
            public GameObject References;

        }
    }
}