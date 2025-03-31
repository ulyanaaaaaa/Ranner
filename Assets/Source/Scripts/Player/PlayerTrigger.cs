using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private RoadSpawner _roadSpawner;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out RoadTrigger trigger))
        {
            _roadSpawner.Spawn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
