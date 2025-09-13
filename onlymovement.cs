using UnityEngine;

public class onlymovement : MonoBehaviour
{

    [SerializeField] private float _playerHeight;
    private bool _canJump = true;

    [SerializeField] private KeyCode _jumpKey;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _jumpCooldown;

    private Vector3 _movementDirection;

    [SerializeField] private float _movementSpeed;

    [SerializeField] private Transform _orientation;

    [SerializeField] private float _jumpForce;

    private float _horizontalinput, _verticalinput;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ResetJumping();

    }

    private void FixedUpdate()
    {
        movement();
    }
    private void movement()
    {
        _horizontalinput = Input.GetAxisRaw("Horizontal");
        _verticalinput = Input.GetAxisRaw("Vertical");

        _movementDirection = _orientation.forward * _verticalinput + _orientation.right * _horizontalinput;

        rb.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
    }

    private void jumping()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false;

            jumping();

            Invoke(nameof(CanJump), _jumpCooldown);

        }


    }

    private void CanJump()
    {
        _canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }
}
