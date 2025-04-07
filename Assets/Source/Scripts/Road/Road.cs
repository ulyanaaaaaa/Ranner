using UnityEngine;

public class Road : MonoBehaviour
{
    private float _speed;

    public Road SetupSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.forward * _speed * Time.deltaTime);
    }
}