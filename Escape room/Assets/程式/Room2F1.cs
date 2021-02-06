using UnityEngine;
using System;
using UnityEngine.UI;


public class Room2F1 : MonoBehaviour
{
    public GameObject passscreen;
    public GameObject USBpassscreen;
    public GameObject USB2passscreen;
    public GameObject PC;
    public GameObject wrongpass;
    public GameObject USBwrongpass;
    public GameObject USB;
    public GameObject USB2;
    public bool passaccount = false;
    public bool passpassword = false;
    public bool USBpasspassword = false;
    public bool USB2passpassword = false;

    public void Start()
    {
        if (GameManager.canindoor2to2)
        {
            Pass();
        }
        if (GameManager.getusb)
        {
            USB.SetActive(true);
        }
        if (GameManager.getRaysLight)
        {
            USB2.SetActive(true);
        }

    }
    public void Pass()
    {
        passscreen.SetActive(true);
    }
    public void cameraPC()
    {
        PC.SetActive(true);
        if (GameManager.getRaysLight)
        {
            string[] wordsText = { "其他房間應該有線索..." };
            dialogue.instance.words = wordsText;
            dialogue.instance.Dia.SetActive(true);
            dialogue.instance.StartEffect();
        }
        else
        {
            if (GameManager.getusb == false)
            {
                string[] wordsText = { "帳號通常為英文名子，密碼通常為數字" };
                dialogue.instance.words = wordsText;
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.StartEffect();
            }
            else
            {
                string[] wordsText = { "密碼!?是隨身碟上面的數字嗎?" };
                dialogue.instance.words = wordsText;
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.StartEffect();
            }
        }

        GameManager.instance.roomNumber.SetActive(false);
    }

    public void retuenRoom()
    {
        PC.SetActive(false);
        string[] wordsText = { "黑板上有些奇怪的數字，好像是簽名" };
        dialogue.instance.words = wordsText;
        if (GameManager.getRaysLight)
        {
            GameManager.instance.roomNumber.SetActive(true);
        }
    }
    public void Getaccount(string getInput)
    {
        string account = getInput;
        print("帳號" + account);
        if (account == "Tony")
        {
            passaccount = true;
        }
        else
        {
            passaccount = false;
        }
    }
    public void Getpassword(string getInput)
    {
        string number = getInput;
        if (number == "134")
        {
            passpassword = true;
        }
        else
        {
            passpassword = false;
        }
    }
    public void Enter()
    {
        if (passaccount && passpassword)
        {
            GameManager.canindoor2to2 = true;
            GameManager.canindoor2to3 = true;
            passscreen.SetActive(true);
        }
        else
        {
            wrongpass.SetActive(true);
        }    
    }
    public void backtotype()
    {
        wrongpass.SetActive(false);
        USBwrongpass.SetActive(false);
    }
    public void USBPassword(string getInput)
    {
        string number = getInput;
        if (number == "9869")
        {
            USBpasspassword = true;
        }
        else
        {
            USBpasspassword = false;
        }
    }
    public void USB2Password(string getInput)
    {
        string number = getInput;
        if (number == "369428715")
        {
            USB2passpassword = true;
        }
        else
        {
            USB2passpassword = false;
        }
    }
    public void USB2Enter()
    {
        if (USB2passpassword)
        {
            GameManager.canindoor1to3 = true;
            USB2passscreen.SetActive(true);
            string[] wordsText = { "這個鍵盤看起來很好用，拿走好了" };
            dialogue.instance.words = wordsText;
            dialogue.instance.Dia.SetActive(true);
            dialogue.instance.StartEffect();
        }
        else
        {
            USBwrongpass.SetActive(true);
        }
    }
    public void USBEnter()
    {
        if (USBpasspassword)
        {
            GameManager.canindoor3to1 = true;
            GameManager.canindoor3to2 = true;
            GameManager.canindoor3to3 = true;
            USBpassscreen.SetActive(true);
        }
        else
        {
            USBwrongpass.SetActive(true);
        }
    }

}
