using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс, описывающий логику взаимодействия с ключями и дверью
/// </summary>
public class Objectives : MonoBehaviour
{
    /// <summary>
    /// Следующий уровень, открывающийся после прохождения текущего
    /// </summary>
    [SerializeField] private string _nextLevel;

    /// <summary>
    /// Звук поднятия ключа
    /// </summary>
    [SerializeField] private AudioClip _keyUp;
    
    /// <summary>
    /// Хранилище звуков
    /// </summary>
    [SerializeField] private AudioSource _audioSource;
    
    /// <summary>
    /// Количество ключей
    /// </summary>
    [SerializeField] private int _keyCount;

    /// <summary>
    /// Переход на следующий уровень по нажатию клавиши N(отладка)
    /// </summary>
    private void Update()
    {
        if(Input.GetKey(KeyCode.N))
            SceneManager.LoadScene(_nextLevel);
    }

    /// <summary>
    /// Механика взаимодействия с ключом и дверью: ключи подбираются, если подобраны все ключи дверь открывается, 
    /// взаимодействие с открытой дверью переносит игрока на следующий уровень
    /// </summary>
    /// <param name="collision">Объект столкновения</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            _audioSource.PlayOneShot(_keyUp);

            _keyCount--;
            Destroy(collision.gameObject);

            if (_keyCount <= 0)
            {
                GameObject.FindGameObjectWithTag("CloseDoor").SetActive(false);

                GameObject.FindGameObjectWithTag("OpenDoor").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.FindGameObjectWithTag("OpenDoor").GetComponent<BoxCollider2D>().enabled = true;


        if (collision.tag == "OpenDoor")
        {
            SceneManager.LoadScene(_nextLevel);
        }
    }
    

}
