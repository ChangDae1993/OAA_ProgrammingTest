using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private Player_Input input;

    //������ �ִϸ��̼��� ���� ���� ����
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

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

    //���� �ִϸ��̼� ó���� ���������� ���� ����
    private AnimState animState;
    //���� � �ִϸ��̼��� ����ǰ� �ִ����� ���� ����
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

        //�ִϸ��̼� ����
        SetCurAnimation(animState);
    }

    private void FixedUpdate()
    {
        input.rigid.velocity = new Vector2(input.xx * 300 * Time.deltaTime, input.rigid.velocity.y);
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
        //ª�� �ۼ��Ѵٸ� �̷���
        AsncAnimation(AnimClip[(int)state], true, 1f);
    }
}
