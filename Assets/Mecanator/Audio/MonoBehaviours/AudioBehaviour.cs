#if AlphaECS
using AlphaECS.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : ComponentBehaviour
{
    public string AudioPrefix;
    public string AudioSuffix;

    public void PlayAudio(AnimationEvent animationEvent)
    {
        EventSystem.Publish(new AudioEvent()
        {
            EventName = AudioPrefix + animationEvent.stringParameter + AudioSuffix,
            Options = animationEvent.objectReferenceParameter as AudioOptionsProxy != null ? (animationEvent.objectReferenceParameter as AudioOptionsProxy).AudioOptions : new AudioOptions(),
            Target = this.GetComponent<Animator>(),
        });
    }
}
#endif