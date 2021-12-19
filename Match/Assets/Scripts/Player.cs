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
        if(IsPunching){
            if(PunchsRemaining > 0){
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.IsInTransition(0)){
                    animator.SetTrigger("Punch");
                    PunchsRemaining--;
                    Debug.Log("PUNCH");
                }
            }
            else{
                IsPunching = false;
                GameController.instance.FinishedPunching();
                animator.SetTrigger("FinishedPunching");
            }
        }
    }

    public void StartMoving(){
        Move(true);
    }

    private void Move(bool yORn) {
        IsMoving = yORn;
        animator.SetBool("IsRunning", yORn);
		Environment.instance.Move(yORn);
    }

    public void Punch(int numOfPunches) {
        PunchsRemaining = numOfPunches;
        IsPunching = true;
    }
}
