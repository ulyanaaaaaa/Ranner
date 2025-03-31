using TMPro;
using UnityEngine;

public class CoinsCount : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet; 
    private TextMeshProUGUI _count;

    private void Awake()
    {
        _count = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _playerWallet.OnCoinsChanged += UpdateCount; 
    }

    private void OnDisable()
    {
        _playerWallet.OnCoinsChanged -= UpdateCount; 
    }

    private void UpdateCount(int totalCoins)
    {
        _count.text = "<sprite name=\"coin\">" + totalCoins; 
    }
}