using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Setting")]
public class GameSetting : ScriptableObject
{
    internal static event EventHandler OnValueChanged;

    [SerializeField] internal string SettingName;
    [SerializeField] internal GameSettingType SettingType;
    [SerializeField] internal SacrificeDirection SacrificeDirection;

    internal float Value { get; private set; }

    [SerializeField] private float _sacrificeDelta;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _minValue;
    [SerializeField] private float _startingValue;

    private void OnEnable()
    {
        Value = _startingValue;
    }

    /// <summary>
    /// Returns true if the sacrifice was successful and false if the value was not modified.
    /// </summary>
    internal bool Sacrifice()
    {
        if (SacrificeDirection == SacrificeDirection.Decrement)
        {
            if (Mathf.Approximately(Value, _minValue)) { return false; }

            if (Value - _sacrificeDelta < _minValue)
            {
                Value = _minValue;
            }
            else
            {
                Value -= _sacrificeDelta;
            }
        }
        else if (SacrificeDirection == SacrificeDirection.Increment)
        {
            if (Mathf.Approximately(Value, _maxValue)) { return false; }

            if (Value + _sacrificeDelta > _maxValue)
            {
                Value = _maxValue;
            }
            else
            {
                Value += _sacrificeDelta;
            }
        }

        OnValueChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    /// <summary>
    /// Returns true if the unsacrifice was successful and false if the value was not modified.
    /// </summary>
    internal bool Unsacrifice()
    {
        if (SacrificeDirection == SacrificeDirection.Decrement)
        {
            if (Mathf.Approximately(Value, _maxValue)) { return false; }

            if (Value + _sacrificeDelta > _maxValue)
            {
                Value = _maxValue;
            }
            else
            {
                Value += _sacrificeDelta;
            }
        }
        else if (SacrificeDirection == SacrificeDirection.Increment)
        {
            if (Mathf.Approximately(Value, _minValue)) { return false; }

            if (Value - _sacrificeDelta < _minValue)
            {
                Value = _minValue;
            }
            else
            {
                Value -= _sacrificeDelta;
            }
        }

        OnValueChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    internal float GetImageFill()
    {
        if (SacrificeDirection == SacrificeDirection.Increment)
        {
            return (Value - _minValue) / (_maxValue - _minValue);
        }

        if (SacrificeDirection == SacrificeDirection.Decrement)
        {
            return 1 - (_maxValue - Value) / (_maxValue - _minValue);
        }

        return 0;
    }
}
