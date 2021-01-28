using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class DialogueSystem
    {
        private static readonly List<IHearing> Listeners = new List<IHearing>();

        public static void AddIHearingToList(IHearing hearing)
        {
            if (Listeners.Contains(hearing)) return;
            Listeners.Add(hearing);
        }

        public static void RemoveIHearingToList(IHearing hearing)
        {
            if (Listeners.Contains(hearing))
                Listeners.Remove(hearing);
        }

        public static void Talking(ITalker talker)
        {
            foreach (IHearing listener in Listeners)
            {
                if (Vector2.Distance(listener.GetLocation(), talker.GetLocation()) < talker.GetTalkRange())
                {
                    listener.OnHearing(talker);
                }
            }
        }
    }

    public interface IHearing : ILocation
    {
        void OnHearing(ITalker talker);
    }

    public interface ITalker : ILocation
    {
        Hieroglyph GetLatestWord();
        float GetTalkRange();
    }

    public interface ILocation
    {
        Vector2 GetLocation();
    }
}