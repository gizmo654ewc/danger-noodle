using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassThruPlatforms : MonoBehaviour
{
    private GameObject currentPlat;
    [SerializeField] private BoxCollider2D playerCollider;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPlat != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlat = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlat = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentPlat.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(.6f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
