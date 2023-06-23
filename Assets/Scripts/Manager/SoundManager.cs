using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    SoundData soundData = new SoundData();
    public static SoundManager Instance { get; private set; }

    [Header("Setup Sound")]
    public AudioSource _sourceBGM;
    public AudioSource _sourceSFX;
    
    public static Action<AudioClip> OnPlaySFXEvent;
    public static Action<float> OnVariableChange;

    public AudioClip SFXTestClip;

    private float _BGMVolume = 1f;
    private float _SFXVolume = 1f;

    public float BGMVolume{
        get { return _BGMVolume; }

        set{
            if (_BGMVolume == value) return;
            _BGMVolume = value;
            OnVariableChange?.Invoke(_BGMVolume);
        }
    }

    [Range(0f,1f)]
    public float debugBGMVolume = 1f;
    [Range(0f, 1f)]
    public float debugSFXVolume = 1f;

    private void Awake()
    {
        LoadSoundData();

        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        //BGMVolume = debugBGMVolume;
        //publicSFXVolume = debugSFXVolume;
    }

    private void OnEnable()
    {
        OnPlaySFXEvent += PlaySound;
        OnVariableChange += DebugOnChange;
    }

    private void OnDisable()
    {
        OnPlaySFXEvent -= PlaySound;
    }

    public void DebugOnChange(float value)
    {
        _sourceBGM.volume = BGMVolume;
        Debug.Log("Value Changed");
    }

    [ContextMenu("Play SFX")]
    public void TestPlay()
    {
        BGMVolume++;
        PlaySound(SFXTestClip);
    }
    
    [ContextMenu("Play BGM")]
    public void PlayBGM(AudioClip clip)
    {
        AudioSource source = _sourceBGM;
        source.volume = BGMVolume;
            //source.clip = AssetDatabase.LoadAssetAtPath("Assets\\Sound\\BGM\\13 P.I.M.P. Rewards (Instrumental).mp3", typeof(AudioClip)) as AudioClip;
        source.clip = clip;
        source.Play();
    }

    [ContextMenu("Stop BGM")]
    public void StopBGM()
    {
        AudioSource source = _sourceBGM;
        source.Stop();
        //source.clip = null;
    }

    public void PlaySound(AudioClip clip)
    {
        float volume = _SFXVolume;
        _sourceSFX.PlayOneShot(clip, volume);
    }

    public void PlaySoundWithDelay(AudioClip clip, float delay)
    {
        StartCoroutine(PlaySoundCoroutine(clip, delay));
    }

    private IEnumerator PlaySoundCoroutine(AudioClip clip, float delay)
    {
        AudioClip clipToPlay = clip;
        yield return new WaitForSeconds(delay);
        PlaySound(clipToPlay);
    }

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Confirm:
               PlaySound(AudioLibrary.Instance.confirmSound);
                break;
            case SoundType.Cancel:
                PlaySound(AudioLibrary.Instance.cancelSound);
                break;
            default:
                break;
        }
    }

    private void OnApplicationQuit()
    {
        SaveSoundData();
    }

    public void SaveSoundData()
    {
        soundData = new SoundData();
        soundData.volume_BGM = BGMVolume;
        soundData.volume_SFX = _SFXVolume;

        PlayerPrefs.SetString("SoundData", JsonUtility.ToJson(soundData));
    }

    public void LoadSoundData()
    {
        soundData = JsonUtility.FromJson<SoundData>(PlayerPrefs.GetString("SoundData"));
        if (soundData == null)
            Debug.Log("No Saves Found");
        else
        {
            Debug.Log("Saves Found \n Loading....");

            BGMVolume = soundData.volume_BGM;
            _SFXVolume = soundData.volume_SFX;
        }
    }
}

[Serializable]
public class SoundData
{
    public float volume_BGM;
    public float volume_SFX;
}

public enum SoundType
{
    Confirm,
    Cancel
}
