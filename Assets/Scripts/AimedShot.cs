using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedShot : MonoBehaviour
{
    private GameObject character;
    private Rigidbody2D rb;
    public float speed;
    private Vector2 charPos;
    private Vector2 initPos;
    private Vector2 fromTo;
    private Vector2 shotVel;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Vector2 charPos = character.transform.position;
        Vector2 initPos = transform.position;
        Vector2 fromTo = charPos - initPos;
        fromTo.Normalize();
        Vector2 shotVel = fromTo * speed;
        rb.velocity = shotVel;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
