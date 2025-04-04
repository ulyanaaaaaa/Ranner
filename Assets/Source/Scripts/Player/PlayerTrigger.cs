using System;
using System.Collections;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Action<Coin> AddCoin;
    public Action OnDie;
    private RoadSpawner _roadSpawner;
    private AudioManager _audioManager;
    private bool _isLifeBonus;

    public PlayerTrigger Setup(RoadSpawner roadSpawner, AudioManager audioManager)
    {
        _audioManager = audioManager;
        _roadSpawner = roadSpawner;
        return this;
    }
    
    private void ActivateLifeBonus(float time)
    {
        _isLifeBonus = true;
        StartCoroutine(DeactivateLifeBonus(time));
    }

    private IEnumerator DeactivateLifeBonus(float time)
    {
        yield return new WaitForSeconds(time);
        _isLifeBonus = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out RoadTrigger trigger))
        {
            _roadSpawner.Spawn();
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
            ActivateLifeBonus(lifeBonus.Time);
            Destroy(lifeBonus.gameObject);
        }
    }
}