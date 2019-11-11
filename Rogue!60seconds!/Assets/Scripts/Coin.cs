using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            gameObject.SetActive(false);
        }
    }
}
