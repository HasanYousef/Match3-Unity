using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] float StopDistance;
    private Animator animator;
    private bool IsMoving = false;
    private bool IsPunching = false;
    private bool WaitToFinishLastPunch = false;
    private float RotationDegreesRemaining = 0f;
    private int PunchsRemaining = 0;
    void Start()
    {
        instance = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(IsMoving){
            Ray look = new Ray(transform.position, new Vector3(1, 0, 0));
            RaycastHit hit;
            if(Physics.Raycast(look, out hit, StopDistance)){
                Move(false);
                GameController.instance.FinishedMoving();
            }
        }
        else if(IsPunching){
            if(PunchsRemaining > 0){
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.IsInTransition(0)){
                    SFXManager.instance.PlaySFX(Clip.Punch);
                    animator.SetTrigger("Punch");
                    PunchsRemaining--;
                }
            }
            else{
                IsPunching = false;
                WaitToFinishLastPunch = true;
                GameController.instance.FinishedPunching();
                animator.SetTrigger("FinishedPunching");
            }
        }
        else if(WaitToFinishLastPunch && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.IsInTransition(0)){
            RotationDegreesRemaining = 90;
            WaitToFinishLastPunch = false;
        }
        else if(RotationDegreesRemaining > 0){
            float rotated = 600f * Time.deltaTime;
            RotationDegreesRemaining -= rotated;
            transform.Rotate(0, rotated, 0);
        }
    }

    public void StartMoving(){
        Move(true);
    }

    private void Move(bool yORn) {
        IsMoving = yORn;
        animator.SetBool("IsRunning", yORn);
		Environment.instance.Move(yORn);
        if(!yORn)
            transform.Rotate(0, 90, 0);
    }

    public void Punch(int numOfPunches) {
        transform.Rotate(0, -90, 0);
        PunchsRemaining = numOfPunches;
        IsPunching = true;
    }
}
