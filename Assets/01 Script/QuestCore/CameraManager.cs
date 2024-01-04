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
    
    public void Awake()
    {
        Instance = this;
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        main = Camera.main;
        
    }

    private Sequence seq;

    public void CameraShake(float shakeScale = 1f, float randomness = 50f, int vibrato = 10, float time = 1f)
    {
        Vector3 currentPos = main.transform.position;

        Transform currentFollow = virtualCamera.Follow.transform; 
        Vector3 forwardPos =  (virtualCamera.transform.position.z * Vector3.forward);
        virtualCamera.Follow = null;
        
        virtualCamera.transform.parent.gameObject.SetActive(false);

        if(seq != null && seq.IsActive())
            seq.Complete();
        
        seq = DOTween.Sequence()
            .Append(main.transform.DOShakePosition(time, shakeScale, vibrato, randomness,
                randomnessMode: ShakeRandomnessMode.Harmonic))
            .AppendCallback(() =>
            {
                Transform followTrm = currentFollow;
                main.transform.DOMove( forwardPos + (Vector3)(Vector2)(followTrm.transform.position), 1f);
            })
            .AppendInterval(1f)
            .AppendCallback(() =>
            {
                virtualCamera.transform.parent.gameObject.SetActive(true);
                virtualCamera.Follow = currentFollow;
            });
    }

    public void ChangeTarget(Transform trm)
    {
        virtualCamera.Follow = trm;
    }
    
    
}
