using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishMove : MonoBehaviour
{
    Rigidbody2D rb;
    FishMove enemy;
    FishHP hp;
    float input = 0f;

    public float maxPower = 10f;

    [SerializeField] WaterBomb bomb;
    [SerializeField] float speed;
    [SerializeField] AudioSource shootSFX;
    [SerializeField] GameObject bubbles;

    [HideInInspector] public float modifier = 1f;
    [HideInInspector] public float jumpPower = 1f;
    [HideInInspector] public float power = 1f;
    [HideInInspector] public bool inWater = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponent<FishHP>();
        enemy = GetEnemy();
    }

    void Start()
    {
        StartCoroutine(SpawnParticles());
    }

    private FishMove GetEnemy()
    {
        foreach (FishMove move in FindObjectsOfType<FishMove>())
        {
            if (move != this)
                return move;
        }
        return null;
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.right * input * speed * modifier, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        if (enemy != null)
        transform.right = enemy.transform.position - transform.position;

        power = Mathf.Min(power, maxPower);
    }

    public void GetMove(InputAction.CallbackContext callback)
    {
        input = callback.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (inWater)
            {
                rb.AddForce(Vector2.up * (10f + jumpPower), ForceMode2D.Impulse);
                jumpPower = 1f;
            }
        }
    }

    public void Shoot(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            ShootBomb();
        }
    }

    private void ShootBomb()
    {
        WaterBomb projectile = Instantiate(bomb, transform.position + transform.right * 3f, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce(transform.right * (10f + power), ForceMode2D.Impulse);
        projectile.SetRadius(power);
        projectile.SetOwner(hp);

        shootSFX.pitch = Mathf.Min(8f / power, 4f);
        shootSFX.Play();
        
        power = 1f;
    }

    IEnumerator SpawnParticles()
    {
        while (true)
        {
            if (inWater)
            {
                Instantiate(bubbles, transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(5f / (1f + power));
        }
    }
}
