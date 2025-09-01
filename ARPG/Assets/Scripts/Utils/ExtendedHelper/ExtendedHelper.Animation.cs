using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static partial class ExtendedHelper
{
    public static AnimationClip GetAnimationClip(this Animator animator, string clipName, int layerIndex = 0)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        var index = Array.FindIndex(clips, x => x.name == clipName);
        if (index == -1)
        {
            Debug.LogWarningFormat("Clip with name {0} not found in layer with index {1}", clipName, layerIndex);
            return null;
        }
        
        var ret = clips[index];
        return ret;
    }

    public static void RegisterAnimationEvent(this AnimationClip clip, string eventName, float time)
    {
        AnimationEvent animEvent = new AnimationEvent();
        animEvent.time = time;
        animEvent.functionName = eventName;
        clip.AddEvent(animEvent);
    }

    public static void UnregisterAnimationEvent(this AnimationClip clip, string eventName, float frame)
    {
        int index = Array.FindIndex(clip.events, evt => evt.time == frame && evt.functionName == eventName);
        if (index != -1)
        {
            List<AnimationEvent> evts = new List<AnimationEvent>();
            for (int i = 0; i < clip.events.Length; i++)
            {
                if (i == index)
                    continue;

                evts.Add(clip.events[i]);
            }

            clip.events = evts.ToArray();
        }

    }
}