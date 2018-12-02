using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HitpointsUI : MonoBehaviour
{
    [Header("====================Colors====================")]
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    [Header("====================References====================")]
    [SerializeField] private Image[] _hearts;
    [SerializeField] private TextMeshProUGUI _hitpointsText;

    private void OnEnable()
    {
        Sacrificer.OnHitpointsChanged += UpdateUi;
    }

    private void UpdateUi(object sender, SacrificeDirection hitpointsDirection)
    {
        var hitpoints = ((Sacrificer) sender).HitPoints;

        if (hitpoints <= 0) { return; }

        if (hitpoints > _hearts.Length) { return; }

        for (int i = 0; i < hitpoints; i++)
        {
            _hearts[i].color = _activeColor;
        }

        for (int i = hitpoints; i < _hearts.Length; i++)
        {
            _hearts[i].color = _inactiveColor;
        }

        _hitpointsText.text = $"{hitpoints}/{_hearts.Length}";
    }

    private void OnDisable()
    {
        Sacrificer.OnHitpointsChanged -= UpdateUi;
    }
}
