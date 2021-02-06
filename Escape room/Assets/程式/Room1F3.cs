using UnityEngine;
using System;
using UnityEngine.UI;

public class Room1F3 : MonoBehaviour
{
    public GameObject gateopen;
    public GameObject calendar;
    public GameObject gatelock;
    public GameObject gate;
    public GameObject passgame;
    public bool pass = false;
    public AudioClip doorsound;
    public AudioSource aud;

    public void Start()
    {
       
    }
    public void opengate()
    {
        gateopen.SetActive(true);
    }
    public void cameracalendar()
    {
        calendar.SetActive(true);
        string[] wordsText = { "這日曆上面有數字，???或許就是大門的密碼!" };
        dialogue.instance.words = wordsText;
        dialogue.instance.Dia.SetActive(true);
        dialogue.instance.StartEffect();
    }
    public void cameralock()
    {
        gatelock.SetActive(true);
        string[] wordsText = { "按鍵壞了只能連接鍵盤輸入了" };
        dialogue.instance.words = wordsText;
        dialogue.instance.Dia.SetActive(true);
        dialogue.instance.StartEffect();
    }
    public void cameragate()
    {
        gate.SetActive(true);
        string[] wordsText = { "這就是大門，到外面就自由了!" };
        dialogue.instance.words = wordsText;
        dialogue.instance.Dia.SetActive(true);
        dialogue.instance.StartEffect();
    }
    public void retuenRoom()
    {
        
            calendar.SetActive(false);
            gate.SetActive(false);

            string[] wordsText = { "終於來到大廳了，只差一步就能逃出去了" };
            dialogue.instance.words = wordsText;
       

    }
    public void returnGate()
    {
        if (pass==false)
        {
            gatelock.SetActive(false);
        }
        else
        {
            gatelock.SetActive(false);
            passgame.SetActive(true);
        }
       
    }
    public void GetInput(string getInput)
    {
        string number = getInput;
        if (number == "046115")
        {
            opengate();
            aud.PlayOneShot(doorsound);
            pass = true;
            string[] wordsText = { "大門打開了!" };
            dialogue.instance.words = wordsText;
            dialogue.instance.Dia.SetActive(true);
            dialogue.instance.Startdia();


        }
    }
    public void exitgame()
    {
        Application.Quit();
    }
}
