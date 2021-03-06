﻿using System.Collections;
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
    }

    private Dictionary<string, MAudio> Sounds;
    public AudioClip CreepyMusic1;
    public AudioClip TrainAmbient1;
    public AudioClip TrainAmbient2;
    public AudioClip TrainAmbient3;

    public void Init()
    {
        Sounds = new Dictionary<string, MAudio>()
        {
            { "CreepyMusic1", new MAudio() { AudioClip = CreepyMusic1 } },
            { "TrainAmbient", new MAudio() {
                    AudioClips = new AudioClip[3] { TrainAmbient1, TrainAmbient2, TrainAmbient3 }
                }
            }
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
        _backgroundMusicId = EazySoundManager.PrepareMusic(Sounds[musicName].AudioClip, 0.7f, loop, false);
        _backgroundMusic = musicName;
        audio = EazySoundManager.GetAudio(_backgroundMusicId.Value);
        audio.Play();
        if (time.HasValue)
        {
            audio.AudioSource.time = time.Value;
        }
    }

    public void PlayAmbient(string musicName, bool loop = true)
    {
        ClearAmbientQueue();
        _ambientMusic = musicName;
        if (Sounds[_ambientMusic].AudioClip != null)
        {
            PlayAmbientAudio(Sounds[musicName], loop);
        }
        else
        {
            StartCoroutine(PlayAllAmbientAudio(Sounds[musicName], loop));
        }
    }

    private void ClearAmbientQueue() {
        if (_ambientMusicId.HasValue)
        {
            Audio audio = null;
            audio = EazySoundManager.GetAudio(_ambientMusicId.Value);
            audio.Stop();
            audio = null;
        }
    }

    private Audio PlayAmbientAudio(MAudio mAudio, bool loop, bool loadMany = false)
    {
        ClearAmbientQueue();
        _ambientMusicId = EazySoundManager.PrepareMusic(
            loadMany ? mAudio.AudioClips[mAudio.GetRandomIndex()] : mAudio.AudioClip,
            0.2f,
            loop,
            false
            );
        Audio audio = EazySoundManager.GetAudio(_ambientMusicId.Value);
        audio.Play();
        return audio;
    }

    private IEnumerator PlayAllAmbientAudio(MAudio mAudio, bool loop)
    {
        var audio = PlayAmbientAudio(mAudio, loop, true);
        var time = audio.AudioSource.clip.length;

        yield return new WaitForSeconds(time);

        StartCoroutine(PlayAllAmbientAudio(mAudio, loop));
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

public class MAudio
{
    public AudioClip AudioClip;
    public AudioClip[] AudioClips;

    public int GetRandomIndex()
    {
        if (AudioClips != null && AudioClips.Length > 0)
        {
            return (int)Mathf.Ceil(Random.Range(0, AudioClips.Length));
        }
        return 0;
    }
}
