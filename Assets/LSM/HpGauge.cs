using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpGauge : MonoBehaviour
{

    public float Rate
    {
        set
        {
            baseTrm.localScale = new Vector3(Mathf.Clamp(value,0,1), 1, 1);
        }

        get
        {
            return baseTrm.localScale.x;
        }
    }


    Transform baseTrm;

    private void Awake()
    {
        baseTrm = transform.Find("HpBase/Base");
    }


}
