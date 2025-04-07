using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerTrigger))]
public class PlayerWallet : MonoBehaviour
{
    public int CurrentCoins { get; private set; }
    private PlayerTrigger _playerTrigger;
    
    public Action<int> OnCoinsChanged;
    private bool _isMoneyBonus;
    private Coroutine _bonusTick;
    private GameObject _slider;

    public PlayerWallet Setup(GameObject slider)
    {
        _slider = slider;
        return this;
    }

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
    
    private void ActivateMoneyBonus(MoneyBonus moneyBonus)
    {
        if (_isMoneyBonus)
            StopCoroutine(_bonusTick);
        
        _isMoneyBonus = true;
        _bonusTick = StartCoroutine(DeactivateLifeBonus(moneyBonus));
    }

    private IEnumerator DeactivateLifeBonus(MoneyBonus moneyBonus)
    {
        _playerTrigger.OnDie += () => {  _slider.SetActive(false); };
        _slider.SetActive(true);
        Image bar = _slider.GetComponentInChildren<Image>();
        
        float time = 0f;

        while (time <= moneyBonus.Duration)
        {
            yield return new WaitForSeconds(0.01f);
            time += 0.01f;
            bar.fillAmount = (moneyBonus.Duration - time) / moneyBonus.Duration;
        }
        
        _isMoneyBonus = false;
        _slider.gameObject.SetActive(false);
        _playerTrigger.OnDie -= () => { _slider.gameObject.SetActive(false); };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MoneyBonus moneyBonus))
        {
            ActivateMoneyBonus(moneyBonus);
            Destroy(moneyBonus.gameObject);
        }
    }
}