using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    //참조
    [SerializeField] private Player_Interaction interact;

    //좌우 회전
    public int key;

    //상호작용 처리 변수
    public bool isInteract;

    //움직임 처리 변수
    public Rigidbody2D rigid;
    public float xx;

    //잠드는 동작 관련 변수
    public float sleepTimer;
    public bool isSleep;

    //점프 관련 변수
    public bool isJump;
    public bool isJumpDown;
    RaycastHit2D rayHit;
    public string[] layernames;
    private float jump_Power = 5.0f;

    //앉기 관련 동작
    public bool isCrawl;

    //달리기 관려
    public bool isRun;

    //움직임 속도 관련
    public float walkSpeed;
    public float crawlSpeed;
    public float runSpeed;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        interact = GetComponent<Player_Interaction>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        key = 0;

        walkSpeed = 300;
        crawlSpeed = 150;
        runSpeed = 600;

        isSleep = false;

        isCrawl = false;
        isRun = false;
        sleepTimer = 10.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        xx = Input.GetAxisRaw("Horizontal");

        //좌우 회전 처리 변수
        if(xx < 0)
        {
            key = -1;
        }
        else if (xx > 0)
        {
            key = 1;
        }
        else
        {
            key = 0;
        }

        //잠들기 처리
        if (!Input.anyKeyDown && key == 0.0f)
        {
            sleepTimer -= Time.deltaTime;
        }
        else
        {
            sleepTimer = 10.0f;
        }

        if(sleepTimer <= 0.0f)
        {
            sleepTimer = 0.0f;
            isSleep = true;
        }
        else
        {
            isSleep = false;
        }

        //점프 처리
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(!isJump)
            {
                isJump = true;
                pJump();
            }
            isJump = true;
            //Debug.Log("점프");
        }

        //기어가기 처리
        if(Input.GetKey(KeyCode.DownArrow))
        {
            isCrawl = true;
        }
        else
        {
            isCrawl = false;
        }

        //달리기
        if (Input.GetKey(KeyCode.X))
        {
            if (isCrawl)
                return;
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        if(interact.isTouch)
        {
            if(Input.GetKey(KeyCode.X))
            {
                isInteract = true;
            }
            else
            {
                isInteract = false;
            }
        }
        else
        {
            isInteract = false;
        }
    }

    private void FixedUpdate()
    {
        rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask(layernames));

        if(rigid.velocity.y <=0)
        {
            //바닥감지를 위해 레이저를 쏜다
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    isJump = false;
                }
            }
        }
    }

    void pJump()
    {
        rigid.AddForce(transform.up * jump_Power, ForceMode2D.Impulse);
    }
}
