using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager sInstance;

    [SerializeField] SFXClip[] myAudioClips;

    Dictionary<EClipName, SFXClip> mySoundClips;

    void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        InitializeClipDictionary();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeClipDictionary()
    {
        mySoundClips = new Dictionary<EClipName, SFXClip>();
        foreach(SFXClip clip in myAudioClips) 
        {
            mySoundClips.Add(clip.ClipType, clip);
        }
    }

    public bool GetAudioClip(EClipName aName, out AudioClip anAudioClip)
    {
        if (mySoundClips.TryGetValue(aName, out SFXClip aSound))
        {
            anAudioClip = aSound.Clip;
            return true;
        }
        anAudioClip = null;
        return false;
    }
}

public enum EClipName
{
    Buy,
    Duck1,
    Duck2,
    DuckDeath,
    InteractUI,
    LaserPistol,
    LaserSniper,
    LaserWall,
    MuscleDuck,
    Plasmathrower,
    SpaceManDrop
}

[Serializable]
public struct SFXClip
{
    public AudioClip Clip;
    public EClipName ClipType;
}
