using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    float explosionRadius;
    FishHP owner;

    public void SetRadius(float radius)
    {
        explosionRadius = radius;
        transform.GetChild(0).localScale = Vector3.one * radius;
    }

    public void SetOwner(FishHP owner)
    {
        this.owner = owner;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(transform.position, explosionRadius))
        {
            if (collider.TryGetComponent<FishHP>(out var hp))
            {
                if (hp != owner)
                    hp.TakeDamage((int)explosionRadius);

                Vector2 distance = hp.transform.position - transform.position;

                if (distance.magnitude > 0f)
                {
                    float force = explosionRadius / distance.magnitude;
                    hp.GetComponent<Rigidbody2D>().AddForce(distance.normalized * force * 200f);
                }
            }
        }

        if (collision.gameObject.TryGetComponent<FishHP>(out var hp2))
        {
            if (hp2 != owner)
                Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
