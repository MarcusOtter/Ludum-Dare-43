using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamerOverUI : MonoBehaviour
{
    private static float _bestTime;
    private static int _bestKillCount;
                   
    private static int _bestRunWaveCount;
    private static int _bestRunLoopCount;

    [Header("====================Latest run====================")]
    [SerializeField] private TextMeshProUGUI _latestTimeText;
    [SerializeField] private TextMeshProUGUI _latestKillCountText;
    [SerializeField] private TextMeshProUGUI _latestWaveCountText;
    [SerializeField] private TextMeshProUGUI _latestLoopCountText;

    [Header("====================Best run====================")]
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private TextMeshProUGUI _bestKillCountText;
    [SerializeField] private TextMeshProUGUI _bestRunWaveCountText;
    [SerializeField] private TextMeshProUGUI _bestRunLoopCountText;

    private float _latestTime;
    private int _latestKillCount;
    private int _latestWaveCount;
    private int _latestLoopCount;

    private void Awake()
    {
        _latestTime = Stats.ElapsedTime;
        _latestKillCount = Stats.KillCount;
        _latestWaveCount = Stats.WaveCount;
        _latestLoopCount = Stats.LoopCount;

        CheckHighscores();
        UpdateUi();
    }

    private void CheckHighscores()
    {
        if (_latestTime > _bestTime) { _bestTime = _latestTime; }
        if (_latestKillCount > _bestKillCount) { _bestKillCount = _latestKillCount; }

        if (_latestLoopCount > _bestRunLoopCount)
        {
            _bestRunLoopCount = _latestLoopCount;
            _bestRunWaveCount = _latestWaveCount;
        }
        else if (_latestLoopCount == _bestRunLoopCount)
        {
            if (_latestWaveCount > _bestRunWaveCount)
            {
                _bestRunWaveCount = _latestWaveCount;
            }
        }
    }

    private void UpdateUi()
    {
        _latestTimeText.text = $"{_latestTime:00.00}s";
        _latestKillCountText.text = _latestKillCount.ToString("0");
        _latestWaveCountText.text = _latestWaveCount.ToString("0");
        _latestLoopCountText.text = _latestLoopCount.ToString("0");

        _bestTimeText.text = $"{_bestTime:00.00}s";
        _bestKillCountText.text = _bestKillCount.ToString("0");
        _bestRunWaveCountText.text = _bestRunWaveCount.ToString("0");
        _bestRunLoopCountText.text = _bestRunLoopCount.ToString("0");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
