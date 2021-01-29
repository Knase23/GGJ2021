using System.Collections.Generic;
using Game.Core;
using Game.Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class DisplayPlayerLearnedGlyphs : MonoBehaviour
    {
        public GameObject glyphButtonPrefab;

        private readonly Dictionary<HieroGlyph, GlyphButton> _displayedGlyphs = new Dictionary<HieroGlyph, GlyphButton>();

        public void UpdateDisplayedGlyphs(PlayerDialogueOptions playerDialogueOptions)
        {
            if (_displayedGlyphs.Count < playerDialogueOptions.knownGlyphs.Count)
            {
                foreach (HieroGlyph hieroglyph in playerDialogueOptions.knownGlyphs)
                {
                    if (_displayedGlyphs.ContainsKey(hieroglyph)) continue;
                
                    _displayedGlyphs.Add(hieroglyph,CreateButton(hieroglyph,playerDialogueOptions));
                }
            }
        }
        private GlyphButton CreateButton(HieroGlyph glyph, ITalker talker)
        {
            GlyphButton glyphButton = new GlyphButton() {HieroGlyph = glyph, Talker = talker};

            GameObject buttonObject = Instantiate(glyphButtonPrefab, Vector3.zero, Quaternion.identity, transform);
            buttonObject.GetComponent<Image>().sprite = glyph.Image;
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(()=> talker.Talk(glyph));
            glyphButton.References = buttonObject;
            return glyphButton;
        }

        private class GlyphButton
        {
            public HieroGlyph HieroGlyph;
            public ITalker Talker;
            public GameObject References;

        }
    }
}