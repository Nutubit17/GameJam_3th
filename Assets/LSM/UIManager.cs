using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Image playerHpbar;

    public void Hpdown(float damage)
    {
        Debug.Log(damage);
        playerHpbar.fillAmount -= damage * 0.01f;
    }

}
