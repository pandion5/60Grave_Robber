using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    private bool isOpen = false;
    private Animator animator;
    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen)
            animator.SetBool("IsOpen",true);
        else
            animator.SetBool("IsOpen",false);
    }

    public void OpenState(bool State)
    {
        isOpen = State;
        coll.enabled = !State;
    }
}
