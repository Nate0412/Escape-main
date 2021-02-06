using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("目標")]
    public Transform target;
    [Header("追蹤速度"),Range(0,100)]
    public float speed = 1.5f;
    [Header("攝影機下方與上方的限制")]
    public Vector2 limit = new Vector2(0, 3.5f);

    private void Track()
    {
        Vector3 posA = transform.position;    //取得攝像機座標
        Vector3 posB = target.position;       //取得目標座標
        posB.z = -10;                         //固定z軸
        posB.y = Mathf.Clamp(posB.y, limit.x, limit.y); //將y軸夾在限制範圍內
        posB.x = Mathf.Clamp(posB.x, 0, 79.1f); //將x軸夾在限制範圍內
        //一禎的時間 Time.deltaTime
        posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);//插值

        transform.position = posA;            //攝影機座標 = A點
    }
    private void LateUpdate()
    {
        Track();
    }
}
