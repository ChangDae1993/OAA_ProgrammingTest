using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private Player_Input input;
    [SerializeField] private Player_Interaction interaction;

    //���� �ִϸ��̼� ó���� ���������� ���� ����
    [SerializeField] private AnimState animState;

    //������ �ִϸ��̼��� ���� ���� ����
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    //ĳ���� ȸ���� ���� ��
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isRight;


    //�ִϸ��̼ǿ� ���� ���°�
    public enum AnimState
    {
        Cutting,
        Idle,
        JumpDown,
        JumpUp,
        Landing,
        Pull,
        Push,
        Run,
        RunDown,
        RunFast,
        Sit,
        Sleep_Loop,
        StandUp,
        Turn_Reverse,
        Turn,
    }

    //���� � �ִϸ��̼��� ����ǰ� �ִ����� ���� ����
    private string CurAnim;


    private void Awake()
    {
        input = GetComponent<Player_Input>();
        interaction = GetComponent<Player_Interaction>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (input.xx == 0.0f && !input.isSleep)
        {

            if (!input.isCrawl)
            {
                //�⺻ idle ����
                animState = AnimState.Idle;

            }
            else
            {
                animState = AnimState.Sit;
            }
        }
        else if(input.isSleep)
        {
            //���� ����
            transform.localScale = new Vector2(1, 1);
            animState = AnimState.Sleep_Loop;
            //Debug.Log(animState);
        }
        else
        {
            if(!input.isCrawl)
            {
                //�ȴ� ����
                animState = AnimState.Run;
            }
            else
            {
                //���� ����
                animState = AnimState.RunDown;
            }

            if(input.isRun)
            {
                animState = AnimState.RunFast;
            }

            if (input.key == 1)
            {
                isRight = true;
                isLeft = false;
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                //animState = AnimState.Turn;
            }
            else if (input.key == -1)
            {
                isRight = false;
                isLeft = true;
                this.transform.localEulerAngles = new Vector3(0, 180, 0);
                //animState = AnimState.Turn_Reverse;
            }
        }

        //�а� ���� ����
        if(input.isInteract)
        {
            if (input.xx > 0)
            {
                animState = AnimState.Push;
            }
            else if (input.xx < 0)
            {
                animState = AnimState.Pull;
            }
        }


        if(input.isJump)
        {
            //���� ����
            animState = AnimState.JumpUp;
        }
        else if(input.isJumpDown)
        {
            animState = AnimState.JumpDown;
        }

        //����
        if(interaction.isTrap)
        {
            animState = AnimState.Cutting;
        }

        //�ִϸ��̼� ����
        SetCurAnimation(animState);
    }

    private void FixedUpdate()
    {
        if(input.isRun)
        {
            input.rigid.velocity = new Vector2(input.xx * input.runSpeed * Time.deltaTime, input.rigid.velocity.y);
        }
        else if(input.isCrawl)
        {
            input.rigid.velocity = new Vector2(input.xx * input.crawlSpeed * Time.deltaTime, input.rigid.velocity.y);
        }
        else
        {
            input.rigid.velocity = new Vector2(input.xx * input.walkSpeed * Time.deltaTime, input.rigid.velocity.y);
        }
    }

    private void AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        //������ �ִϸ��̼��� ����Ϸ��� �Ѵٸ� �Ʒ� �ڵ带 return �Ѵ�.
        if (animClip.name.Equals(CurAnim))
            return;

        //�ش� �ִϸ��̼����� �����Ѵ�.
        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        //���� ����ǰ� �ִ� �ִϸ��̼� ���� �����Ѵ�.
        CurAnim = animClip.name;
    }

    private void SetCurAnimation(AnimState state)
    {

        if(input.isCrawl && input.xx == 0 || interaction.isTrap)
        {
            AsncAnimation(AnimClip[(int)state], false, 1f);
        }
        else
        {
            AsncAnimation(AnimClip[(int)state], true, 1f);
        }
    }
}
