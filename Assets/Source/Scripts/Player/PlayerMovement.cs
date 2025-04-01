using UnityEngine;

[RequireComponent(typeof(SwipeDetection))]
public class PlayerMovement : MonoBehaviour
{
    private SwipeDetection _swipeDetection;

    private void Awake()
    {
        _swipeDetection = GetComponent<SwipeDetection>();
    }

    private void OnEnable()
    {
        _swipeDetection.OnLeftSwipe += GoLeft;
        _swipeDetection.OnRightSwipe += GoRight;
    }

    private void OnDisable()
    {
        _swipeDetection.OnLeftSwipe -= GoLeft;
        _swipeDetection.OnRightSwipe -= GoRight;
    }

    private void GoLeft()
    {
        if (transform.position.x > -0.8f)
        {
            transform.position += new Vector3(-1, 0, 0); 
        }
    }

    private void GoRight()
    {
        if (transform.position.x < 0.8f) 
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }
}
