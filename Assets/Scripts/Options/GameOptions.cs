using UnityEngine;

public enum GameLanguage
{
    Russian = 0,
    English = 1
}

public class GameOptions : SingletonComponent
{
    public static GameOptions Instance => instance as GameOptions;

    [Header("Default Values")]
    public float musicVolume = 0.5f;
    public float soundVolume = 1f;
    public GameLanguage language = GameLanguage.English; // ѕо умолчанию Ч английский!

    [Header("Audio Sources (assign in Inspector)")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Start()
    {
        Load();
        ApplyAudioSettings();
    }

    private void ApplyAudioSettings()
    {
        if (musicSource != null) musicSource.volume = musicVolume;
        if (sfxSource != null) sfxSource.volume = soundVolume;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        ApplyAudioSettings();
        Save();
    }

    public void SetSoundVolume(float value)
    {
        soundVolume = value;
        ApplyAudioSettings();
        Save();
    }

    public void SetLanguage(GameLanguage newLanguage)
    {
        if (language == newLanguage) return;

        language = newLanguage;
        Save();
        LocalizationManager.Instance?.ApplyLanguage(); // ќбновл€ем сразу
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.SetInt("Language", (int)language);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", soundVolume);
        language = (GameLanguage)PlayerPrefs.GetInt("Language", (int)GameLanguage.English); // ƒефолт Ч English

        ApplyAudioSettings();
    }
}