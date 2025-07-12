using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    private bool idle;
    private bool move;
    private bool attack;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        SetIdle();
    }

    void Update()
    {
        AnimationController();
    }

    private void AnimationController()
    {
        playerAnimator.SetBool("Idle", idle);
        playerAnimator.SetBool("Move", move);
        playerAnimator.SetBool("Attack", attack);
    }


    public void SetIdle()
    {
        idle = true;
        move = false;
        attack = false;
    }

    public void SetMove()
    {
        idle = false;
        move = true;
        attack = false;
    }

    public void SetAttack()
    {
        idle = false;
        move = false;
        attack = true;
    }
}
