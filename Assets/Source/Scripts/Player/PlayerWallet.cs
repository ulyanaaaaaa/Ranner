using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerTrigger))]
public class PlayerWallet : MonoBehaviour
{
    public int CurrentCoins { get; private set; }
    private PlayerTrigger _playerTrigger;
    
    public Action<int> OnCoinsChanged;
    private bool _isMoneyBonus;

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
        if (_isMoneyBonus)
        {
            CurrentCoins += coin.Count * 2;
        }
        else
        {
            CurrentCoins += coin.Count;
        }

        OnCoinsChanged?.Invoke(CurrentCoins); 
    }
    
    private void ActivateMoneyBonus(float time)
    {
        _isMoneyBonus = true;
        StartCoroutine(DeactivateMoneyBonus(time));
    }

    private IEnumerator DeactivateMoneyBonus(float time)
    {
        yield return new WaitForSeconds(time);
        _isMoneyBonus = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MoneyBonus moneyBonus))
        {
            ActivateMoneyBonus(moneyBonus.Time);
            Destroy(moneyBonus.gameObject);
        }
    }
}