using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public CinemachineVirtualCamera virtualCamera;
    public Camera main;

    public Transform followTrm;
    
    public void Awake()
    {
        Instance = this;
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        main = Camera.main;
        
    }

    private void Start()
    {
        followTrm = virtualCamera.Follow;
    }

    public void CameraShake(float shakeScale = 1f, float randomness = 50f, int vibrato = 10, float time = 1f)
    {

        Vector3 forwardPos =  (virtualCamera.transform.position.z * Vector3.forward);
        followTrm = virtualCamera.Follow; 
        
        virtualCamera.transform.parent.gameObject.SetActive(false);

        DOTween.Sequence()
            .Append(main.transform.DOShakePosition(time, shakeScale, vibrato, randomness,
                randomnessMode: ShakeRandomnessMode.Harmonic))
            .AppendCallback(() => ResetFollowTrm(forwardPos));
    }

    private void ResetFollowTrm(Vector3 forwardPos)
    {
        virtualCamera.transform.parent.gameObject.SetActive(true);
        main.transform.position = forwardPos + (Vector3)(Vector2)followTrm.position;
    }

    public void ChangeTarget(Transform trm)
    {
        virtualCamera.Follow = trm;
    }
    
    
}
