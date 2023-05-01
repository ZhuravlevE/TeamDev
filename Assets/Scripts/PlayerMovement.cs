using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _headMask;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Animator _anim;
    
    private bool _enableInput;
    private float _horizontal;
    private float _vertical;

    private bool _mleft;
    private bool _mright;
    private bool _mjump;

    private bool _facingRight;

    [SerializeField] private AudioClip _dead;
    [SerializeField] private AudioSource _audioSource;

    public int playerState { get; private set; }
    public bool facingRight { get; private set; }

    private void Start()
    {
        _enableInput = true;
        _horizontal = .0f;
        _vertical = 0.0f;

        _mleft = false;
        _mright = false;
        _mjump = false;

        _facingRight = true;
}

    private void Update()
    {
        if (_enableInput)
           GetInput();

        SetState();

        Flip();
       
    }

    private void FixedUpdate()
    {
        //if (_enableInput)
           


        ApplyMovement();
        MoveAnim();
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if ((Input.GetButton("Jump") || _mjump) && (IsGrounded() || IsHeaded()))
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);


        if(_horizontal == 0f)
            NewButHorizontal();


    }

    private void NewButHorizontal()
    {
        if (_mleft)
            _horizontal = -1f;
        else if (_mright)
            _horizontal = 1f;
        else _horizontal = 0f;
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);
    }

    private void MoveAnim()
    {
        if (playerState == 1)
        {
            _anim.SetBool("run", true);
        }
        else _anim.SetBool("run", false);
    }
    private void Flip()
    {
        if(_facingRight && _horizontal < .0f || !_facingRight && _horizontal > .0f)
        {
            _facingRight = !_facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        }
        facingRight = _facingRight;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .4f, _groundMask);
    }

    private bool IsHeaded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .4f, _headMask);
    }

    private void SetState()
    {
        if (_horizontal == .0f)
            playerState = 0;
        else
            playerState = 1;
    }

    public bool GetFacing()
    {
        return _facingRight;
    }

    public float GetHorizontal()
    {
        return _horizontal;
    }

    public bool GetEnableInput()
    {
        return _enableInput;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red" || collision.tag == "Green" || collision.tag == "Yellow")
        {
            Dead();
        }

    }

    private void Dead()
    {
        _audioSource.PlayOneShot(_dead);

        _enableInput = false;
        _anim.SetBool("dead", true);

        transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;
    }

    public void LeftDownButton()
    {
        _mleft = true;
    }
    public void RightDownButton()
    {
        _mright = true;
    }
    public void MoveUpButton()
    {
        _mleft = false;
        _mright = false;
    }
    public void JumpDownButton()
    {
        _mjump = true;
    }
    public void JumpUpButton()
    {
        _mjump = false;
    }

}
