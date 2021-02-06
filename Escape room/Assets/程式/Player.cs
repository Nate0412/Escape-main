using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)
public class Player : MonoBehaviour
{
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10.5f;
    public Animator ani;
    public AudioSource aud;
    public Rigidbody2D rig;
    public bool indoor1to1;
    public bool indoor1to2;
    public bool indoor1to3;
    public bool indoor2to1;
    public bool indoor2to2;
    public bool indoor2to3;
    public bool indoor2to4;
    public bool indoor3to1;
    public bool indoor3to2;
    public bool indoor3to3;
    public bool floorup1;
    public bool floorup2;
    public bool floordown2;
    public bool floordown3;
    public bool squatdown = false;
    public bool up = true;
    public static Vector2 pos;
 
    public void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }
    public void Start()
    {
        
        if (GameManager.TP == true)
        {
            transform.position = new Vector3(pos.x, pos.y, 0);
            GameManager.TP = false;
        }
    }
    public void Update()
    {
        Move();
        StartCoroutine(INSIDE());
    }
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {        
        if (!GameManager.instance.move)
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
            ani.SetBool("跑步開關", false);
            return;
        }

        if (GameManager.instance.move == true && squatdown == false)
        {
            
            //水平浮點數 = 輸入 的 取得軸向("水平")-左右AD
            float h = Input.GetAxis("Horizontal");
            //剛體 的 加速度 = 新 二維向量(水平浮點數 * 速度, 剛體的加速度Y)
            rig.velocity = new Vector2(h * speed, rig.velocity.y);
            //動畫的設定布林值(參數名稱，水平 不等於零時勾選)
            ani.SetBool("跑步開關", h != 0);
            //keycode 列單(下拉式選單) 所有輸入的選項:滑鼠、鍵盤、搖桿
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.S) == false)
            {
                //此物件的變形元件
                //eulerAngles 歐拉角度
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.S) == false)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);

            }          
        }

        if (Input.GetKey(KeyCode.S) && GameManager.instance.move == true)
        {
            squatdown = true;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", true);
        }
        if (Input.GetKeyUp(KeyCode.S) && GameManager.instance.move == true)
        {
            squatdown = false;
            ani.SetBool("蹲下開關", false);
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    /// <param name="obj"></param>
    public void Dead(string obj)
    {
        if (obj == "敵人")
        {
            if (ani.GetBool("死亡觸發")) return;
            enabled = false;
            ani.SetBool("死亡觸發", true);
        }
    }
    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ODE碰撞時執行一次的事件
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dead(collision.gameObject.tag);
    }

    /// <summary>
    /// 判斷是否在房間或樓層的進入範圍
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "1F門1") indoor1to1 = true;
        if (collision.name == "1F門2") indoor1to2 = true;
        if (collision.name == "1F門3") indoor1to3 = true;
        if (collision.name == "2F門1") indoor2to1 = true;
        if (collision.name == "2F門2") indoor2to2 = true;
        if (collision.name == "2F門3") indoor2to3 = true;
        if (collision.name == "2F門4") indoor2to4 = true;
        if (collision.name == "3F門1") indoor3to1 = true;
        if (collision.name == "3F門2") indoor3to2 = true;
        if (collision.name == "3F門3") indoor3to3 = true;
        if (collision.tag == "1F上樓區域") floorup1 = true;
        if (collision.tag == "2F上樓區域") floorup2 = true;
        if (collision.tag == "2F下樓區域") floordown2 = true;
        if (collision.tag == "3F下樓區域") floordown3 = true;
    }

    /// <summary>
    /// 判斷是否離開房間或樓層的進入範圍
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "1F門1") indoor1to1 = false;
        if (collision.name == "1F門2") indoor1to2 = false;
        if (collision.name == "1F門3") indoor1to3 = false;
        if (collision.name == "2F門1") indoor2to1 = false;
        if (collision.name == "2F門2") indoor2to2 = false;
        if (collision.name == "2F門3") indoor2to3 = false;
        if (collision.name == "2F門4") indoor2to4 = false;
        if (collision.name == "3F門1") indoor3to1 = false;
        if (collision.name == "3F門2") indoor3to2 = false;
        if (collision.name == "3F門3") indoor3to3 = false;
        if (collision.tag == "1F上樓區域") floorup1 = false;
        if (collision.tag == "2F上樓區域") floorup2 = false;
        if (collision.tag == "2F下樓區域") floordown2 = false;
        if (collision.tag == "3F下樓區域") floordown3 = false;
    }

    /// <summary>
    /// 進入房間和樓層
    /// </summary>
    /// <returns></returns>
    private IEnumerator INSIDE()
    {
       //進入房間
        if (indoor1to1 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            up = false;
            GameManager.instance.move = false;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", false);
            pos = transform.position;
            StartCoroutine(GameManager.instance.Endloadingimage());
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("房間1F-1");
        }
        else if (indoor1to2 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            GameManager.instance.move = false;
            up = false;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", false);
            pos = transform.position;
            StartCoroutine(GameManager.instance.Endloadingimage());
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("房間1F-2");
        }
        else if (indoor1to3 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            
            if (GameManager.canindoor1to3 == false)
            {
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                string[] wordsText = { "房間被上鎖了" };
                dialogue.instance.words = wordsText;
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.Startdia();
            }
            else
            {
                up = false;
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                ani.SetBool("蹲下開關", false);
                pos = transform.position;
                StartCoroutine(GameManager.instance.Endloadingimage());
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("房間1F-3");
            }     
        }
        else if (indoor2to1 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            if (GameManager.canindoor2to1 == false)
            {
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.Startdia();
            }
            else
            {
                up = false;
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                ani.SetBool("蹲下開關", false);
                pos = transform.position;
                pos = transform.position;
                StartCoroutine(GameManager.instance.Endloadingimage());
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("房間2F-1");
            }
        }
        else if (indoor2to2 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            if (GameManager.canindoor2to2 == false)
            {
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.Startdia();
            }
            else
            {
                up = false;
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                ani.SetBool("蹲下開關", false);
                pos = transform.position;
                pos = transform.position;
                StartCoroutine(GameManager.instance.Endloadingimage());
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("房間2F-2");
            }
        }
        else if (indoor2to3 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            GameManager.instance.move = false;
            if (GameManager.canindoor2to3 == false)
            {
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.Startdia();
            }
            else
            {
                up = false;
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                ani.SetBool("蹲下開關", false);
                pos = transform.position;
                pos = transform.position;
                StartCoroutine(GameManager.instance.Endloadingimage());
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("房間2F-3");
            }
        }
        else if (indoor2to4 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            up = false;
            GameManager.instance.move = false;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", false);
            pos = transform.position;
            StartCoroutine(GameManager.instance.Endloadingimage());
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("房間2F-4");
        }
        else if (indoor3to1 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            if (GameManager.canindoor3to1 == false)
            {
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.Startdia();
            }
            else
            {
                up = false;
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                ani.SetBool("蹲下開關", false);
                pos = transform.position;
                StartCoroutine(GameManager.instance.Endloadingimage());
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("房間3F-1");
            }
        }
        else if (indoor3to2 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
           
            if (GameManager.canindoor3to2 == false)
            {
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.Startdia();
            }
            else
            {
                up = false;
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                ani.SetBool("蹲下開關", false);
                pos = transform.position;
                StartCoroutine(GameManager.instance.Endloadingimage());
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("房間3F-2");
            }
        }
        else if (indoor3to3 && Input.GetKeyDown(KeyCode.W) && up) //如果 在門裡面 並且按下w
        {
            if (GameManager.canindoor3to3 == false)
            {
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                dialogue.instance.Dia.SetActive(true);
                dialogue.instance.Startdia();
            }
            else
            {
                up = false;
                GameManager.instance.move = false;
                rig.velocity = new Vector2(0, 0);
                ani.SetBool("跑步開關", false);
                ani.SetBool("蹲下開關", false);
                pos = transform.position;
                StartCoroutine(GameManager.instance.Endloadingimage());
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("房間3F-3");
            }
        }
       //1F上2F
        if (floorup1 && Input.GetKeyDown(KeyCode.W) && up)
        {
            up = false;
            GameManager.instance.move = false;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", false);
            FloorManager.floor = FloorManager.floor + 1;
            StartCoroutine(GameManager.instance.Endloadingimage());
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("2F");
        }
       //2F上3F
        if (floorup2 && Input.GetKeyDown(KeyCode.W) && up)
        {
            up = false;
            GameManager.instance.move = false;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", false);
            FloorManager.floor = FloorManager.floor + 1;
            StartCoroutine(GameManager.instance.Endloadingimage());
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("3F");
        }
        //2F下1F
        if (floordown2 && Input.GetKeyDown(KeyCode.W) && up)
        {
            up = false;
            GameManager.instance.move = false;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", false);
            FloorManager.floor = FloorManager.floor - 1;
            StartCoroutine(GameManager.instance.Endloadingimage());
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("1F");
        }
       //3F下2F
        if (floordown3 && Input.GetKeyDown(KeyCode.W) && up)
        {
            up = false;
            GameManager.instance.move = false;
            rig.velocity = new Vector2(0, 0);
            ani.SetBool("跑步開關", false);
            ani.SetBool("蹲下開關", false);
            FloorManager.floor = FloorManager.floor - 1;
            StartCoroutine(GameManager.instance.Endloadingimage());
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("2F");
        }
    }
}