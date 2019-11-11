using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool isGameover = false;
    public Text timerText;
    public Text coinText;
    public Text objectText;

    public GameObject checkBox_Checked;
    public GameObject checkBox_Unchecked;
    public GameObject holyGrail;

    public Sprite holyGrail_Full_Image;
    public float timer = 60.0f;
    public int coinCount = 0;
    public bool winningPlag = false;
    public bool win = false;

    public GameObject traps;
    public GameObject cant_through;
    public GameObject exitPoint;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Scene에 2개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isGameover)
        {
            timer = 0;
            timerText.text = "You Are Dead!\n" + "Touch The Screen To Restart";
        }
        //타이머
        if(timer > 0 && !isGameover)
        {
            timer -= Time.deltaTime;
            timerText.text = string.Format("{0:#.#}",timer)+" (s) remainning!";
        }
        else if(timer == 0)
        {
            timerText.text = "You Are Dead!\n" + "Touch The Screen To Restart";
            isGameover = true;
        }
        if(timer < 0)
        {
            timer = 0;
        }

        //게임 오버시 재시작을 위한 UI
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }

        if(win && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }

        if(coinCount == 5)
            holyGrail.GetComponent<SpriteRenderer>().sprite = holyGrail_Full_Image;

        if(winningPlag)
        {
            traps.gameObject.SetActive(true);
            cant_through.SetActive(false);
            exitPoint.SetActive(true);
        }

        if(win)
        {
            timer = -10;
            timerText.text = "You Win!\n" + "Touch The Screen To Main Screen";
        }

        coinText.text = "X " + coinCount;
        
        if(coinCount < 5)
            objectText.text = "Collect The " + (5-coinCount) + " Coins";
        else if(win)
            objectText.text = "Congratulations!";
        else if(coinCount >= 5 && !winningPlag)
            objectText.text = "Get The HolyGrail! Now!";
        else if(winningPlag)
        {
            objectText.text = "Escape the Cave : Exit is StartPoint";
            checkBox_Checked.SetActive(true);
            checkBox_Unchecked.SetActive(false);
        }
        
    }

    public void GoMain()
    {
        SceneManager.LoadScene(0);
    }

}
