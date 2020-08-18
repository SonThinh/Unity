using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Kunai : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D r2;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        r2.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
