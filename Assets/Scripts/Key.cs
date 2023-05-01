using UnityEngine;

/// <summary>
/// Класс, описывающий объект ключа и механику взаимодействия с ключом
/// </summary>
public class Key : MonoBehaviour
{
    /// <summary>
    /// Указатель на игровой объект, который сейчас владеет ключом
    /// </summary>
    private GameObject _hold;
    
    /// <summary>
    /// Флаг указывающий поднял ли другой игровой объект ключ
    /// </summary>
    private bool _pick = false;

    /// <summary>
    /// Обновление положения ключа в пространтсве по положению объекта, владеющего ключом
    /// </summary>
    void FixedUpdate()
    {
        if (_pick)
            transform.position = _hold.transform.position;
    }

    /// <summary>
    /// Обработка процесса столкновение ключа и друогого игрового объекта, 
    /// статус объекта изменяется на поднятый, сохраняется ссылка на объект, владющий ключом 
    /// </summary>
    /// <param name="collision">Объект столкновения</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == "Green")
        {
            _pick = false;
            Debug.Log(11);
        }*/

        if (collision.tag == "Red")
        {
            _hold = collision.gameObject.transform.GetChild(2).gameObject;//.FindGameObjectWithTag("HoldKeyRed");
            _pick = true;
        }

    }
  

}
