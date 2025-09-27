using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuAnimations : MonoBehaviour
{
    public TMP_Text TitleText;
    public GameObject SettingsPanel;
    public GameObject Buttons;
    public CanvasGroup FadedPanel;

    private RectTransform _titleTextTransform;
    private RectTransform _settingsPanelTransform;
    private RectTransform _buttonsTransform;

    private Vector3 _settingsPanelStartPosition;

    public void Awake()
    {
        _titleTextTransform = TitleText.GetComponent<RectTransform>();
        _settingsPanelTransform = SettingsPanel.GetComponent<RectTransform>();
        _buttonsTransform = Buttons.GetComponent<RectTransform>();
        _settingsPanelStartPosition = _settingsPanelTransform.anchoredPosition;
    }

    private void Start()
    {
        SlideUpButtons();
        ScaleTitleText();
        DropDownTitleText();
    }

    private void DropDownTitleText()
    {
        if (_titleTextTransform != null)
        {
            Vector3 startPosition = new Vector2(0, -239f);
            _titleTextTransform.anchoredPosition = new Vector2(0, 200);
            _titleTextTransform.DOAnchorPos(startPosition, 2f).SetEase(Ease.OutBounce);
        }
    }

    private void ScaleTitleText()
    {
        if (_titleTextTransform != null)
        {
            _titleTextTransform.transform.DOScale(0.8f, 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnDestroy()
    {
        if (_titleTextTransform != null) { _titleTextTransform.transform.DOKill(); }
        FadedPanel.DOKill();
        _settingsPanelTransform.DOKill();
        _buttonsTransform.DOKill();
        transform.DOKill();
    }

    public void SlideToStartPositionSettingsPanel(GameObject panel)
    {
        _settingsPanelTransform.DOAnchorPos(_settingsPanelStartPosition, 1f).OnComplete(() => panel.SetActive(false));

    }

    public void SlideToCenterSettingsPanel()
    {
        _settingsPanelTransform.DOAnchorPos(Vector3.zero, 1f);
    }

    private void SlideUpButtons()
    {
        _buttonsTransform.anchoredPosition = new Vector3(_buttonsTransform.anchoredPosition.x, -800);
        _buttonsTransform.DOAnchorPos(new Vector3(_buttonsTransform.anchoredPosition.x, -150), 1f).SetEase(Ease.OutBounce);
    }

    public void ActivateFadePanel()
    {
        FadedPanel.gameObject.SetActive(true);
        FadedPanel.alpha = 0;
        FadedPanel.DOFade(1, 3f).OnComplete(() =>
        {
            transform.DOKill();
            SceneManager.LoadScene(1);            
        });
    }
}
