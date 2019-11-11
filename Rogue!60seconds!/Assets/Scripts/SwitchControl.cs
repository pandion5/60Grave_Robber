using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    public DoorSwitch DoorSelect;
    public bool isOn = false;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            animator.SetBool("IsOn",true);
            DoorSelect.OpenState(true);
        }
        else
        {
            animator.SetBool("IsOn",false);
            DoorSelect.OpenState(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Collider Detected");
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Key Detected");
            if(isOn == false)
                isOn = true;
            else
                isOn = false;
        }
    }
}
