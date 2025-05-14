using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("启用Debug模式")]
    public bool DebugModel;

    public float AudioVolume
    {
        get=> _AudioVolume;
        set
        {
            _AudioVolume=value;
            if (_AudioSources != null)
            {
                foreach (var source in _AudioSources.Values)
                {
                    source.volume = value;
                }
            }
        }
    }
    public float MusicVolume
    {
        get => _MusicVolume;
        set
        {
            _MusicVolume = value;
            _MusicSource.volume = value;
        }
    }

    private float _AudioVolume;
    private float _MusicVolume;
    private Dictionary<string,AudioClip> _Audios;
    private Dictionary<string,AudioSource> _AudioSources;
    private AudioSource _MusicSource;

    private static AudioManager _AudioManager;
    public static AudioManager instance
    {
        get
        {
            if (!_AudioManager)
            {
                _AudioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;
                if (!_AudioManager)
                {
                    return null;
                }
            }
            return _AudioManager;
        }
    }
    public void Init()
    {
        _AudioVolume=DataManager.instance.setting_data.AudioVolume;
        _MusicVolume=DataManager.instance.setting_data.MusicVolume;
        _Audios ??= DataManager.instance.audio_data.AudioClipDict;
        _AudioSources ??=new();
        InitMusicSource();
    }
    void InitMusicSource()
    {
        if (_MusicSource == null)
        {
            var obj = new GameObject("MusicSource");
            obj.transform.SetParent(transform);
            _MusicSource = obj.AddComponent<AudioSource>();
            _MusicSource.loop = true;
            _MusicSource.playOnAwake = false;
            _MusicSource.mute = false;
            _MusicSource.volume = _MusicVolume;
        }
    }
    AudioSource NewAudioSource(string url)
    {
        var obj = new GameObject($"AudioSource_{url}");
        obj.transform.SetParent(transform);
        AudioSource audioSource=obj.AddComponent<AudioSource>();
        audioSource.loop=false;
        audioSource.playOnAwake=false;
        audioSource.mute=false;
        audioSource.volume=_AudioVolume;
        _AudioSources.Add(url,audioSource);
        return audioSource;
    }

    public void PlayAudio(string url_source,string url_clip)
    {
        if(!_Audios.ContainsKey(url_clip))
        {
            Debug.Log($"url: {url_clip}, 音效不存在");
            return;
        }

        AudioSource source;
        if(!_AudioSources.TryGetValue(url_source,out source))
        {
            source=NewAudioSource(url_source);
        }
        source.clip=_Audios[url_clip];
        source.Play();
    }

    public void UnPauseAudio()
    {
        foreach (var source in _AudioSources.Values)
        {
            source.UnPause();
        }
    }

    public void PauseAudio()
    {
        foreach (var source in _AudioSources.Values)
        {
            source.Pause();
        }
    }

    public void StopAudio()
    {
        foreach (var source in _AudioSources.Values)
        {
            source.Stop();
        }
    }

    public void StopAudio(string url_source)
    {
        if(!_AudioSources.ContainsKey(url_source))
        {
            Debug.Log($"url: {url_source}, 音源不存在");
            return;
        }

        _AudioSources[url_source].Stop();
    }

    public AudioManager PlayMusic(string url)
    {
        if(!_Audios.ContainsKey(url))
        {
            Debug.Log($"url: {url}, 音乐不存在");
            return instance;
        }
        _MusicSource.clip=_Audios[url];
        _MusicSource.Stop();
        StartCoroutine(EPlayMusic(url,0.5f));
        return instance;
    }

    public AudioManager ForcePlayMusic(string url)
    {
        if(!_Audios.ContainsKey(url))
        {
            Debug.Log($"url: {url}, 音乐不存在");
            return instance;
        }
        _MusicSource.clip=_Audios[url];
        _MusicSource.Play();
        return instance;
    }

    public void Next(string url)
    {
        if(!_Audios.ContainsKey(url))
        {
            Debug.Log($"url: {url}, 音乐不存在");
            return;
        }
        StartCoroutine(EPlayMusic(url,_MusicSource.clip.length));
    }

    IEnumerator EPlayMusic(string url,float delay)
    { 
        yield return new WaitForSecondsRealtime(delay+0.5f);
        _MusicSource.clip=_Audios[url];
        _MusicSource.Play();
    }

    public void StopMusic()
    {
        _MusicSource.Stop();
    }
}
