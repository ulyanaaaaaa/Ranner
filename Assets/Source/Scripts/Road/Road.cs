using UnityEngine;

public class Road : MonoBehaviour
{
    private float _speed;

    public Road SetupSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    private void Update()
    {
        DestroyRoad();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.forward * _speed * Time.deltaTime);
    }

    private void DestroyRoad()
    {
        if (transform.position.z < -80f)
        {
            Destroy(gameObject);
        }
    }
}
