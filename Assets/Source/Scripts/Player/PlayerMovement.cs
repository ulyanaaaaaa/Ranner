using UnityEngine;

[RequireComponent(typeof(SwipeDetection))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    private SwipeDetection _swipeDetection;
    private bool _isJumping;
    [SerializeField] private float jumpForce; 
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _swipeDetection = GetComponent<SwipeDetection>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator.SetBool("IsRun", true);
    }

    private void OnEnable()
    {
        _swipeDetection.OnLeftSwipe += GoLeft;
        _swipeDetection.OnRightSwipe += GoRight;
        _swipeDetection.OnUpSwipe += Jump; 
    }

    private void OnDisable()
    {
        _swipeDetection.OnLeftSwipe -= GoLeft;
        _swipeDetection.OnRightSwipe -= GoRight;
        _swipeDetection.OnUpSwipe -= Jump;
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

    private void Jump()
    {
        if (!_isJumping)
        {
            _animator.SetBool("IsJump", true);
            _isJumping = true;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("IsJump", false);
            _isJumping = false;
        }
    }
}