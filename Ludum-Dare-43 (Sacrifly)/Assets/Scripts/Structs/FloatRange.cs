[System.Serializable]
public class FloatRange
{
    public readonly float Min;
    public readonly float Max;

    public FloatRange(float minValue, float maxValue)
    {
        Min = minValue;
        Max = maxValue;
    }
}
