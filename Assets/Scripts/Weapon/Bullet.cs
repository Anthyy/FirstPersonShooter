using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10f;
    public GameObject effectsPrefab;
    public Transform line;

    private Rigidbody rigid;

    void Awake()
    {
        // Get component on awake so we don't miss it if it starts disabled
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rigid.velocity.magnitude > 0)
        {
            // Rotate the line to face direction of bullet travel
            line.transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // Get contact point from collision
        ContactPoint contact = col.contacts[0];
        // Spawn the Effect (i.e, Bullet Hole / Sparks)
        //Instantiate(effectsPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        Enemy enemy = col.collider.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
        // Destroy the bullet
        Destroy(gameObject);
    }

    public void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        // Add an instant force to the bullet
        rigid.AddForce(direction * speed, ForceMode.Impulse);
        // Set the line's origin (different from the bullet's starting position)
        line.transform.position = lineOrigin;
    }
}
