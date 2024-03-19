using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRestrict : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 7)
        {
            transform.Translate(new Vector2(transform.position.x, 7));
        }
    }
}
