using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roads;
    [SerializeField] private float roadsLength;
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedIncreaseAmount; 
    [SerializeField] private float increaseInterval; 

    private float _speed;
    private Road _road;

    private void Start()
    {
        _speed = startSpeed;
        
        _road = Instantiate(_roads[Random.Range(0, _roads.Count)], transform.position, Quaternion.identity)
            .GetComponent<Road>()
            .SetupSpeed(startSpeed);

        StartCoroutine(IncreaseSpeedOverTime());
    }

    public void Spawn()
    {
        Vector3 position = new Vector3(0, 0, roadsLength);
        _road = Instantiate(_roads[Random.Range(0, _roads.Count)], position, Quaternion.identity)
            .GetComponent<Road>()
            .SetupSpeed(_speed);
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseInterval);
            _speed += speedIncreaseAmount;
            _road.SetupSpeed(startSpeed); 
        }
    }
}