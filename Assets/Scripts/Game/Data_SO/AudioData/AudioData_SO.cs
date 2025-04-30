using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData_SO", menuName = "Data/Audio/AudioData_SO")]
public class AudioData_SO : ScriptableObject
{
    public Dictionary<string,AudioClip> AudioClipDict{get=>AudioClipList.ToDictionary(item=>item.url,item=>item.audio);}

    [SerializeField]public List<AudioStruct> AudioClipList;

    [Serializable]
    public struct AudioStruct
    {
        public string url;
        public AudioClip audio;
    }
}
