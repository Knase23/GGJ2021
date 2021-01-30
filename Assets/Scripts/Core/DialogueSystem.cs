using System;
using System.Collections.Generic;
using Game.Core;
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
            //Debug.Log($"{talker.GetName()} is saying:{talker.GetLatestWord()}");
            foreach (IHearing listener in Listeners)
            {
                if(talker == listener) continue;

                float distance = Vector2.Distance(listener.GetLocation(), talker.GetLocation());
                
                if (distance < talker.GetTalkRange() + 2)
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
        GameObject GetSource();
        Glyph GetLatestGlyph();
        float GetTalkRange();
        void Talk();
        void Talk(Glyph glyph, Glyph glyph2 = null);
        string GetName();
        void WaitingForSecondWord(bool state);

        void SubscribeToOnSpeechComplete(Action action);
    }

    public interface ILocation
    {
        Vector2 GetLocation();
    }
}