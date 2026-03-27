using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("UI References")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
        }
        
        if(!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
        }
        Load();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(clip);
    }

    public void MusicVolumeChanged()
    {
        musicSource.volume = musicVolumeSlider.value;
        Save();
    }

    public void SfxVolumeChanged()
    {
        sfxSource.volume = sfxVolumeSlider.value;
        Save(); 
    }

    private void Load()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicVolumeSlider.value = musicVol;
        sfxVolumeSlider.value = sfxVol;

        musicSource.volume = musicVol;
        sfxSource.volume = sfxVol;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }
}