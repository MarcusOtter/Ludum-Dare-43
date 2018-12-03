using System;
using UnityEngine;

public class Sacrificer : MonoBehaviour
{
    internal static event EventHandler<GameSetting> OnActiveSettingChanged;
    internal static event EventHandler<SacrificeDirection> OnHitpointsChanged;

    internal int HitPoints { get; private set; } = 10;

    private int _activeGameSettingIndex;
    private CurrentGameSettings _currentGameSettings;

    private void OnEnable()
    {
        PlayerInput.OnWDown += NextGameSetting;
        PlayerInput.OnADown += Decrement;
        PlayerInput.OnSDown += PreviousGameSetting;
        PlayerInput.OnDDown += Increment;

        HitPoints = 10;
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
            var successful = currentSetting.Unsacrifice();
            if (successful) { ModifyHitpoints(-1); }
        }
        else if (currentSetting.SacrificeDirection == SacrificeDirection.Decrement)
        {
            var successful = currentSetting.Sacrifice();
            if (successful) { ModifyHitpoints(1); }
        }
    }

    private void Increment(object sender, EventArgs e)
    {
        var currentSetting = _currentGameSettings.GameSettings[_activeGameSettingIndex];

        if (currentSetting.SacrificeDirection == SacrificeDirection.Increment)
        {
            var successful = currentSetting.Sacrifice();
            if (successful) { ModifyHitpoints(1); }
        }
        else if (currentSetting.SacrificeDirection == SacrificeDirection.Decrement)
        {
            var successful = currentSetting.Unsacrifice();
            if (successful) { ModifyHitpoints(-1); }
        }
    }

    internal void ModifyHitpoints(int delta)
    {
        if (delta == 0) { return; }

        HitPoints += delta;
        OnHitpointsChanged?.Invoke(this, delta > 0 
            ? SacrificeDirection.Increment 
            : SacrificeDirection.Decrement);
    }

    private void OnDisable()
    {
        PlayerInput.OnWDown -= NextGameSetting;
        PlayerInput.OnADown -= Decrement;
        PlayerInput.OnSDown -= PreviousGameSetting;
        PlayerInput.OnDDown -= Increment;
    }
}
