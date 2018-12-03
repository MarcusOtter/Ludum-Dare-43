using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _elapsedTimeText;
    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private TextMeshProUGUI _waveCountText;
    [SerializeField] private TextMeshProUGUI _loopCountText;

    private float _startTime;

    internal static float ElapsedTime;
    internal static int KillCount;
    internal static int WaveCount;
    internal static int LoopCount;

    private void Awake()
    {
        _startTime = Time.time;

        ElapsedTime = 0;
        KillCount = 0;
        WaveCount = 0;
        LoopCount = 0;
    }

    private void Update()
    {
        ElapsedTime = Time.time - _startTime;
        UpdateTimeUI();
    }

    private void UpdateTimeUI()
    {
        _elapsedTimeText.text = $"{ElapsedTime:00.00}s";
    }

    internal void IncrementKillCount()
    {
        _killCountText.text = (++KillCount).ToString("0");
    }

    internal void SetWaveCount(int newWave)
    {
        WaveCount = newWave;
        _waveCountText.text = WaveCount.ToString("0");
    }

    internal void IncrementLoopCount()
    {
        _loopCountText.text = (++LoopCount).ToString("0");
    }
}
