using System.Collections;
using UnityEngine;

public static class TouchCheck {

    private static TouchInput _touchInput = GameObject.Find ("TouchInput").GetComponent<TouchInput> (); //インスタンスに GameController.cs 情報を格納

    public static bool CheckTouch (int lineNum) {
        if (Input.touchCount > 0) {
            Touch _touch = Input.GetTouch (0);
            Vector2 newVec = new Vector2 (_touch.position.x, Screen.height - _touch.position.y);
            if (_touch.phase == TouchPhase.Began) {
                //Rectとタッチの位置を判定
                if (newVec.x >=_touchInput._rect.xMin &&
                    newVec.x <_touchInput._rect.xMax &&
                    newVec.y >=_touchInput._rect.yMin &&
                    newVec.y <_touchInput._rect.yMax) {

                    //タッチ処理
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
}