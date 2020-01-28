using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    public static MusicManager Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
        InitializeSounds();
    }

    private Dictionary<string, AudioClip> Sounds;
    public AudioClip CreepyMusic1;
    public AudioClip TrainAmbient1;
    public AudioClip TrainAmbient2;
    public AudioClip TrainAmbient3;

    void InitializeSounds()
    {
        Sounds = new Dictionary<string, AudioClip>()
        {
            { "CreepyMusic1", CreepyMusic1 },
            { "TrainAmbient1", TrainAmbient1 },
            { "TrainAmbient2", TrainAmbient2 },
            { "TrainAmbient3", TrainAmbient3 }
        };
    }

    private int? _backgroundMusicId;
    private string _backgroundMusic;
    private int? _ambientMusicId;
    private string _ambientMusic;

    public void PlayBackgroundMusic(string musicName, bool loop = true, float? time = null)
    {
        Audio audio;
        if (_backgroundMusicId.HasValue)
        {
            audio = EazySoundManager.GetAudio(_backgroundMusicId.Value);
            audio.Stop();
        }
        _backgroundMusicId = EazySoundManager.PrepareMusic(Sounds[musicName], 0.7f, loop, false);
        _backgroundMusic = musicName;
        audio = EazySoundManager.GetAudio(_backgroundMusicId.Value);
        audio.Play();
        if (time.HasValue)
        {
            audio.AudioSource.time = time.Value;
        }
    }

    public void PlayAmbient(string musicName, bool loop = true) {
        Audio audio;
        if (_ambientMusicId.HasValue)
        {
            audio = EazySoundManager.GetAudio(_ambientMusicId.Value);
            audio.Stop();
        }
        _ambientMusicId = EazySoundManager.PrepareMusic(Sounds[musicName], 0.2f, loop, false);
        _ambientMusic = musicName;
        audio = EazySoundManager.GetAudio(_ambientMusicId.Value);
        audio.Play();
    }

    public void PlayRequiredBackgroundMusic(string musicName, bool loop = true, float? time = null)
    {
        if (_backgroundMusic == musicName)
        {
            return;
        }

        PlayBackgroundMusic(musicName, loop, time);
    }

    public void PlayRequiredAmbient(string musicName, bool loop = false)
    {
        if (_ambientMusic == musicName)
        {
            return;
        }

        PlayAmbient(musicName, loop);
    }
}
