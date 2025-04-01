using System;
using TMPro;
using UnityEngine;

public class CoinsCount : MonoBehaviour
{
    private PlayerWallet _playerWallet; 
    private TextMeshProUGUI _count;

    public void Setup(PlayerWallet playerWallet)
    {
        _playerWallet = playerWallet;
        _playerWallet.OnCoinsChanged += UpdateCount; 
    }

    private void Awake()
    {
        _count = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateCount(0);
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