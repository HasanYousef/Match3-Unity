using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public static Environment instance;
    [SerializeField] private Street street;
    [SerializeField] private float RunningSpeed;
    private bool IsMoving;
    void Start(){
        instance = GetComponent<Environment>();
    }

    void Update(){
        if(IsMoving)
            transform.position -= new Vector3(RunningSpeed * Time.deltaTime, 0, 0);
    }

    public void Move(bool yORn){
        if(yORn)
            street.LoadChunks();
        IsMoving = yORn;
    }
}
