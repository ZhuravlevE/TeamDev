using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Класс, описывающий создание персонажем НИПов и общие механики
/// </summary>
public class SpawnPhantom : MonoBehaviour
{
    /// <summary>
    /// Персонаж
    /// </summary>
    [SerializeField] private PlayerMovement _player;
    
    /// <summary>
    /// Расположение красного НИПа
    /// </summary>
    [SerializeField] private Transform _spawnRedTransform;
    
    /// <summary>
    /// Стек доступных НИПов
    /// </summary>
    [SerializeField] private GameObject[] _phantoms;
    
    /// <summary>
    /// Количетсов НИПов
    /// </summary>
    [SerializeField] private int _spawnCount;

    /// <summary>
    /// Интерфейс
    /// </summary>
    [SerializeField] private GameObject _ui;
    
    /// <summary>
    /// Контейнер
    /// </summary>
    [SerializeField] private GameObject _Container;

    /// <summary>
    /// Хранилище звуков
    /// </summary>
    [SerializeField] private AudioSource _audioSource;
    
    /// <summary>
    /// Звук старта
    /// </summary>
    [SerializeField] private AudioClip _start;
    
    /// <summary>
    /// Звук появления НИПа
    /// </summary>
    [SerializeField] private AudioClip _spawn;

    /// <summary>
    /// Флаг ввода
    /// </summary>
    private bool _enableInput = true;
    
    /// <summary>
    /// Флаг кнопки рестарта
    /// </summary>
    private bool _restartButton = false;
    
    /// <summary>
    /// Флаг кнопки создания НИПа
    /// </summary>
    private bool _spawnButton = false;

    /// <summary>
    /// (Отладка)
    /// </summary>
    private void Awake()
    {
        //for (int i = 0; i < _spawnCount; i++)
        //    Instantiate(_ui, _Container.transform);
    }

    /// <summary>
    /// Инициализация стека НИПов
    /// </summary>
    private void Start()
    {
        _audioSource.PlayOneShot(_start);
        _spawnCount = _phantoms.Length;
    }

    /// <summary>
    /// Перезапуск уровня и выход из приложения
    /// </summary>
    void Update()
    {
        if (_enableInput = _player.GetEnableInput())
            GetInput();

        Restart();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }
    
    /// <summary>
    /// Перезапуск уровня
    /// </summary>
    private void Restart()
    {
        if(Input.GetKey(KeyCode.R) || _restartButton)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Считывание нажатяи клавишы создания НИПа
    /// </summary>
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || _spawnButton)
        {
            Spawn();
            _spawnButton = false;
        }
    }

    /// <summary>
    /// Появление НИПов
    /// </summary>
    private void Spawn()
    {
        if(_spawnCount > 0)
        {
            _audioSource.PlayOneShot(_spawn);

            Instantiate(_phantoms[_spawnCount - 1],_spawnRedTransform.position, Quaternion.identity);
            _spawnCount--;
            Destroy(GameObject.FindGameObjectWithTag("Container").transform.GetChild(1).gameObject);
        }
    }

    /// <summary>
    /// Нажатие кнопки рестарта уровня
    /// </summary>
    public void RestartButtonDown()
    {
        _restartButton = true;
    }

    /// <summary>
    /// Нажатие кнопки создания НИПа
    /// </summary>
    public void SpawnButtonDown()
    {
        _spawnButton = true;
    }
    
    /// <summary>
    /// Отжатие кнопки создания НИПа
    /// </summary>
    public void SpawnButtonUp()
    {
        _spawnButton = false;
    }
}
