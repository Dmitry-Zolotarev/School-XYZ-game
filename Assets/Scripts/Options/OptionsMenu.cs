using System.Collections.Generic;
using TMPro; // Обязательно для TMP_Dropdown
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TMP_Dropdown languageDropdown; // Правильный тип — TMP_Dropdown
    [SerializeField] private GameObject optionsMenu; // Панель настроек, которую показываем/скрываем

    private void Start()
    {
        try
        {
            var options = GameOptions.Instance;

            // Инициализация значений слайдеров и дропдауна
            musicSlider.value = options.musicVolume;
            soundSlider.value = options.soundVolume;
            languageDropdown.value = (int)options.language;

            // Очищаем предыдущие слушатели (на случай, если объект переиспользуется)
            musicSlider.onValueChanged.RemoveAllListeners();
            soundSlider.onValueChanged.RemoveAllListeners();
            languageDropdown.onValueChanged.RemoveAllListeners();

            // Подписываемся на изменения
            musicSlider.onValueChanged.AddListener(options.SetMusicVolume);
            soundSlider.onValueChanged.AddListener(options.SetSoundVolume);
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

            // Дополнительно: обновляем опции дропдауна (чтобы названия были на текущем языке)
            RefreshLanguageDropdownOptions();
        }
        catch { }
        
    }

    private void OnLanguageChanged(int index)
    {
        GameOptions.Instance.SetLanguage((GameLanguage)index);

        // После смены языка сразу обновляем текст в дропдауне (например, "English" → "Русский")
        RefreshLanguageDropdownOptions();
    }

    // Обновляем значения UI, если настройки изменились извне (например, из другой сцены)
    private void OnEnable()
    {
        if (GameOptions.Instance == null) return;

        musicSlider.value = GameOptions.Instance.musicVolume;
        soundSlider.value = GameOptions.Instance.soundVolume;
        languageDropdown.value = (int)GameOptions.Instance.language;

        // Обновляем названия языков в дропдауне
        RefreshLanguageDropdownOptions();
    }

    // Метод для переключения видимости меню настроек
    public void ToggleOptionsMenu()
    {
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(!optionsMenu.activeSelf);
        }
    }

    // Вспомогательный метод: обновляет текст опций в дропдауне на текущем языке
    private void RefreshLanguageDropdownOptions()
    {
        if (languageDropdown == null) return;

        languageDropdown.ClearOptions();

        // Добавляем варианты на текущем языке
        var options = GameOptions.Instance.language == GameLanguage.Russian
            ? new List<string> { "Русский", "English" }
            : new List<string> { "Russian", "English" };
        // Восстанавливаем выбранное значение
        languageDropdown.value = (int)GameOptions.Instance.language;
        languageDropdown.RefreshShownValue();
    }
}