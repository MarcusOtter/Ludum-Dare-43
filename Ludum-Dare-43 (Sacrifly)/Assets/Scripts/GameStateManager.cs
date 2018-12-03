using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] internal CurrentGameSettings CurrentGameSettings;

    private void Awake()
    {
        foreach (var setting in CurrentGameSettings.GameSettings)
        {
            setting.SetValue(setting.StartingValue);
        }

        QualitySettings.vSyncCount = 0;
    }

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
            SceneManager.LoadScene(2);
        }
    }

    private void OnDisable()
    {
        GameSetting.OnValueChanged -= HandleChangedSetting;
        Sacrificer.OnHitpointsChanged -= CheckGameOver;
    }
}
