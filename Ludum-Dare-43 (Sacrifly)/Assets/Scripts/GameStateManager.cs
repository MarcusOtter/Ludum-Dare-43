using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] internal CurrentGameSettings CurrentGameSettings;

    private void OnEnable()
    {
        GameSetting.OnValueChanged += HandleChangedSetting;
        Sacrificer.OnHitpointsChanged += CheckGameOver;
    }

    private void HandleChangedSetting(object sender, EventArgs e)
    {
        var changedSetting = (GameSetting) sender;

        switch (changedSetting.SettingType)
        {
            case GameSettingType.TargetFps:
                Application.targetFrameRate = (int) changedSetting.Value;
                break;

            case GameSettingType.GameSpeed:
                Time.timeScale = changedSetting.Value;
                break;
        }
    }

    private void CheckGameOver(object sender, SacrificeDirection hitpointDirection)
    {
        if (hitpointDirection == SacrificeDirection.Increment) { return; }

        var hitpoints = ((Sacrificer) sender).HitPoints;

        if (hitpoints <= 0)
        {
            print("GAME OVER!");
        }
    }

    private void OnDisable()
    {
        GameSetting.OnValueChanged -= HandleChangedSetting;
        Sacrificer.OnHitpointsChanged -= CheckGameOver;
    }
}
