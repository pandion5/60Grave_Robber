using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    float upMax;
    float originPos = 0f;
    float currentPos;
    float dir = 5.0f;
    float vibration_width;
    void Start()
    {
        currentPos = transform.position.y;
        upMax = currentPos + 0.7f;
        originPos = currentPos;
        vibration_width = Random.Range(3.0f,6.0f);
    }
    void Update()
    {
        currentPos += Time.deltaTime * dir;
        if(currentPos >= upMax)
        {
            dir *= -1 / vibration_width;
            currentPos = upMax;
        }
        else if(currentPos <= originPos)
        {
            dir *= -1 * vibration_width;
        }
        transform.position = new Vector3(transform.position.x,currentPos,transform.position.z);
        
    }
}
