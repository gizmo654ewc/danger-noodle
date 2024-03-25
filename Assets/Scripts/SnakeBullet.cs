using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float shotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void ShootRight()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * shotSpeed;
    }
    public void ShootLeft()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * shotSpeed;
    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
