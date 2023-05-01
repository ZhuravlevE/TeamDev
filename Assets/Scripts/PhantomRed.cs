using System.Collections;
using UnityEngine;

public class PhantomRed : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundBackCheck;
    [SerializeField] private LayerMask _groundBackMask;

    [SerializeField] private float _speed;

    private Vector2 _velocity;
    private bool _facingRight;
    private float _horizontal;
    private float _vertical;

    void Start()
    {
        _facingRight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().GetFacing();
        if (_facingRight)
            _horizontal = _speed;
        else
        {
            _horizontal = -_speed;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        }

        _vertical = 0f;
    }

    void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        if (IsGrounded() && !IsBackGrounded())
            _horizontal *= -1;

        _rigidbody.velocity = new Vector2(_horizontal, _vertical);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .2f, _groundMask);
    }

    private bool IsBackGrounded()
    {
        return Physics2D.OverlapCircle(_groundBackCheck.position, .2f, _groundBackMask);
    }

    private void Flip()
    {
        if (_facingRight && _horizontal < .0f || !_facingRight && _horizontal > .0f)
        {
            _facingRight = !_facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GrTag")
            StartCoroutine(UP());
        if (collision.tag == "YeTag")
            StartCoroutine(DOWN());
    }

    IEnumerator UP()
    {
        float hSpeed = _horizontal;
        _horizontal = 0f;
        _vertical = _speed;
        yield return new WaitForSeconds(.2f);
        _vertical = 0f;
        _horizontal = hSpeed;
    }

    IEnumerator DOWN()
    {
        float hSpeed = _horizontal;
        _horizontal = 0f;
        _vertical = -_speed;
        yield return new WaitForSeconds(.2f);
        _vertical = 0f;
        _horizontal = hSpeed;
    }
}
