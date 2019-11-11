using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //레이어 저장
    private int playerLayer, wallLayer, platformLayer;
    bool jumpOffCoroutineIsRunning = false;
    //충돌
    private CollisionSphere coll;
    private Rigidbody2D rb;

    //애니메이션
    private Animator anim;
    private bool isSit = false;
    private bool isPlayed = false;
    //무브먼트
    public float speed = 280;
    public float jumpForce = 310;

    //모바일 버튼
    private int move;
    private int jump;
    private int sit;
    private int get;
    private GameObject obj_inter;

    //음악
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioClip sfx_jump;
    public AudioClip sfx_dead;
    public AudioClip sfx_open;
    public AudioClip sfx_close;
    public AudioClip sfx_coin;
    public AudioClip sfx_win;
    // Start is called before the first frame update
    void Start()
    {
        move = 0;

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CollisionSphere>();
        anim = GetComponentInChildren<Animator>();

        playerLayer = LayerMask.NameToLayer("Player");
        wallLayer = LayerMask.NameToLayer("Wall");
        platformLayer = LayerMask.NameToLayer("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGameover)
        {
            //PC조작
            ////달리기
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(x,y);
            if(!isSit)
                Run(dir);

            ////점프
            if(Input.GetKeyDown(KeyCode.X) || jump != 0)
            {
                if(coll.onGround)
                {
                    Jump();
                    sfx.PlayOneShot(sfx_jump);
                }
            }
            else if((Input.GetKeyUp(KeyCode.X) || jump == 0) && rb.velocity.y > 0)
            {
                if(!coll.onGround )
                {
                    rb.velocity *= 0.5f;
                }
            }

            ////앉기
            if(Input.GetKeyDown(KeyCode.C) || sit != 0)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                isSit = true;
                anim.SetBool("Sit",true);
                StartCoroutine("JumpOff");
            }
            else
            {
                isSit = false;
                anim.SetBool("Sit",false);
            }
            //플랫폼 레이어 제어
            if(rb.velocity.y > 0)
                Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
            if(rb.velocity.y <= 0 && !jumpOffCoroutineIsRunning)
                Physics2D.IgnoreLayerCollision(playerLayer,platformLayer, false);
        }
        else
        {
            Die();
            return;     
        }
    }
    IEnumerator JumpOff()
    {
        jumpOffCoroutineIsRunning = true;
        Physics2D.IgnoreLayerCollision(playerLayer,platformLayer,true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(playerLayer,platformLayer,false);
        jumpOffCoroutineIsRunning = false;
    }
    private void Run(Vector2 dir)
    {
        Vector3 characterScale = transform.localScale;
        if(Input.GetAxis("Horizontal") < 0 || move < 0)
        {
            characterScale.x = -2;
            
                
            if(!coll.onLeftWall)
                if(move < 0)
                    rb.velocity = new Vector2(move * speed * Time.deltaTime, rb.velocity.y);
                else
                    rb.velocity = new Vector2(dir.x * speed * Time.deltaTime, rb.velocity.y);
        }
        if(Input.GetAxis("Horizontal") > 0 || move > 0)
        {
            characterScale.x = 2;
            if(!coll.onRightWall)
                if(move > 0)
                    rb.velocity = new Vector2(move * speed * Time.deltaTime, rb.velocity.y);
                else
                    rb.velocity = new Vector2(dir.x * speed * Time.deltaTime, rb.velocity.y);
        }
        transform.localScale = characterScale;

        RunAnim();
    }
    


    private void RunAnim()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || move != 0)
        {
            anim.SetBool("Run",true);
        }
        else
        {
            anim.SetBool("Run",false);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce * Time.deltaTime;
        anim.SetBool("Jump",true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(rb.velocity.y == 0 || other.gameObject.layer == wallLayer || other.gameObject.layer == platformLayer)
        {
            anim.SetBool("Jump",false);
        }
    }

     //모바일 버튼 식별 메소드
    public void OnMove(int n)
    {
        //이동
        move=n;
    }

    public void OnJump(int n)
    {
        jump = n;
    }

    public void OnSit(int n)
    {
        sit = n;
    }

    public void GetThing()
    {
        if(obj_inter){
            if(obj_inter.CompareTag("Switch")){
                if(obj_inter.GetComponent<SwitchControl>().isOn)
                {
                    obj_inter.GetComponent<SwitchControl>().isOn = false;
                    sfx.PlayOneShot(sfx_open);
                }
                else
                {
                    obj_inter.GetComponent<SwitchControl>().isOn = true;
                    sfx.PlayOneShot(sfx_close);
                }
                    
            } 
            else if (obj_inter.CompareTag("HolyGrail"))
            {
                GameManager.instance.winningPlag = true;
                obj_inter.SetActive(false);
            }
        }
    }

    //쥬금
    void Die()
    {
        if(!isPlayed)
        {
            anim.SetTrigger("Dead");
            sfx.PlayOneShot(sfx_dead);
            isPlayed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Trap" && !GameManager.instance.isGameover)
        {
            GameManager.instance.isGameover = true;
            Die();
        }

        if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.coinCount += 1;
            sfx.PlayOneShot(sfx_coin);
        }

        if(other.gameObject.CompareTag("HolyGrail") && GameManager.instance.coinCount == 5)
        {
            other.gameObject.SetActive(false);
            GameManager.instance.winningPlag = true;
            GameManager.instance.timer = 60;
            sfx.PlayOneShot(sfx_coin);
        }

        if(other.gameObject.CompareTag("Switch"))
        {
            obj_inter=other.gameObject;
        }else
            obj_inter=null;

        if(other.gameObject.CompareTag("Exit"))
        {
            GameManager.instance.win = true;
            sfx.PlayOneShot(sfx_win);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
            obj_inter=null;
    }
}

