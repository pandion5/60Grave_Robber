using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAtoB : MonoBehaviour
{
    public GameObject mainCamera;
    public Transform TeleportPoint;
    public bool isMainTileMap;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Player"))
        {
            collision2D.gameObject.transform.position = TeleportPoint.position;
            mainCamera.GetComponent<CameraFollow>().bound = !isMainTileMap;
        }
    }
}
