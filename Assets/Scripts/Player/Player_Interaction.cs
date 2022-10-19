using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    [SerializeField] private Player_Input input;

    public bool isTouch;
    public bool isTrap;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<Player_Input>();
        isTouch = false;
        isTrap = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("InterAction"))
        {
            isTouch = true;
            Debug.Log("´êÀ½");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("InterAction"))
        {
            isTouch = false;
            Debug.Log("¶³¾îÁü");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            input.xx = 0.0f;
            input.enabled = false;
            isTrap = true;
            Debug.Log("Á×À½");
        }
    }
}
