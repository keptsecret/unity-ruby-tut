using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController controller = collision.collider.GetComponent<EnemyController>();
        if (controller != null)
        {
            controller.Fix();
        }

        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }
}
