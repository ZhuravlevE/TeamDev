using System.Collections;
using UnityEngine;

/// <summary>
/// Класс, описывающий поведение красного НИПа
/// </summary>
public class PhantomRed : MonoBehaviour
{
    
    /// <summary>
    /// Масса НИПа
    /// </summary>
    [SerializeField] private Rigidbody2D _rigidbody;

    /// <summary>
    /// Точка, где "стоит" НИП
    /// </summary>
    [SerializeField] private Transform _groundCheck;
    
    /// <summary>
    /// Фильтр на землю
    /// </summary>
    [SerializeField] private LayerMask _groundMask;
    
    /// <summary>
    /// Боковые границы НИПа
    /// </summary>
    [SerializeField] private Transform _groundBackCheck;
    
    /// <summary>
    /// Фильтр на стены
    /// </summary>
    [SerializeField] private LayerMask _groundBackMask;

    /// <summary>
    /// Скорость передвижения НИПа
    /// </summary>
    [SerializeField] private float _speed;

    /// <summary>
    /// Скорость передвижения НИПа
    /// </summary>
    private Vector2 _velocity;
    
    /// <summary>
    /// Направление движения НИПа
    /// </summary>
    private bool _facingRight;
    
    /// <summary>
    /// Смещение по горизонтали
    /// </summary>
    private float _horizontal;
    
    /// <summary>
    /// Смещение по вертикали
    /// </summary>
    private float _vertical;

    /// <summary>
    /// Инициализация начального состояния НИПа
    /// </summary>
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

    /// <summary>
    /// Расчет разворота НИПа
    /// </summary>
    void Update()
    {
        Flip();
    }

    /// <summary>
    /// Расчет движения НИПа
    /// </summary>
    private void FixedUpdate()
    {
        if (IsGrounded() && !IsBackGrounded())
            _horizontal *= -1;

        _rigidbody.velocity = new Vector2(_horizontal, _vertical);
    }

    /// <summary>
    /// Проверка нахождения НИПа на земле
    /// </summary>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, .2f, _groundMask);
    }

    /// <summary>
    /// Проверка нахождения НИПа у стены
    /// </summary>
    private bool IsBackGrounded()
    {
        return Physics2D.OverlapCircle(_groundBackCheck.position, .2f, _groundBackMask);
    }

    /// <summary>
    /// Разворот НИПа
    /// </summary>
    private void Flip()
    {
        if (_facingRight && _horizontal < .0f || !_facingRight && _horizontal > .0f)
        {
            _facingRight = !_facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            
        }
    }

    /// <summary>
    /// Взаимодействие красного НИПа с зеленым и желтым
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GrTag")
            StartCoroutine(UP());
        if (collision.tag == "YeTag")
            StartCoroutine(DOWN());
    }

    /// <summary>
    /// Подъем красного НИПа на 1 блок вверх
    /// </summary>
    IEnumerator UP()
    {
        float hSpeed = _horizontal;
        _horizontal = 0f;
        _vertical = _speed;
        yield return new WaitForSeconds(.2f);
        _vertical = 0f;
        _horizontal = hSpeed;
    }

    /// <summary>
    /// Спуск красного НИПа на 1 блок вниз
    /// </summary>
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
