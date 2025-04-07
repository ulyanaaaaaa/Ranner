using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    public Action<Coin> AddCoin;
    public Action OnDie;
    private RoadSpawner _roadSpawner;
    private AudioManager _audioManager;
    private bool _isLifeBonus;
    private Coroutine _bonusTick;
    private GameObject _slider;

    public PlayerTrigger Setup(RoadSpawner roadSpawner, AudioManager audioManager, GameObject slider)
    {
        _audioManager = audioManager;
        _roadSpawner = roadSpawner;
        _slider = slider;
        return this;
    }
    
    private void ActivateLifeBonus(LifeBonus lifeBonus)
    {
        if (_isLifeBonus)
            StopCoroutine(_bonusTick);
        
        _isLifeBonus = true;
        _bonusTick = StartCoroutine(DeactivateLifeBonus(lifeBonus));
    }

    private IEnumerator DeactivateLifeBonus(LifeBonus lifeBonus)
    {
        OnDie += () => { _slider.SetActive(false); };
        _slider.SetActive(true);
        Image bar = _slider.GetComponentInChildren<Image>();
        
        float time = 0f;

        while (time <= lifeBonus.Duration)
        {
            yield return new WaitForSeconds(0.01f);
            time += 0.01f;
            bar.fillAmount = (lifeBonus.Duration - time) / lifeBonus.Duration;
        }
        
        _isLifeBonus = false;
        _slider.gameObject.SetActive(false);
        OnDie -= () => { _slider.SetActive(false); };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out RoadTrigger trigger))
        {
            _roadSpawner.SpawnRoad();
        }

        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            AddCoin?.Invoke(coin);
            _audioManager.PlayCoinSound();
            Destroy(coin.gameObject);
        }

        if (other.gameObject.TryGetComponent(out Barrier barrier))
        {
            if (!_isLifeBonus)
                OnDie?.Invoke();
        }

        if (other.gameObject.TryGetComponent(out LifeBonus lifeBonus))
        {
            ActivateLifeBonus(lifeBonus);
            Destroy(lifeBonus.gameObject);
        }
    }
}