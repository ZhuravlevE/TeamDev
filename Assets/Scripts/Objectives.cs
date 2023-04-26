using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectives : MonoBehaviour
{
    [SerializeField] private string _nextLevel;

    [SerializeField] private AudioClip _keyUp;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private int _keyCount;


    private void Update()
    {
        if(Input.GetKey(KeyCode.N))
            SceneManager.LoadScene(_nextLevel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.tag == "OpenDoor")
        {
            SceneManager.LoadScene(_nextLevel);
        }
    }
    

}
