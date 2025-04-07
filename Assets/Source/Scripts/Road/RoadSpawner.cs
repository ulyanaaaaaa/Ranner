using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roads; 
    [SerializeField] private float roadLength; 
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedIncreaseAmount; 
    [SerializeField] private float increaseInterval;

    private float _speed;
    private Queue<GameObject> _activeRoads = new Queue<GameObject>();

    private void Awake()
    {
        _speed = startSpeed;
        
        for (int i = 0; i < 3; i++)
        {
            Vector3 position = new Vector3(0, 0, i * roadLength);
            GameObject road = Instantiate(_roads[Random.Range(0, _roads.Count)], position, Quaternion.identity);
            road.GetComponent<Road>().SetupSpeed(_speed);
            _activeRoads.Enqueue(road);
        }

        StartCoroutine(IncreaseSpeedOverTime());
    }

    public void SpawnRoad()
    {
        Vector3 newPosition =  _activeRoads.ToArray()[_activeRoads.Count - 1].transform.position + new Vector3(0, 0, roadLength);
        GameObject newRoad = Instantiate(_roads[Random.Range(0, _roads.Count)], newPosition, Quaternion.identity);
        newRoad.GetComponent<Road>().SetupSpeed(_speed);

        _activeRoads.Enqueue(newRoad);
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseInterval);
            _speed += speedIncreaseAmount;
            
            foreach (GameObject road in _activeRoads)
            {
                road.GetComponent<Road>().SetupSpeed(_speed);
            }
        }
    }
}
