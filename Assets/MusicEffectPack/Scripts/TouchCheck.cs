using System.Collections;
using UnityEngine;

public static class TouchCheck { //By smpny7

    static int _touchCount;
    static bool _flag;

    static Touch _touch;
    static Vector2 newVec;
    static Rect _rect;

    public static bool CheckTouch (int lineNum, TouchInput _touchInput) {
        _touchCount = Input.touchCount;
        _flag = false;
        if (_touchCount > 0) {
            switch (lineNum) {
                case 0:
                    _rect = _touchInput._rect1;
                    break;
                case 1:
                    _rect = _touchInput._rect2;
                    break;
                case 2:
                    _rect = _touchInput._rect3;
                    break;
                case 3:
                    _rect = _touchInput._rect4;
                    break;
                case 4:
                    _rect = _touchInput._rect5;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < _touchCount; i++) {
                _touch = Input.GetTouch (i);
                newVec = new Vector2 (_touch.position.x, Screen.height - _touch.position.y);
                if (_touch.phase == TouchPhase.Began) {
                    if (newVec.x >= _rect.xMin &&
                        newVec.x < _rect.xMax &&
                        newVec.y >= _rect.yMin &&
                        newVec.y < _rect.yMax) {
                        _flag = true;
                    }
                }
            }
        }
        return _flag;
    }
}