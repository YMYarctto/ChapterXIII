using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("启用Debug模式")]
    public bool DebugModel;
    private Dictionary<string,AudioClip> _Audios;
    private Dictionary<string,AudioSource> _AudioSources;

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
        _Audios ??= DataManager.instance.audio_data.AudioClipDict;
        _AudioSources ??=new();
    }
    AudioSource NewAudioSource(string url)
    {
        var obj = new GameObject("AudioSource");
        obj.transform.SetParent(transform);
        AudioSource audioSource=obj.AddComponent<AudioSource>();
        audioSource.loop=false;
        audioSource.playOnAwake=false;
        audioSource.mute=false;
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

    public void StopAudio(string url_source)
    {
        if(!_AudioSources.ContainsKey(url_source))
        {
            Debug.Log($"url: {url_source}, 音源不存在");
            return;
        }

        _AudioSources[url_source].Stop();
    }
}
