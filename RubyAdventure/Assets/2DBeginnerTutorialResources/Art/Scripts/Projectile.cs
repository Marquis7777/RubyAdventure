using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }

        BatEnemy b = other.collider.GetComponent<BatEnemy>();
        if (b != null)
        {
            b.Die();
        }

        BossScript b = other.collider.GetComponent<BossScript>();
        if (b != null)
        {
            b.Death();
        }

        SlimeScriptPart2 s = other.collider.GetComponent<SlimeScriptPart2>();
        if (s != null)
        {
            s.SlimeDeath();
        }

        Destroy(gameObject);
    }
}