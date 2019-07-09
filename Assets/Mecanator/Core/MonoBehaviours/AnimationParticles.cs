using UnityEngine;
using System;
using System.Collections.Generic;

public class AnimationParticles : MonoBehaviour
{
    public ParticleEventsTable ParticleEvents = new ParticleEventsTable();

    public void PlayParticles(string particlesKey)
    {
        List<ParticleSystem> particleSystems;
        if (!ParticleEvents.TryGetValue(particlesKey, out particleSystems))
            return;

        particleSystems.ForEach(p =>
        {
            p.gameObject.SetActive(true);
            p.Stop();
            p.Play();
        });
    }

    public void StopParticles(string particlesKey)
    {
        List<ParticleSystem> particleSystems;
        if (!ParticleEvents.TryGetValue(particlesKey, out particleSystems))
            return;

        particleSystems.ForEach(p => p.Stop());
    }
}

[Serializable]
public class ParticleEventsTable : SerializableDictionary<string, List<ParticleSystem>, ParticleSystemList> { }

[Serializable]
public class ParticleSystemList : SerializableDictionary.Storage<List<ParticleSystem>> { }

//[Serializable]
//public class ParticleEventsTable : SerializableDictionary<string, ParticleSystemList>
//{
//    //public string ParticleName;
//    //public List<ParticleSystem> ParticleSystems;
//}

//[Serializable]
//public class ParticleSystemList
//{
//    public List<ParticleSystem> ParticleSystems = new List<ParticleSystem>();
//}