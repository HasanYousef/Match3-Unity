using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public static Environment instance;
    [SerializeField] private float RunningSpeed;
    [SerializeField] private List<Enemy> Enemies;
    private bool IsMoving;
    void Start(){
        instance = GetComponent<Environment>();
    }

    void Update(){
        if(IsMoving)
            transform.position -= new Vector3(RunningSpeed * Time.deltaTime, 0, 0);
    }

    public void Move(bool yORn){
        IsMoving = yORn;
    }

    public Enemy CurrentEnemy(){
        return Enemies.Count > 0 ? Enemies[0] : null;
    }

    public void CurrentEnemyDied(){
        Enemies.RemoveAt(0);
        GameController.instance.EnemyDied();
    }
}
