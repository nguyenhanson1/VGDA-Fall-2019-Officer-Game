using UnityEngine.Audio;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class Sound
{
    [Tooltip("Rename to be called in other scripts.")]
    public string name;                 // Custom name of element
    [Tooltip("Place audio file here")]
    public AudioClip clip;              // insert audio clip

    [Header("Volume and Tempo Controls")]
    [Tooltip("Volume manipulation")]
    [Range(0f, 1f)]                             // range of volume from 0-100%
    public float volume = 1f;                   // auto set to 100%
    [Tooltip("Tempo manipulation")]
    [Range(-3f, 3f)]                            // range of tempo of audio from -300% to 300%
    public float tempo = 1f;                    // auto set to 100%
    [Tooltip("Volume randomization")]
    [Range(-0.5f, 0.5f)]                        // range of -50% to 50% fluctuation
    public float RandomVolume;                  // randomizes sound volume
    [Tooltip("Tempo randomization")]
    [Range(-0.5f, 0.5f)]                        // range of -50% to 50% fluctuation
    public float RandomTempo;                   // randomizes sound speed
    [Tooltip("Check box for looping.")]
    public bool loop = false;                   // option for looping
    [Tooltip("Check box for muting audio.")]
    public bool mute = false;                   // option for muting

    /* still working on these parts, can use Google Resonance Audio package for now.
    [Header("Spatial Controls")]
    [Tooltip("Short for binuaral head-related transfer function.")]
    public bool HRTF = false;           // option for binuaral head-related transfer function (2D/3D spatialization)
    [Tooltip("Manipulate intensity for left-right awareness audio.")]
    [Range(0f, 1f)]                     // range of spatialization
    public float spatialBlender;        // 2D/3D spatial sourcing based on AudioManager transform

    [Range(0f, 360f)]                   // range of audio from 0-360 degrees of activation
    public float audioPanning = 360f;   // spreads audio to cover area in area of influence
    */

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;               // source placement
        UpdatedSource();
    }

    public void UpdatedSource()
    {
        source.clip = clip;                     // clip placement
        source.loop = loop;                     // loop activation
        source.mute = mute;                     // mute activation
        source.volume = volume;                 // volume modifier

        /*
        source.spatialize = HRTF;               // spatializer activation
        source.spatialBlend = spatialBlender;   // 2D/3D spatializer modifier
        source.spread = audioPanning;           // spread modifier
        */
    }

    public void Play()
    {
        if (source.mute)
        {
            source.volume = volume * 0f;        // volume control for mute
        }

        else if (!source.mute)
        {
            source.volume = volume * (1 + Random.Range(-RandomVolume / 2f, RandomVolume / 2f));     // volume control with random volume fluctuation
            source.pitch = tempo * (1 + Random.Range(-RandomTempo / 2f, RandomTempo / 2f));         // tempo control with random tempo fluctuation 
        }

        source.Play();              // plays sound
    }

    public void Stop()
    {
        source.Stop();              // stops sound
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    // identifies AudioManager

    [Tooltip("Store all audio here.")]
    [SerializeField] Sound[] Music;         // instantiates music array
    [SerializeField] Sound[] Sounds;        // instantiates Sound array

    void Awake()
    {
        // For editor's log
        if (instance != null)
        {
            // Debug.LogError("More than one AudioManager in scene.");
            if (instance != this)               // if audiomanager is not same as old audiomanager
            {
                Destroy(this.gameObject);       // destroy the new one
            }
        }
        else
        {
            instance = this;                    // otherwise
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        for (int i = 0; i < Sounds.Length; i++)                                         // any audible sound of increasing order
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + Sounds[i].name);       // creates temp gameobject of music
            _go.transform.SetParent(this.transform);                                    // as a child
            Sounds[i].SetSource(_go.AddComponent<AudioSource>());                       // and of specific sound file played
        }

        for (int i = 0; i < Music.Length; i++)                                          // any audible sound of increasing order
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + Music[i].name);        // creates temp gameobject of music
            _go.transform.SetParent(this.transform);                                    // as a child
            Music[i].SetSource(_go.AddComponent<AudioSource>());                        // and of specific music file played
        }

        PlaySound("Title_BGM");                     // plays this on start of play
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < Sounds.Length; i++)     // any audible sound of increasing order
        {
            if (Sounds[i].name == _name)
            {
                Sounds[i].Play();                   // plays sound
                return;
            }
        }

        for (int i = 0; i < Music.Length; i++)      // any audible sound of increasing order
        {
            if (Music[i].name == _name)
            {
                Music[i].Play();                    // plays sound
                return;
            }
        }
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < Sounds.Length; i++)     // any audible sound of increasing order
        {
            if (Sounds[i].name == _name)
            {
                Sounds[i].Stop();                   // stops sound
                return;
            }
        }

        for (int i = 0; i < Music.Length; i++)      // any audible sound of increasing order
        {
            if (Music[i].name == _name)
            {
                Music[i].Stop();                    // stops sound
                return;                             // loops yo
            }
        }

        // No Sound Warning
        Debug.LogWarning("AudioManager: Sound ~" + _name + "~ not found in library.");
    }
}