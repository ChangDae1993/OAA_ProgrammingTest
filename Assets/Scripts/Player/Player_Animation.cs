using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private Player_Input input;

    //현재 애니메이션 처리가 무엇인지에 대한 변수
    [SerializeField] private AnimState animState;

    //스파인 애니메이션을 위한 변수 선언
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;


    //애니메이션에 대한 상태값
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

    //현재 어떤 애니메이션이 재생되고 있는지에 대한 변수
    private string CurAnim;


    private void Awake()
    {
        input = GetComponent<Player_Input>();
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
                //기본 idle 동작
                animState = AnimState.Idle;
            }
            else
            {
                animState = AnimState.Sit;
            }
        }
        else if(input.isSleep)
        {
            //잠들기 동작
            transform.localScale = new Vector2(input.xx + 1, 1);
            animState = AnimState.Sleep_Loop;
            //Debug.Log(animState);
        }
        else
        {
            if(!input.isCrawl)
            {
                //걷는 동작
                animState = AnimState.Run;
            }
            else
            {
                //기어가는 동작
                animState = AnimState.RunDown;
            }

            if(input.isRun)
            {
                animState = AnimState.RunFast;
            }

            transform.localScale = new Vector2(input.xx, 1);
        }



        if(input.isJump)
        {
            //점프 동작
            animState = AnimState.JumpUp;
        }
        else if(input.isJumpDown)
        {
            animState = AnimState.JumpDown;
        }

        //애니메이션 적용
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
        //동일한 애니메이션을 재생하려고 한다면 아래 코드를 return 한다.
        if (animClip.name.Equals(CurAnim))
            return;

        //해당 애니메이션으로 변경한다.
        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        //현재 재생되고 있는 애니메이션 값을 변경한다.
        CurAnim = animClip.name;
    }

    private void SetCurAnimation(AnimState state)
    {

        if(input.isCrawl && input.xx == 0)
        {
            AsncAnimation(AnimClip[(int)state], false, 1f);
        }
        else
        {
            AsncAnimation(AnimClip[(int)state], true, 1f);
        }
    }
}
