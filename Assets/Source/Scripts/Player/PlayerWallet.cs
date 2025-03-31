using System;
using UnityEngine;

[RequireComponent(typeof(PlayerTrigger))]
public class PlayerWallet : MonoBehaviour
{
    private int _currentCoins;
    private PlayerTrigger _playerTrigger;
    
    public Action<int> OnCoinsChanged; 

    private void Awake()
    {
        _playerTrigger = GetComponent<PlayerTrigger>();
    }

    private void OnEnable()
    {
        _playerTrigger.AddCoin += AddCoin;
    }

    private void OnDisable()
    {
        _playerTrigger.AddCoin -= AddCoin;
    }

    private void AddCoin(Coin coin)
    {
        _currentCoins += coin.Count;
        OnCoinsChanged?.Invoke(_currentCoins); 
    }
}