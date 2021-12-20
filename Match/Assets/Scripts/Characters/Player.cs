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
    private int PunchsRemaining = 0;
    [SerializeField] private int Damage;
    [SerializeField] private int MaxHealth;
    private int Health;
    void Start()
    {
        instance = GetComponent<Player>();
        animator = GetComponent<Animator>();
        Health = MaxHealth;
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
                    animator.SetTrigger("Punch" + Random.Range(1, 3));
                    GameController.instance.CurrentEnemy.Punched(Damage);
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
    public void Punched(int damage){
        animator.SetTrigger("Punched");
        Health -= damage;
        InGameUI.instance.UpdatePlayerHealth(Health);
    }

    public int GetHealth(){
        return Health;
    }

    public int GetDamage(){
        return Damage;
    }
}
