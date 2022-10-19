using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    //����
    [SerializeField] private Player_Interaction interact;

    //�¿� ȸ��
    public int key;

    //��ȣ�ۿ� ó�� ����
    public bool isInteract;

    //������ ó�� ����
    public Rigidbody2D rigid;
    public float xx;

    //���� ���� ���� ����
    public float sleepTimer;
    public bool isSleep;

    //���� ���� ����
    public bool isJump;
    public bool isJumpDown;
    RaycastHit2D rayHit;
    public string[] layernames;
    private float jump_Power = 5.0f;

    //�ɱ� ���� ����
    public bool isCrawl;

    //�޸��� ����
    public bool isRun;

    //������ �ӵ� ����
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

        //�¿� ȸ�� ó�� ����
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

        //���� ó��
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

        //���� ó��
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(!isJump)
            {
                isJump = true;
                pJump();
            }
            isJump = true;
            //Debug.Log("����");
        }

        //���� ó��
        if(Input.GetKey(KeyCode.DownArrow))
        {
            isCrawl = true;
        }
        else
        {
            isCrawl = false;
        }

        //�޸���
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
            //�ٴڰ����� ���� �������� ���
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
