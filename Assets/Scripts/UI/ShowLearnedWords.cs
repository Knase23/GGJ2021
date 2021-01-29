using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.Core;
using Game.Gameplay.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShowLearnedWords : MonoBehaviour
{
    public GameObject prefabForGlyphsButton;

    private int _previousCount = -1;
    private List<GlyphButton> _displayedButtons = new List<GlyphButton>();

    public void UpdateViewedWords(PlayerDialogueOptions playerDialogueOptions)
    {
        if (playerDialogueOptions.knownGlyphs.Count > _previousCount)
        {
            foreach (HieroGlyph hieroglyph in playerDialogueOptions.knownGlyphs)
            {
                //Check if their is any object in the list that has that hieroglyph
                if (_displayedButtons.Any(o => o.refrence == hieroglyph))
                {
                    continue;
                }

                _displayedButtons.Add(CreateButton(hieroglyph, playerDialogueOptions));
            }
            _previousCount = playerDialogueOptions.knownGlyphs.Count;
        }
    }

    private GlyphButton CreateButton(HieroGlyph reference,ITalker talker)
    {
        GlyphButton newButton = new GlyphButton {refrence = reference, talker = talker};
        GameObject realButton = Instantiate(prefabForGlyphsButton,Vector3.zero,Quaternion.identity,transform);
        newButton.buttonObject = realButton;
        //Setup so button displays right object.
        // And does the right thing when pressed
        Button button = realButton.GetComponent<Button>();
        button.onClick.AddListener(() => newButton.talker.Talk(newButton.refrence));
        Image image = realButton.GetComponent<Image>();
        image.sprite = newButton.refrence.Image;
        return newButton;
    }
    
    
    public class GlyphButton
    {
        public HieroGlyph refrence;
        public ITalker talker;
        public GameObject buttonObject;
    }
}