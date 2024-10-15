using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Utilities/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    [SerializeField] private List<AudioFile> _audios;

    public AudioClip GetAudio(string name)
    {
        foreach (AudioFile file in _audios)
        {
            if (file.Name == name)
            {
                return file.Audio;
            }
        }

        return null;
    }

    public struct Names
    {
        public const string Cough = "Cough";
        public const string Spit = "Spit";
        public const string Spit2 = "Spit2";
        public const string Spit3 = "Spit3";
        public const string Theme = "Theme";
        public const string Vomiting = "Vomiting";
        public const string Sea = "Sea";
        public const string Seagull = "Seagull";
    }
}

[Serializable]
public struct AudioFile
{
    public string Name;
    public AudioClip Audio;
}
