using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnPhantom : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Transform _spawnRedTransform;
    [SerializeField] private GameObject[] _phantoms;
    [SerializeField] private int _spawnCount;

    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _Container;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _start;
    [SerializeField] private AudioClip _spawn;

    private bool _enableInput = true;
    private bool _restartButton = false;
    private bool _spawnButton = false;

    private void Awake()
    {
        //for (int i = 0; i < _spawnCount; i++)
        //    Instantiate(_ui, _Container.transform);
    }

    private void Start()
    {
        _audioSource.PlayOneShot(_start);
        _spawnCount = _phantoms.Length;
    }

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
    
    private void Restart()
    {
        if(Input.GetKey(KeyCode.R) || _restartButton)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || _spawnButton)
        {
            Spawn();
            _spawnButton = false;
        }
    }

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

    public void RestartButtonDown()
    {
        _restartButton = true;
    }

    public void SpawnButtonDown()
    {
        _spawnButton = true;
    }
    public void SpawnButtonUp()
    {
        _spawnButton = false;
    }
}
