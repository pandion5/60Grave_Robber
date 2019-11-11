using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTimeX, smoothTimeY;
    public float smoothTime = 0.125f;
    public Vector2 velocity;
    public GameObject player;
    public Vector2 minPos, maxPos;
    public bool bound;
    float posX;
    float posY ;

    Vector3 desiredPosition;
    Vector3 smoothedPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        if(player.transform.localScale.x < 0)
            posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x-2.5f, ref velocity.x, smoothTimeX);
        else
            posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x+2.5f, ref velocity.x, smoothTimeX);
            
        posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX,posY,transform.position.z);
        */
        if(player.transform.localScale.x < 0)
            desiredPosition = new Vector3(player.transform.position.x-2.5f,player.transform.position.y,transform.position.z);
        else
            desiredPosition = new Vector3(player.transform.position.x+2.5f,player.transform.position.y,transform.position.z);
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
        transform.position = smoothedPosition;
        if(bound){
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y,minPos.y,maxPos.y),Mathf.Clamp(transform.position.z,transform.position.z,transform.position.z));
        }
    }
}
