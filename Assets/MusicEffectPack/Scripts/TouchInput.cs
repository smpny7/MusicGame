using System.Collections;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    public GUIStyle custom;

    public Rect _rect = new Rect (0, 0, 250, 500);
    // Rect _rect = new Rect (250, 1350, 416, 500);
    // Rect _rect = new Rect (100, 400, 416, 500);
    // _rect.SpriteAlignment = CaseInsensitiveComparer;
    // _rect.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
    // _rect.Alignment = Rect.Center;
    // _rect.LineAlignment = Rect.Center;

    void Start () {
        Vector3 tmp = GameObject.Find ("Lane1").transform.localPosition;
        _rect.x = Screen.width / 2 + tmp.x - 125;

        // Vector3 tmpY = GameObject.Find ("JudgeLine").transform.localPosition;
        // Debug.Log (tmpY.y);
        _rect.y = Screen.height / 2 + 150;
    }

    void OnGUI () {
        // ボタンを作成。使用スタイルを上で定義した GUIStyle で渡します。
        GUI.Box (_rect, "", custom);
    }
}