using UnityEngine;
using System;
using UnityEngine.UI;

public class Room1F1 : MonoBehaviour
{
    public GameObject cofferopen;
    public GameObject coffer;


    public void Start()
    {
        if (GameManager.canindoor2to1)
        {
            opencoffer();
        }
    }
    public void opencoffer()
    {
        cofferopen.SetActive(true);
    }
    public void cameracoffer()
    {
        coffer.SetActive(true);
        GameManager.instance.roomNumber.SetActive(false);
    }
    public void retuenRoom()
    {
        coffer.SetActive(false);
        string[] wordsText = { "這裡有一個保險箱，密碼似乎是9碼..." };
        dialogue.instance.words = wordsText;
        if(GameManager.getRaysLight)
        {
            GameManager.instance.roomNumber.SetActive(true);
        }
    }
    public void GetInput(string getInput)
    {
        string number = getInput;
        if (number == "845136729")
        {              
            opencoffer();
            string[] wordsText = { "取得2F某層的的鑰匙" };
            dialogue.instance.words = wordsText;
            dialogue.instance.Dia.SetActive(true);

            dialogue.instance.Startdia();
            GameManager.canindoor2to1 = true;
            GameManager.instance.Key.SetActive(true);
        }
    }
}
