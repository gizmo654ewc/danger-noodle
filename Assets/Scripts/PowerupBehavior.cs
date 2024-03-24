using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    public AnimationCurve myCurve;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + 26, transform.position.z);
    }
}
