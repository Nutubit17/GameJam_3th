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
    public Image playerStaminaBar;

    public void Hpdown(float damage)
    {
        playerHpbar.fillAmount -= damage * 0.01f;
    }

    public void Staminadown(float damage)
    {
        playerStaminaBar.fillAmount -= damage * 0.01f;
    }

    private void Update()
    {
        playerStaminaBar.fillAmount += 0.01f * Time.deltaTime;
        playerStaminaBar.fillAmount = Mathf.Clamp(playerStaminaBar.fillAmount, 0, 1);
    }

}
