using System;
using UnityEngine;

[RequireComponent(typeof(PlayerTrigger))]
public class PlayerWallet : MonoBehaviour
{
    public int CurrentCoins { get; private set; }
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
        CurrentCoins += coin.Count;
        OnCoinsChanged?.Invoke(CurrentCoins); 
    }
}