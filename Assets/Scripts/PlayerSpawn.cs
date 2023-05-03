using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    void Start()
    { 
        Instantiate(_player, transform.position, Quaternion.identity);

    }

}
