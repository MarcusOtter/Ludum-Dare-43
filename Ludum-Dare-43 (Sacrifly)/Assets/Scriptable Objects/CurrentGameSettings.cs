using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class CurrentGameSettings : ScriptableObject
{
    [SerializeField] internal List<GameSetting> GameSettings;
}
