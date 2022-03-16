using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpeed : MonoBehaviour
{
    [ReadOnly] public float previousX;
    [ReadOnly] public float previousY;
    [ReadOnly] public float previousZ;
    [ReadOnly] public float velocityX;
    [ReadOnly] public float velocityY;
    [ReadOnly] public float velocityZ;
    [ReadOnly] public float speed;

    private void FixedUpdate()
    {
        velocityX = ((transform.position.x - previousX)) / Time.deltaTime;
        velocityY = ((transform.position.y - previousY)) / Time.deltaTime;
        velocityZ = ((transform.position.z - previousZ)) / Time.deltaTime;

        speed = Mathf.Abs(velocityX) + Mathf.Abs(velocityY) + Mathf.Abs(velocityZ);

        previousX = transform.position.x;
        previousY = transform.position.y;
        previousZ = transform.position.z;
    }
}
