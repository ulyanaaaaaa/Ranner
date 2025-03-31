using System;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public Action OnRightSwipe;
    public Action OnLeftSwipe;
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    [SerializeField] private float swipeDistanceThreshold; 

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    _touchEndPos = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    private void DetectSwipe()
    {
        float swipeDistanceX = _touchEndPos.x - _touchStartPos.x;

        if (Mathf.Abs(swipeDistanceX) > swipeDistanceThreshold)
        {
            if (swipeDistanceX > 0)
            {
                OnRightSwipe?.Invoke();
            }
            else
            {
                OnLeftSwipe?.Invoke();
            }
        }
    }
}