using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SacrificeUI : MonoBehaviour
{
    [Header("====================Active Setting====================")]
    [SerializeField] private TextMeshProUGUI _activeSettingText;
    [SerializeField] private TextMeshProUGUI _settingValueText;
    [SerializeField] private Image _settingProgressFill;
    [SerializeField] private Color _sacrificeColor;
    [SerializeField] private Color _unsacrificeColor;
    [SerializeField] private TextMeshProUGUI _leftArrow;
    [SerializeField] private TextMeshProUGUI _rightArrow;

    [Header("====================Other references====================")]
    [SerializeField] private Image _screenBrightnessPanel;

    private void OnEnable()
    {
        GameSetting.OnValueChanged += HandleChangedSetting;
        Sacrificer.OnActiveSettingChanged += UpdateUi;
    }

    // This is invoked from the sacrificer
    private void UpdateUi(object sender, GameSetting newGameSetting)
    {
        _activeSettingText.text = newGameSetting.SettingName;
        _settingValueText.text = newGameSetting.Value.ToString("0.##");
        _settingProgressFill.fillAmount = newGameSetting.GetImageFill();

        if (newGameSetting.SacrificeDirection == SacrificeDirection.Increment)
        {
            _rightArrow.color = _sacrificeColor;
            _leftArrow.color = _unsacrificeColor;
        }
        else if (newGameSetting.SacrificeDirection == SacrificeDirection.Decrement)
        {
            _rightArrow.color = _unsacrificeColor;
            _leftArrow.color = _sacrificeColor;
        }
    }

    // This is invoked from the GameSetting
    private void HandleChangedSetting(object sender, EventArgs eventArgs)
    {
        var changedSetting = (GameSetting) sender;

        switch (changedSetting.SettingType)
        {
            case GameSettingType.ScreenBrightness:
                _screenBrightnessPanel.color = new Color(0, 0, 0, 1 - changedSetting.Value);
                break;
        }

        UpdateUi(null, changedSetting);
    }

    private void OnDisable()
    {
        GameSetting.OnValueChanged -= HandleChangedSetting;
        Sacrificer.OnActiveSettingChanged -= UpdateUi;
    }
}
