using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ITdialogue : MonoBehaviour
{
    public float charsPerSecond = 0.2f; //打字時間間隔
    private float charsPerSecondOriginal;                               // 原始間隔，按左鍵會讓間隔歸零，並恢復
    public string[] words; //保存需要顯示的文字
    public int strindex = 0; //控制語句
    private bool isActive = false;
    private float timer = 0; //計時器
    public Text myText;
    public GameObject Dia;
    public GameObject key;
    public GameObject usb;
    public GameObject raysLight;
    public GameObject usb2;
    public GameObject keyboard;
    public int currentPos = 0; //當前打字位置
    public bool islongWriting = false;

    public void Awake()
    {
        charsPerSecondOriginal = charsPerSecond;                        // 取得原始間隔
    }
    public void Start()
    {

        timer = 0;
        string[] wordsText = { " " };
        words = wordsText;
    }
    public void KEY()
    {
        string[] wordsText = { "這是通往2F房間的鑰匙" };
        words = wordsText;
        key.SetActive(true);
        Dia.SetActive(true);
        StartEffect();
    }
    public void USB()
    {
        string[] wordsText = { "USB上面的顏色好像哪個房間有看過..." };
        words = wordsText;
        usb.SetActive(true);
        Dia.SetActive(true);
        StartEffect();
    }
    public void RaysLight()
    {
        string[] wordsText = { "聽說紫外線燈可以看到隱形筆的字跡" };
        words = wordsText;
        raysLight.SetActive(true);
        Dia.SetActive(true);
        StartEffect();
    }
    public void USB2()
    {
        string[] wordsText = { "USB估計裡面有甚麼資料" };
        words = wordsText;
        usb2.SetActive(true);
        Dia.SetActive(true);
        StartEffect();
    }
    public void Keyboard()
    {
        string[] wordsText = { "很高級的鍵盤" };
        words = wordsText;
        keyboard.SetActive(true);
        Dia.SetActive(true);
        StartEffect();
    }
    public void Update()
    {
        OnStartWriter();
        if (Input.GetMouseButtonDown(0))
        {
            //timer = 0;
            //currentPos = 0;
            //strindex++; //下一句
            //isActive = true;
            if (strindex >= words.Length) //防止超出字串的長度
            {
                strindex = 0;
                GameManager.instance.move = true;
                GameManager.StartGame = true;
                Dia.SetActive(false);
                key.SetActive(false);
                usb.SetActive(false);
                usb2.SetActive(false);
                raysLight.SetActive(false);
                keyboard.SetActive(false);
            }

            if (currentPos >= words[strindex].Length)
            {
                isActive = false;
                Dia.SetActive(false);
                key.SetActive(false);
                usb.SetActive(false);
                usb2.SetActive(false);
                raysLight.SetActive(false);
                keyboard.SetActive(false);
                charsPerSecond = charsPerSecondOriginal;
                currentPos = 0;
            }
            else
            {
                charsPerSecond = 0;
            }
        }
     
    }

    public void StartEffect()
    {
        isActive = true;
    }
    /// 打字
    public void OnStartWriter()
    {
        if (strindex >= words.Length) return;

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            { //判断計時器時間是否到達
                timer = 0;
                currentPos++;
                myText.text = words[strindex].Substring(0, currentPos); //刷新文本顯示内容

                if (currentPos >= words[strindex].Length)
                {
                    // OnFinish();
                    isActive = false;
                }
            }
        }
    }
    /// 結束打字，初始化數據
    public void OnFinish()
    {
        //isActive = false;
        timer = 0;
        currentPos = 0;
        charsPerSecond = charsPerSecondOriginal;
        strindex++;
    }
}