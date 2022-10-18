using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    public bool isTouch;

    // Start is called before the first frame update
    void Start()
    {
        isTouch = false;   
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
}
