using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "LogicWord",menuName = "LogicGlyph")]
    public class LogicGlyph: Hieroglyph
    {
        public bool expectingMoreWords = true;
    }
}