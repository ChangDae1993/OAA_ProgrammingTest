using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private Player_Input input;

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

    //현재 애니메이션 처리가 무엇인지에 대한 변수
    private AnimState animState;
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
        if (input.xx == 0.0f)
        {
            animState = AnimState.Idle;
        }
        else
        {
            animState = AnimState.Run;

            transform.localScale = new Vector2(input.xx, 1);
        }

        //애니메이션 적용
        SetCurAnimation(animState);
    }

    private void FixedUpdate()
    {
        input.rigid.velocity = new Vector2(input.xx * 300 * Time.deltaTime, input.rigid.velocity.y);
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
        //짧게 작성한다면 이렇게
        AsncAnimation(AnimClip[(int)state], true, 1f);
    }
}
