using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBall : MonoBehaviour
{
    float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = Random.Range(400f,600f);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward*speed);
    }
}
