using UnityEngine;
using System.Collections.Generic;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) {}

    public AnimationClipOverrides(IEnumerable<KeyValuePair<AnimationClip, AnimationClip>> ien) : base(ien)
    {
        foreach (var item in ien)
        {
            this.Add(item);
        }
    }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}

public static partial class CollectionExtensions
{
    public static TColl ToTypedCollection<TColl, T>(this IEnumerable<T> ien)
        where TColl : IList<T>, new()
    {
        TColl collection = new TColl();

        foreach (var item in ien)
        {
            collection.Add((T)item);
        }

        return collection;
    }
}