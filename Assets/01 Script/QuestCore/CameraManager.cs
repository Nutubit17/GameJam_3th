using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    private CinemachineVirtualCamera _camera;
    private Camera main;
    
    public void Awake()
    {
        Instance = this;
        _camera = FindAnyObjectByType<CinemachineVirtualCamera>();
        main = Camera.main;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CameraShake(0.3f, 10f, 30);
        }
    }

    public void CameraShake(float shakeScale = 1f, float randomness = 50f, int vibrato = 10, float time = 1f)
    {
        Vector3 currentPos = main.transform.position;

        Transform currentFollow = _camera.Follow.transform; 
        Vector3 forwardPos =  (_camera.transform.position.z * Vector3.forward);
        _camera.Follow = null;
        
        _camera.transform.parent.gameObject.SetActive(false);

        DOTween.Sequence()
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
                _camera.transform.parent.gameObject.SetActive(true);
                _camera.Follow = currentFollow;
            });
    }

    public void ChangeTarget(Transform trm)
    {
        _camera.Follow = trm;
    }
    
    
}
