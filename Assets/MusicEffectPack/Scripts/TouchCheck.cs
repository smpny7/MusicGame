using System.Collections;
using UnityEngine;

public static class TouchCheck {

    private static TouchInput _touchInput = GameObject.Find ("TouchInput").GetComponent<TouchInput> (); //インスタンスに GameController.cs 情報を格納

    static bool _flag;

    public static bool CheckTouch (int lineNum) {
        _flag = false;
        if (Input.touchCount > 0) {
            for (int i = 0; i < 2; i++) {
                // if (Input.GetTouch (i) == 0) break;
                // if (Input.GetTouch (i).phase == TouchPhase.Ended) break;
                if (Input.touchCount == i) break;
                Touch _touch = Input.GetTouch (i);
                Vector2 newVec = new Vector2 (_touch.position.x, Screen.height - _touch.position.y);
                if (_touch.phase == TouchPhase.Began) {
                    switch (lineNum) {
                        case 0:
                            if (newVec.x >= _touchInput._rect1.xMin &&
                                newVec.x < _touchInput._rect1.xMax &&
                                newVec.y >= _touchInput._rect1.yMin &&
                                newVec.y < _touchInput._rect1.yMax) {
                                _flag = true;
                            }
                            break;
                        case 1:
                            if (newVec.x >= _touchInput._rect2.xMin &&
                                newVec.x < _touchInput._rect2.xMax &&
                                newVec.y >= _touchInput._rect2.yMin &&
                                newVec.y < _touchInput._rect2.yMax) {
                                _flag = true;
                            }
                            break;
                        case 2:
                            if (newVec.x >= _touchInput._rect3.xMin &&
                                newVec.x < _touchInput._rect3.xMax &&
                                newVec.y >= _touchInput._rect3.yMin &&
                                newVec.y < _touchInput._rect3.yMax) {
                                _flag = true;
                            }
                            break;
                        case 3:
                            if (newVec.x >= _touchInput._rect4.xMin &&
                                newVec.x < _touchInput._rect4.xMax &&
                                newVec.y >= _touchInput._rect4.yMin &&
                                newVec.y < _touchInput._rect4.yMax) {
                                _flag = true;
                            }
                            break;
                        case 4:
                            if (newVec.x >= _touchInput._rect5.xMin &&
                                newVec.x < _touchInput._rect5.xMax &&
                                newVec.y >= _touchInput._rect5.yMin &&
                                newVec.y < _touchInput._rect5.yMax) {
                                _flag = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return _flag;
    }
}