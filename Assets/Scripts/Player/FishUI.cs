using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FishUI : MonoBehaviour
{
    [SerializeField] public string ownerName;
    FishMove owner;
    FishHP ownerHP;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider chargeSlider;
    [SerializeField] TextMeshProUGUI scoreText;
    private float targetHpValue = 1f;
    private int score = 0;
    private bool scored = false;

    void Awake()
    {
        DontDestroyOnLoad(transform.parent);
        SetSceneStartValues();
        SceneManager.sceneLoaded += SceneLoad;
    }

    private void SceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        SetSceneStartValues();
    }

    private void SetSceneStartValues()
    {
        owner = GameObject.Find(ownerName).GetComponent<FishMove>();
        ownerHP = owner.GetComponent<FishHP>();
        ownerHP.die.AddListener(OwnerDeath);

        scored = false;
        targetHpValue = 1f;
        hpSlider.value = targetHpValue;
    }

    void LateUpdate()
    {
        targetHpValue = GetHPPercentage();
        chargeSlider.value = GetPowerPercentage();
    }

    private void FixedUpdate()
    {
        if (Mathf.Approximately(hpSlider.value, targetHpValue))
            return;

        float change = (hpSlider.value - targetHpValue) / 10f;
        hpSlider.value = Mathf.MoveTowards(hpSlider.value, targetHpValue, change);
    }

    public void IncreaseScore()
    {
        if (scored)
            return;

        scored = true;
        score++;
        scoreText.text = score.ToString();
    }

    public void OwnerDeath()
    {
        foreach (FishUI ui in FindObjectsOfType<FishUI>())
        {
            if (ui.ownerName != ownerName)
            {
                ui.IncreaseScore();
                break;
            }
        }
    }

    private float GetPowerPercentage()
    {
        return owner.power / owner.maxPower;
    }

    private float GetHPPercentage()
    {
        return (float)ownerHP.health / (float)ownerHP.maxHealth;
    }
}
