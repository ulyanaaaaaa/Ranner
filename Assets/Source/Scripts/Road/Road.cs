using System;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        //DestroyRoad();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.forward * speed * Time.deltaTime);
    }

    private void DestroyRoad()
    {
        if (transform.position.z < -30f)
        {
            Destroy(gameObject);
        }
    }
}
