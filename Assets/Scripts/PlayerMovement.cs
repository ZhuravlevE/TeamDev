using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, описывающий движения игрового персонажа
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Масса персонажа
    /// </summary>
    [SerializeField] private Rigidbody2D _rigidbody;
    
    /// <summary>
    /// Точка на которой стоит персонаж
    /// </summary>
    [SerializeField] private Transform _groundCheck;
    
    /// <summary>
    /// Фильтр на землю
    /// </summary>
    [SerializeField] private LayerMask _groundMask;
    
    /// <summary>
    /// Фильтр на НИПов
    /// </summary>
    [SerializeField] private LayerMask _headMask;

    /// <summary>
    /// Скорость передвижения персонажа
    /// </summary>
    [SerializeField] private float _speed;
    
    /// <summary>
    /// Сила прыжка персонажа
    /// </summary>
    [SerializeField] private float _jumpForce;
    
    /// <summary>
    /// Анимация
    /// </summary>
    [SerializeField] private Animator _anim;
    
    /// <summary>
    /// Флаг ввода
    /// </summary>
    private bool _enableInput;
    
    /// <summary>
    /// Смещение по горизонтали
    /// </summary>
    private float _horizontal;
    
    /// <summary>
    /// Смещение по вертикали
    /// </summary>
    private float _vertical;
    
    /// <summary>
    /// Флаг движения влево
    /// </summary>
    private bool _mleft;
    
    /// <summary>
    /// Флаг движения вправо
    /// </summary>
    private bool _mright;
    
    /// <summary>
    /// Флаг прыжка
    /// </summary>
    private bool _mjump;

    /// <summary>
    /// Направление взгляда персонажа
    /// </summary>
    private bool _facingRight;

    /// <summary>
    /// Звук смерти персонажа
    /// </summary>
    [SerializeField] private AudioClip _dead;
    
    /// <summary>
    /// Хранилище звуков
    /// </summary>
    [SerializeField] private AudioSource _audioSource;

    /// <summary>
    /// Состояние персонажа(бежит/не бежит) 
    /// </summary>
    public int playerState { get; private set; }
    
    /// <summary>
    /// Направление взгляда персонажа
    /// </summary>
    public bool facingRight { get; private set; }

    /// <summary>
    /// Инициализация начального состояния
    /// </summary>
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

    /// <summary>
    /// Рассчет движения персонажа
    /// </summary>
    private void Update()
    {
        if (_enableInput)
           GetInput();

        SetState();

        Flip();
       
    }

    /// <summary>
    /// Отображение изменения положения персонажа
    /// </summary>
    private void FixedUpdate()
    {
        //if (_enableInput)
           

        ApplyMovement();
        MoveAnim();
    }

    /// <summary>
    /// Считывание нажатяи клавиш
    /// </summary>
    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if ((Input.GetButton("Jump") || _mjump) && (IsGrounded() || IsHeaded()))
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);


        if(_horizontal == 0f)
            NewButHorizontal();


    }

    /// <summary>
    /// Обновление смещения персонажа
    /// </summary>
    private void NewButHorizontal()
    {
        if (_mleft)
            _horizontal = -1f;
        else if (_mright)
            _horizontal = 1f;
        else _horizontal = 0f;
    }

    /// <summary>
    /// Обновление движения персонажа
    /// </summary>
    private void ApplyMovement()
    {
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);
    }

    /// <summary>
    /// Анимация движения персонажа
    /// </summary>
    private void MoveAnim()
    {
        if (playerState == 1)
        {
            _anim.SetBool("run", true);
        }
        else _anim.SetBool("run", false);
    }
    
    /// <summary>
    /// Разворот персонажа
    /// </summary>
    private void Flip()
    {
        if(_facingRight && _horizontal < .0f || !_facingRight && _horizontal > .0f)
        {
            _facingRight = !_facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        }
        facingRight = _facingRight;
    }

    /// <summary>
    /// Проверка нахождения персонажа на земле
    /// </summary>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .4f, _groundMask);
    }

    /// <summary>
    /// Проверка нахождения персонажа на голове НИПа
    /// </summary>
    private bool IsHeaded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .4f, _headMask);
    }

    /// <summary>
    /// Обновление состояния персонажа
    /// </summary>
    private void SetState()
    {
        if (_horizontal == .0f)
            playerState = 0;
        else
            playerState = 1;
    }

    /// <summary>
    /// Проверка состояния направления взгляда персонажа
    /// </summary>
    public bool GetFacing()
    {
        return _facingRight;
    }

    /// <summary>
    /// Получение состояния смещения по горизонтали
    /// </summary>
    public float GetHorizontal()
    {
        return _horizontal;
    }

    /// <summary>
    /// Получение состояния ввода
    /// </summary>
    public bool GetEnableInput()
    {
        return _enableInput;
    }

    /// <summary>
    /// Столкновение персонажа с НИПом
    /// </summary>
    /// <param name="collision">Объект столкновения</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red" || collision.tag == "Green" || collision.tag == "Yellow")
        {
            Dead();
        }

    }

    /// <summary>
    /// Анимация смерти персонажа
    /// </summary>
    private void Dead()
    {
        _audioSource.PlayOneShot(_dead);

        _enableInput = false;
        _anim.SetBool("dead", true);

        transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;
    }
    
    /// <summary>
    /// Нажатие кнопки Влево
    /// </summary>
    public void LeftDownButton()
    {
        _mleft = true;
    }
    
    /// <summary>
    /// Нажатие кнопки Вправо
    /// </summary>
    public void RightDownButton()
    {
        _mright = true;
    }
    
    /// <summary>
    /// Отжатие кнопоп движения
    /// </summary>
    public void MoveUpButton()
    {
        _mleft = false;
        _mright = false;
    }
    
    /// <summary>
    /// Нажатие кнопки Вниз
    /// </summary>
    public void JumpDownButton()
    {
        _mjump = true;
    }
    
    /// <summary>
    /// Отжатие кнопки прыжка
    /// </summary>
    public void JumpUpButton()
    {
        _mjump = false;
    }

}
