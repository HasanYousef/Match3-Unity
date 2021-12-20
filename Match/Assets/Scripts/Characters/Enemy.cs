using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private bool IsPunching = false;
    private bool WaitToFinishLastPunch = false;
    [SerializeField] private int MaxPunchs  = 0;
    private int PunchsRemaining = 0;
    [SerializeField] private int Damage;
    [SerializeField] private int MaxHealth;
    private int Health;

    void Start()
    {
        animator = GetComponent<Animator>();
        Health = MaxHealth;
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
        PunchsRemaining = MaxPunchs;
        IsPunching = true;
    }
    public void Punched(int damage){
        animator.SetTrigger("Punched");
        Health -= damage;
        InGameUI.instance.UpdateEnemyHealth(Health);
    }
    public int GetHealth(){
        return Health;
    }

    public int GetDamage(){
        return Damage;
    }
}
