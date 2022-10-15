using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishHP : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] AudioSource hitSFX;
    [HideInInspector] public int maxHealth;
    FishMove move;
    [HideInInspector] public UnityEvent die;

    bool dead = false;

    private void Awake()
    {
        move = GetComponent<FishMove>();
        maxHealth = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        move.power = 1f;
        hitSFX.Play();

        if (health <= 0)
            GoDie();
    }

    private void GoDie()
    {
        if (dead)
            return;

        dead = true;
        die.Invoke();
        Destroy(gameObject);
    }
}
