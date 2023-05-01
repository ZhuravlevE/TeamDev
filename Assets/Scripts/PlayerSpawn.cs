using UnityEngine;

/// <summary>
/// Класс, описывающий игрового персонажа
/// </summary>
public class PlayerSpawn : MonoBehaviour
{
    /// <summary>
    /// Игровой персонаж
    /// </summary>
    [SerializeField] private GameObject _player;

    /// <summary>
    /// Появление игрового персонажа на уровне
    /// </summary>
    void Start()
    { 
        Instantiate(_player, transform.position, Quaternion.identity);

    }

}
