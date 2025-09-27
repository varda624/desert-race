using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{
    public Button StartGameButton;
    public Button ExitGameButton;
    public Button SettingsPanelButton;
    public Button HideSettingsButton;
    public GameObject SettingsPanel;
    public Slider MusicSlider;
    public TMP_Text VolumePercentText;
    public MenuAnimations MenuAnimations;

    private AudioSource _audioSource;
    private static float _volumeScale = 0.5f;

    public void Awake()
    {
        StartGameButton.onClick.AddListener(StartGame);
        ExitGameButton.onClick.AddListener(ExitGame);
        SettingsPanelButton.onClick.AddListener(OpenSettingsPanel);
        HideSettingsButton.onClick.AddListener(HideSettingsPanel);
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);

        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = _volumeScale;
        VolumePercentText.text = $"{Mathf.RoundToInt(_volumeScale * 100)}%";
        MusicSlider.value = _volumeScale;        
    }

    private void SetMusicVolume(float value)
    {
        _volumeScale = value;
        _audioSource.volume = _volumeScale;
        VolumePercentText.text = $"{Mathf.RoundToInt(_volumeScale * 100)}%";
    }

    private void StartGame()
    {
        MenuAnimations.ActivateFadePanel();
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void OpenSettingsPanel()
    {
        SettingsPanel.SetActive(true);
        MenuAnimations.SlideToCenterSettingsPanel();
    }

    private void HideSettingsPanel()
    {
        MenuAnimations.SlideToStartPositionSettingsPanel(SettingsPanel);
    }

}
