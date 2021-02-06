using UnityEngine;
using System;
using UnityEngine.UI;

public class Room2F3 : MonoBehaviour
{
    public GameObject paper;


    public void Start()
    {     
    }
    public void camerapaper()
    {
        paper.SetActive(true);
        string[] wordsText = { "好像是人體實驗的報告，照著這個動作做似乎能發現甚麼..." };
        dialogue.instance.words = wordsText;
        dialogue.instance.Dia.SetActive(true);
        dialogue.instance.StartEffect();
        GameManager.instance.roomNumber.SetActive(false);
    }
    public void retuenRoom()
    {
        paper.SetActive(false);
        string[] wordsText = { "好凌亂的房間，似乎是病房" };
        dialogue.instance.words = wordsText;
        if (GameManager.getRaysLight)
        {
            GameManager.instance.roomNumber.SetActive(true);
        }
    }
    
    
}