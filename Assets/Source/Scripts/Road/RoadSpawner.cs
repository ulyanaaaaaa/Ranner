using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roads;
    [SerializeField] private float roadsLenght;

    private void Start()
    {
        Instantiate(_roads[Random.Range(0, _roads.Count - 1)], transform.position, Quaternion.identity);
    }

    public void Spawn()
    {
        Vector3 position = new Vector3(0, 0, roadsLenght);
        Instantiate(_roads[Random.Range(0, _roads.Count - 1)], position, Quaternion.identity);
    }
}
