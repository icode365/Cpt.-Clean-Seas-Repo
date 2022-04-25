using UnityEngine;
using System;

public enum SpawnableTypes
{
    Obstacles,
    Cardboardbox,
    Jar,
    Pizzabox,
    Bottle
}

#region ObjectPool

[Serializable]
public class PoolObject
{
    public SpawnableTypes spawnType;
    public GameObject prefab;
    public int poolCount;
}

#endregion

#region Referencing

[Serializable]
public class ReferenceItem
{
    public AllTags itemName;
    public string referenceString;
}

public enum AllTags
{
    Collectible,
    CollectionBag,
    Player,
    Obstacle,
    DisableSelf
}

#endregion

#region Audio Class + Enums

[Serializable]
public class AudioClips
{
    public AudioClipType clipType;
    public AudioClip clip;

    public AudioClips(AudioClipType _type, AudioClip _clip)
    {
        this.clipType = _type;
        this.clip = _clip;
    }
}

public enum AudioClipType
{
    buttonClickClip = 0,
    collectibleClip = 1,
    gameOverClip = 2,
    damageClip = 3,
    amibenceClip = 4,
    scoreClip = 5
}

#endregion