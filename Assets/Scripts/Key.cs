using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject _hold;

    public bool _pick { get; private set; }

    private void Start()
    {
        _pick = false;
    }

    void FixedUpdate()
    {
        if (_pick)
            transform.position = _hold.transform.position;
    }

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
