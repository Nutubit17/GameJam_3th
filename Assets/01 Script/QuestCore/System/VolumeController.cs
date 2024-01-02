using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    public static VolumeController Instance;

    public VolumeProfile _Profile;

    public Bloom bloom;
    public Vignette vignette;
    public FilmGrain filmGrain;
    
    private void Awake()
    {
        Instance = this;

        _Profile = GetComponent<Volume>().profile;

        _Profile.TryGet(out bloom);
        _Profile.TryGet(out vignette);
        _Profile.TryGet(out filmGrain);

    }

    public IEnumerator BloomIntensityTween(AnimationCurve curve, float endValue = 1f, float time = 1f)
    {
        float startValue = bloom.intensity.GetValue<float>();
        float percent = 0;

        while (percent <= 1)
        {
            bloom.intensity.value = Mathf.Lerp(startValue, endValue, curve.Evaluate(percent));
            
            percent += Time.deltaTime / time;
            yield return null;
        }
    }
    
    public IEnumerator VignetteTween(AnimationCurve curve, Vector3 endPos, float endValue = 1f, float time = 1f)
    {
        float startValue = vignette.intensity.GetValue<float>();
        Vector2 startPos = vignette.center.GetValue<Vector2>();
        float percent = 0;

        while (percent <= 1)
        {
            vignette.intensity.value = Mathf.Lerp(startValue, endValue, curve.Evaluate(percent));
            vignette.center.value = Vector2.Lerp(startPos, endPos, curve.Evaluate(percent));
            
            percent += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FilmGrainTween(AnimationCurve curve, float endValue = 1f, float time = 1f)
    {
        float startValue = filmGrain.intensity.GetValue<float>();
        float percent = 0;

        while (percent <= 1)
        {
            filmGrain.intensity.value = Mathf.Lerp(startValue, endValue, curve.Evaluate(percent));
            
            percent += Time.deltaTime / time;
            yield return null;
        }
    }

}
