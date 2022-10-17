using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    //움직임 처리 변수
    public Rigidbody2D rigid;
    public float xx;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        xx = Input.GetAxisRaw("Horizontal");
    }
}
