using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Action<Coin> AddCoin;
    public Action OnDie;
    [SerializeField] private RoadSpawner _roadSpawner;

    public PlayerTrigger Setup(RoadSpawner roadSpawner)
    {
        _roadSpawner = roadSpawner;
        return this;
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
            Destroy(coin.gameObject);
        }
        
        if (other.gameObject.TryGetComponent(out Barrier barrier))
        {
            OnDie?.Invoke();
        }
    }
}
