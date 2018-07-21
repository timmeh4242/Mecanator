#if AlphaECS
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AlphaECS.Unity;

public class ParticlesBehaviour : ComponentBehaviour
{
    public List<ParticleEvent> ParticleEvents = new List<ParticleEvent>();

    public void PlayParticles(string particles)
    {
        var particle = ParticleEvents.ToList().FirstOrDefault(n => n.ParticleName == particles);
        if (particle != null)
        {
			particle.ParticleSystems.ForEach(p => 
			{
				p.gameObject.SetActive(true);
				p.Stop();
				p.Play();
			});
        }
    }

    public void StopParticles(string particles)
    {
        var particle = ParticleEvents.ToList().FirstOrDefault(n => n.ParticleName == particles);
        if (particle != null)
        {
            particle.ParticleSystems.ForEach(p => p.Stop());
        }
    }

	public void PublishEvent(AnimationEvent animationEvent)
	{
		var animator = this.GetComponent<Animator> ();
		EventSystem.Publish (new AnimationEventProxy (animator, animationEvent));
	}
}

[Serializable]
public class ParticleEvent
{
    public string ParticleName;
    public List<ParticleSystem> ParticleSystems;
}
#endif