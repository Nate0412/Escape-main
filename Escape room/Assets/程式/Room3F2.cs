using UnityEngine;
using System;
using UnityEngine.UI;

public class Room3F2 : MonoBehaviour
{
    public GameObject piano;
    public AudioSource aud;
    public AudioClip piano1;
    public AudioClip piano2;
    public AudioClip piano3;
    public AudioClip piano4;
    public AudioClip piano5;
    public AudioClip piano6;
    public AudioClip piano7;
    public AudioClip bksound;
    public void Start()
    {
        
    }
    public void pianoButton1()
    {
        aud.PlayOneShot(piano1);
    }
    public void pianoButton2()
    {
        aud.PlayOneShot(piano2);
    }
    public void pianoButton3()
    {
        aud.PlayOneShot(piano3);
    }
    public void pianoButton4()
    {
        aud.PlayOneShot(piano4);
    }
    public void pianoButton5()
    {
        aud.PlayOneShot(piano5);
    }
    public void pianoButton6()
    {
        aud.PlayOneShot(piano6);
    }
    public void pianoButton7()
    {
        aud.PlayOneShot(piano7);
    }
    public void camerapiano()
    {
        piano.SetActive(true);
        aud.Stop();
        string[] wordsText = { "鋼琴似乎還可以彈奏" };
        dialogue.instance.words = wordsText;
        dialogue.instance.Dia.SetActive(true);
        dialogue.instance.StartEffect();
        GameManager.instance.roomNumber.SetActive(false);
    }
    
    public void retuenRoom()
    {
        piano.SetActive(false);
        aud.Play();
        string[] wordsText = { "這是怎麼會有一個鋼琴??" };
        dialogue.instance.words = wordsText;
        if (GameManager.getRaysLight)
        {
            GameManager.instance.roomNumber.SetActive(true);
        }
    }


}