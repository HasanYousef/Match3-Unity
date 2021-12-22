using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private CinemachineVirtualCamera CVC;
    [SerializeField] private float Intensity;
    //[SerializeField] private float ShakeTime;
    //private float ShakeTimer;
    void Awake()
    {
        instance = this;
        CVC = GetComponent<CinemachineVirtualCamera>();
    }

    /*void Update()
    {
        if(ShakeTimer > 0){
            ShakeTimer -= Time.deltaTime;
            if(ShakeTimer <= 0f){
                CinemachineBasicMultiChannelPerlin anotherShit = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                anotherShit.m_AmplitudeGain = 0f;
            }
        }
    }*/

    public void ShakeThatAss(bool yesORno) {
        CinemachineBasicMultiChannelPerlin shit = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shit.m_AmplitudeGain = yesORno ? Intensity : 0;
        //ShakeTimer = ShakeTime;
    }

}
