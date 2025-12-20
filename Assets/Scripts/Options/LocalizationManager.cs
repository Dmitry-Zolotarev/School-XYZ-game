using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class LocalizationManager : SingletonComponent
{
    public static LocalizationManager Instance => instance as LocalizationManager;

    private List<List<string>> locales = new List<List<string>>();
    private List<string> currentLocale;
    private List<string> russianLocaleList; //  лючи Ч всегда русский текст
    private int currentLanguageIndex = -1;

    [Header("Locale Files (place in Resources/Locales folder)")]
    public TextAsset russianLocale;
    public TextAsset englishLocale;

    private void Start()
    {
        LoadLocaleFiles();
        SceneManager.sceneLoaded += OnSceneLoaded;
        ApplyLanguage();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void LoadLocaleFiles()
    {
        if (russianLocale == null)
            russianLocale = Resources.Load<TextAsset>("Locales/russian");
        if (englishLocale == null)
            englishLocale = Resources.Load<TextAsset>("Locales/english");

        if (russianLocale != null)
        {
            russianLocaleList = russianLocale.text
                .Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList();
            locales.Add(russianLocaleList);
        }
        else
        {
            Debug.LogError("Russian locale file not found in Resources/Locales!");
        }

        if (englishLocale != null)
        {
            locales.Add(englishLocale.text
                .Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList());
        }
        else
        {
            Debug.LogError("English locale file not found in Resources/Locales!");
        }
    }

    public void ApplyLanguage()
    {
        int langIndex = (int)GameOptions.Instance.language;

        if (langIndex == currentLanguageIndex || langIndex >= locales.Count)
            return;

        currentLanguageIndex = langIndex;
        currentLocale = locales[langIndex];

        UpdateAllTextsInCurrentScene();

        Debug.Log($"Language applied: {(langIndex == 0 ? "Russian" : "English")}");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateAllTextsInCurrentScene();
    }

    public void UpdateAllTextsInCurrentScene()
    {
        // Legacy UI Text
        foreach (var text in FindObjectsOfType<Text>(includeInactive: true))
        {
            if (!string.IsNullOrEmpty(text.text))
            {
                text.text = Localize(text.text);
            }
        }

        // TextMeshPro
        foreach (var tmp in FindObjectsOfType<TextMeshProUGUI>(includeInactive: true))
        {
            if (!string.IsNullOrEmpty(tmp.text))
            {
                tmp.text = Localize(tmp.text);
            }
        }
    }

    public string Localize(string russianKey)
    {
        if (string.IsNullOrEmpty(russianKey) || russianLocaleList == null || currentLocale == null)
            return russianKey;

        string trimmedKey = russianKey.Trim();

        int index = russianLocaleList.FindIndex(s => s == trimmedKey);

        if (index >= 0 && index < currentLocale.Count)
        {
            return currentLocale[index];
        }

        return russianKey; // ≈сли перевода нет Ч оставл€ем как есть
    }
}