using UnityEngine;
using UnityEngine.SceneManagement;//引用場景管理API

public class FloorManager : MonoBehaviour
{
    public static int floor = 1;
    public static FloorManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        print("樓層為 :" + floor);
    }
}
