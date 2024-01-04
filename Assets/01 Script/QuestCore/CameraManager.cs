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
    public float baseCameraSize;
    
    public void Awake()
    {
        Instance = this;
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        main = Camera.main;

        baseCameraSize = virtualCamera.m_Lens.OrthographicSize;
    }

    private void Start()
    {
        followTrm = virtualCamera.Follow;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCameraSize(0.5f, 0.5f, AnimationCurve.Linear(0,0,1,1));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeCameraSize(1f, 0.5f, AnimationCurve.Linear(0,0,1,1));            
        }
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

    public void ChangeCameraSize(float size = 1, float time = 1f, AnimationCurve curve = null)
    {
        if(curve == null) return;
        
        DOTween.To(
                () => virtualCamera.m_Lens.OrthographicSize, x => virtualCamera.m_Lens.OrthographicSize = x,
                size * baseCameraSize, time)
            .SetEase(curve);
    }
    
}
