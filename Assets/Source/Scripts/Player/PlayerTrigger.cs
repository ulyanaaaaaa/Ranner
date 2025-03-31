using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Action<Coin> AddCoin;
    [SerializeField] private RoadSpawner _roadSpawner;
    
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
    }
}
