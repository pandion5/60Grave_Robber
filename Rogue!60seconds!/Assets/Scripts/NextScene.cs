using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public Sprite speakerOn;
    public Sprite speakerOff;
    public GameObject muteUI;
    private AudioSource bgm;
    void Start()
    {
        bgm = GetComponent<AudioSource>();
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void AudioMute()
    {
        bgm.mute = !bgm.mute;

        if(bgm.mute)
            muteUI.GetComponent<Image>().sprite = speakerOff;
        else
            muteUI.GetComponent<Image>().sprite = speakerOn;

            
    }
    
}
