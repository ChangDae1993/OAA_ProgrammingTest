using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpin_Ctrl : MonoBehaviour
{
    public Transform Trap;
    [SerializeField] private bool isUp;
    [SerializeField] private bool isStop;

    [SerializeField] private float Timer;
    [SerializeField] private Vector2 upTarget;
    [SerializeField] private float upSpeed;

    //private int a_key = 1;

    private float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        upSpeed = 1f;
        isUp = true;
        isStop = false;
        Timer = 0.0f;

        rotSpeed = 800.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Trap.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));

        //if (isUp && a_key == -1)
        //    a_key = 1;
        //else if(!isUp && a_key == 1)
        //    a_key = -1;

        if(!isStop)
        {
            if(isUp)
            {
                Trap.localPosition = new Vector2(0, Trap.localPosition.y + upSpeed * Time.deltaTime);
            }
            else
            {
                Trap.localPosition = new Vector2(0, Trap.localPosition.y - upSpeed * Time.deltaTime);
            }
        }

        if (!isStop && (Trap.localPosition.y >= 1.5f || Trap.localPosition.y < -0.5f))
        {
            Timer = 3.0f;
            isStop = true;
        }

        if (0.0f < Timer)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0.0f)
            {
                Timer = 0.0f;
                isUp = !isUp;
                isStop = false;
            }
        }
    }
}
