using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FontScroller : MonoBehaviour {

    public Rect drawRect = new Rect (0, 0, 0, 0);
    public float speed = 0.5f;

    public GUIStyle style;

    public string Title;

    public int count = 0;

    private Rect fontRect;

    void Start () {
        fontRect = new Rect (0, 0, Screen.width, Screen.height);
    }

    void Update () {
        if (Title.Length > 8) {
            if (fontRect.x != 0) {
                fontRect.x -= speed;
            } else {
                if (count++ > 200) {
                    count = 0;
                    fontRect.x -= speed;
                }
            }
            if (fontRect.x < 0 - style.fontSize * Title.Length) fontRect.x = drawRect.width;
        }
    }

    void OnGUI () {
        GUILayout.BeginArea (drawRect);
        GUI.Label (fontRect, Title, style);
        GUILayout.EndArea ();
    }
}