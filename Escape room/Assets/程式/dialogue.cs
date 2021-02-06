using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialogue : MonoBehaviour
{
    public float charsPerSecond = 0.2f; //打字時間間隔
    private float charsPerSecondOriginal;                               // 原始間隔，按左鍵會讓間隔歸零，並恢復
    public string[] words; //保存需要顯示的文字
    public int strindex = 0; //控制語句
    public static dialogue instance; //對戰管理實體物件
    private bool isActive = false;
    private float timer = 0; //計時器
    public Text myText;
    public GameObject Dia;
    public int currentPos = 0; //當前打字位置
    public bool islongWriting = false;

    public void Awake()
    {
        instance = this;
        charsPerSecondOriginal = charsPerSecond;                        // 取得原始間隔
    }
    public void Start()
    {

        timer = 0;
        judgeRoom();
    }
    public void judgeRoom()
    {
        string name = SceneManager.GetActiveScene().name;
        if (name == "1F" || name == "2F" || name == "3F")
        {
            string[] wordstart = { "這裡是哪裡?", "我記得我收到她簡訊約在附近公園見面,但是我在約定時間卻沒看到人....", "但我怎麼會在這...?", "還是先離開這裡再說" };
            string[] wordsText = { "房間被上鎖了" };
            print(name);
            if (GameManager.StartGame == false)
            {
                words = wordstart;
                Dia.SetActive(true);
                Startdia();
                GameManager.instance.move = false;
            }
            else
            {
                words = wordsText;
            }
        }
        if (name == "房間1F-1")
        {
            string[] wordsText = { "這裡有一個保險箱，旁邊貼著被撕成半張的死亡證明書...","上面只剩年齡15歲還能依稀看出來" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間1F-2")
        {
            string[] wordsText = { "左邊寫著所有的數字宣告著結束的那一年,右邊寫著遵循著數字找到通往大門的鑰匙" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間1F-3")
        {
            string[] wordsText = { "終於來到大廳了，只差一步就能逃出去了" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間2F-1")
        {
            string[] wordsText = { "黑板上有些奇怪的數字，好像是簽名" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間2F-2")
        {
            string[] wordsText = { "這也有一個保險箱...", "地上有不同顏色的油漆還有一個箭頭", "好像是要遵循這個方向..." };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間2F-3")
        {
            string[] wordsText = { "好凌亂的房間，桌上擺放著實驗報告" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間2F-4")
        {
            string[] wordsText = { "這些破碎的窗戶好像有甚麼玄機" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間3F-1")
        {
            string[] wordsText = { "這裡被圍起來了...無法進去","咦...!? 腳邊有撕碎的日記","X月XX日,今天是你的生日...你說想和媽媽一樣成為鋼琴家","今年我答應你要送你鋼琴當生日禮物...如今卻...","我要讓..付出....後面破損的太嚴重了看不清楚" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間3F-2")
        {
            string[] wordsText = { "這就是信中的生日禮物??" };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
        if (name == "房間3F-3")
        {
            string[] wordsText = { "又是保險箱...還有一門..." };
            words = wordsText;
            Dia.SetActive(true);
            StartEffect();
        }
    }
    public void Update()
    {
        OnStartWriter();
        if (Input.GetMouseButtonDown(0))
        {
            //timer = 0;
            //currentPos = 0;
            //strindex++; //下一句

            

            isActive = true;
            if (strindex >= words.Length) //防止超出字串的長度
            {
                strindex = 0;
                GameManager.instance.move = true;
                GameManager.StartGame = true;
                Dia.SetActive(false);
            }
            
            
            if (currentPos >= words[strindex].Length)           // 判定文字跑完在進下一句
            {
                OnFinish();
            }
            else
            {
                charsPerSecond = 0;                             // 還沒跑完就加速
            }
        }
    }

    public void StartEffect()
    {
        isActive = true;
    }
    public void Startdia()
    {
        timer = 0;
        currentPos = 0;
        strindex = 0;
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
