using System;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public Action OnRightSwipe;
    public Action OnLeftSwipe;
    public Action OnUpSwipe; 
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
        float swipeDistanceY = _touchEndPos.y - _touchStartPos.y;

        if (Mathf.Abs(swipeDistanceX) > swipeDistanceThreshold || Mathf.Abs(swipeDistanceY) > swipeDistanceThreshold)
        {
            if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY)) 
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
            else
            {
                if (swipeDistanceY > 0)
                {
                    OnUpSwipe?.Invoke();
                }
            }
        }
    }
}