using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ExpressionBubble : Bubble
    {
        public void DisplayExpression(ExpressionGlyph expressionGlyph)
        {
            BubbleStartEffect();
            firstRenderer.sprite = expressionGlyph.image;
            BubbleEnd();
        }

    }
}