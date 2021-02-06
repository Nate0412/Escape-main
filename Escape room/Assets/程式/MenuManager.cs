using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class MenuManager : MonoBehaviour
{   
    public GameObject background;
    public Animator ani;
    public GameObject runtext;
    public GameObject chicktext;
    public GameObject story;
    public bool Startbutton = false;
    public AudioClip glass;
    public AudioSource aud;
    
    public void StartGame()
    {
        SceneManager.LoadScene("1F");
    }
    public IEnumerator ButtunChick()
    {
        Startbutton = true;
        aud.PlayOneShot(glass);
        ani.SetBool("點擊開始遊戲", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(runtext);
        Destroy(chicktext);
        Invoke("startstory", 1f);
    }
    public void buttonclick()
    {
        if (Startbutton==false)
        {
            StartCoroutine(ButtunChick());
        }

    }
    public void startstory()
    {
        story.SetActive(true);
        Invoke("StartGame", 20f);
    }
}
