using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonCustom<SoundManager>
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    public float masterVolume = 1;
    public float bgmMasterVolume = 1;
    public float seMasterVolume = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Playbgm(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.Play();
    }


    public void PlaySE(SESoundData.SE se)
    {
        Debug.Log("playSE");
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.clip = data.audioClip;
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

    private void Update()
    {
        bgmAudioSource.volume = 0.1f * bgmMasterVolume * masterVolume;
        seAudioSource.volume = 0.1f * seMasterVolume * masterVolume;
    }

}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        MainGame
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Shoot,
        Merge,
        Submit,
        Cancel
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}
