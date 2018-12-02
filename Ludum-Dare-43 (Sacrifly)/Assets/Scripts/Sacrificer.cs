using System;
using UnityEngine;

public class Sacrificer : MonoBehaviour
{
    internal static event EventHandler<GameSetting> OnActiveSettingChanged;

    private int _activeGameSettingIndex;

    private CurrentGameSettings _currentGameSettings;

    private void OnEnable()
    {
        PlayerInput.OnWDown += NextGameSetting;
        PlayerInput.OnADown += Decrement;
        PlayerInput.OnSDown += PreviousGameSetting;
        PlayerInput.OnDDown += Increment;
    }


    private void Start()
    {
        _currentGameSettings = FindObjectOfType<GameStateManager>()?.CurrentGameSettings;
        OnActiveSettingChanged?.Invoke(this, _currentGameSettings?.GameSettings[_activeGameSettingIndex]);
    }

    private void NextGameSetting(object sender, EventArgs e)
    {
        if (_activeGameSettingIndex + 1 > _currentGameSettings.GameSettings.Count - 1)
        {
            _activeGameSettingIndex = 0;
        }
        else
        {
            _activeGameSettingIndex += 1;
        }

        OnActiveSettingChanged?.Invoke(this, _currentGameSettings.GameSettings[_activeGameSettingIndex]);
    }

    private void PreviousGameSetting(object sender, EventArgs e)
    {
        if (_activeGameSettingIndex - 1 < 0)
        {
            _activeGameSettingIndex = _currentGameSettings.GameSettings.Count - 1;
        }
        else
        {
            _activeGameSettingIndex -= 1;
        }

        OnActiveSettingChanged?.Invoke(this, _currentGameSettings.GameSettings[_activeGameSettingIndex]);
    }

    private void Decrement(object sender, EventArgs e)
    {
        var currentSetting = _currentGameSettings.GameSettings[_activeGameSettingIndex];

        if (currentSetting.SacrificeDirection == SacrificeDirection.Increment)
        {
            currentSetting.Unsacrifice();
        }
        else if (currentSetting.SacrificeDirection == SacrificeDirection.Decrement)
        {
            currentSetting.Sacrifice();
        }
    }

    private void Increment(object sender, EventArgs e)
    {
        var currentSetting = _currentGameSettings.GameSettings[_activeGameSettingIndex];

        if (currentSetting.SacrificeDirection == SacrificeDirection.Increment)
        {
            currentSetting.Sacrifice();
        }
        else if (currentSetting.SacrificeDirection == SacrificeDirection.Decrement)
        {
            currentSetting.Unsacrifice();
        }
    }

    private void OnDisable()
    {
        PlayerInput.OnWDown -= Increment;
        PlayerInput.OnADown -= NextGameSetting;
        PlayerInput.OnSDown -= Decrement;
        PlayerInput.OnDDown -= NextGameSetting;
    }
}
