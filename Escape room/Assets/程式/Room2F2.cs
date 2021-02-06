using UnityEngine;
using System;
using UnityEngine.UI;

public class Room2F2 : MonoBehaviour
{
    public GameObject cofferopen;
    public GameObject coffer;


    public void Start()
    {
        if (GameManager.getusb)
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
        string[] wordsText = { "又有一個保險箱...", "地上有不同顏色的油漆還有一個箭頭", "好像是要遵循這個方向..." };
        dialogue.instance.words = wordsText;
        if (GameManager.getRaysLight)
        {
            GameManager.instance.roomNumber.SetActive(true);
        }
    }
    public void GetInput(string getInput)
    {
        String number = getInput;
        if (number == "791149129")
        {
            opencoffer();
            string[] wordsText = { "取得一個隨身碟，上面還有數字，似乎是密碼" };
            dialogue.instance.words = wordsText;
            dialogue.instance.Dia.SetActive(true);
            dialogue.instance.Startdia();
            GameManager.getusb = true;
            GameManager.instance.usb.SetActive(true);
        }
    }
}