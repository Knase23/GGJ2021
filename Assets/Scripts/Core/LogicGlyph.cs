using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "LogicWord",menuName = "Glyph/LogicGlyph")]
    public class LogicGlyph: Glyph
    {
        public bool expectingMoreWords = true;
    }
}