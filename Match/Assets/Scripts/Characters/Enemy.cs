using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private bool IsPunching = false;
    private bool WaitToFinishLastPunch = false;
    private int PunchsRemaining = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(IsPunching){
            if(PunchsRemaining > 0){
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.IsInTransition(0)){
                    SFXManager.instance.PlaySFX(Clip.Punch);
                    animator.SetTrigger("Punch" + Random.Range(1, 3));
                    PunchsRemaining--;
                }
            }
            else{
                IsPunching = false;
                WaitToFinishLastPunch = true;
                GameController.instance.FinishedPunching();
            }
        }
        else if(WaitToFinishLastPunch && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.IsInTransition(0)){
            WaitToFinishLastPunch = false;
        }
    }

    public void Punch(int numOfPunches) {
        PunchsRemaining = 2;
        IsPunching = true;
    }
    public void Punched(){
        Debug.Log("ssssss");
        animator.SetTrigger("Punched");
    }
}
