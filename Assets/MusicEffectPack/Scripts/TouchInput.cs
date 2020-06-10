using System.Collections;
using UnityEngine;

public class TouchInput : MonoBehaviour { //By smpny7

    public GUIStyle custom;
    public Rect _rect1;
    public Rect _rect2;
    public Rect _rect3;
    public Rect _rect4;
    public Rect _rect5;

    void Start () {
        _rect1 = new Rect (0, 0, 0, 630);
        _rect2 = new Rect (0, 0, 0, 630);
        _rect3 = new Rect (0, 0, 0, 630);
        _rect4 = new Rect (0, 0, 0, 630);
        _rect5 = new Rect (0, 0, 0, 630);

        Vector3 tmp1 = GameObject.Find ("Lane1").transform.localPosition;
        _rect1.x = Screen.width / 2 + tmp1.x - 156;
        _rect1.y = Screen.height / 2 + 200;
        _rect1.width = 312;

        Vector3 tmp2 = GameObject.Find ("Lane2").transform.localPosition;
        _rect2.x = Screen.width / 2 + tmp2.x - 156;
        _rect2.y = Screen.height / 2 + 200;
        _rect2.width = 312;

        Vector3 tmp3 = GameObject.Find ("Lane3").transform.localPosition;
        _rect3.x = Screen.width / 2 + tmp3.x - 156;
        _rect3.y = Screen.height / 2 + 200;
        _rect3.width = 312;

        Vector3 tmp4 = GameObject.Find ("Lane4").transform.localPosition;
        _rect4.x = Screen.width / 2 + tmp4.x - 156;
        _rect4.y = Screen.height / 2 + 200;
        _rect4.width = 312;

        Vector3 tmp5 = GameObject.Find ("Lane5").transform.localPosition;
        _rect5.x = Screen.width / 2 + tmp5.x - 156;
        _rect5.y = Screen.height / 2 + 200;
        _rect5.width = 312;
    }

    void OnGUI () {
        GUI.Box (_rect1, "", custom);
        GUI.Box (_rect2, "", custom);
        GUI.Box (_rect3, "", custom);
        GUI.Box (_rect4, "", custom);
        GUI.Box (_rect5, "", custom);
    }
}