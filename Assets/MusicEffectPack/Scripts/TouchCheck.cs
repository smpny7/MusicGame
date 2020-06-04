using System.Collections;
using UnityEngine;

public static class TouchCheck {

    static Touch _touch;
    static Vector2 newVec;
    static bool _flag;
    static Rect _rect;

    public static bool CheckTouch (int lineNum, TouchInput _touchInput) {
        _flag = false;
        if (Input.touchCount > 0) {
            for (int i = 0; i < 2; i++) {
                if (Input.touchCount == i) break;
                _touch = Input.GetTouch (i);
                newVec = new Vector2 (_touch.position.x, Screen.height - _touch.position.y);
                if (_touch.phase == TouchPhase.Began) {
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

                    if (newVec.x >= _rect.xMin &&
                        newVec.x < _rect.xMax &&
                        newVec.y >= _rect.yMin &&
                        newVec.y < _rect.yMax) {
                        _flag = true;
                    }

                    // switch (lineNum) {
                    //     case 0:
                    //         if (newVec.x >= _touchInput._rect1.xMin &&
                    //             newVec.x < _touchInput._rect1.xMax &&
                    //             newVec.y >= _touchInput._rect1.yMin &&
                    //             newVec.y < _touchInput._rect1.yMax) {
                    //             _flag = true;
                    //         }
                    //         break;
                    //     case 1:
                    //         if (newVec.x >= _touchInput._rect2.xMin &&
                    //             newVec.x < _touchInput._rect2.xMax &&
                    //             newVec.y >= _touchInput._rect2.yMin &&
                    //             newVec.y < _touchInput._rect2.yMax) {
                    //             _flag = true;
                    //         }
                    //         break;
                    //     case 2:
                    //         if (newVec.x >= _touchInput._rect3.xMin &&
                    //             newVec.x < _touchInput._rect3.xMax &&
                    //             newVec.y >= _touchInput._rect3.yMin &&
                    //             newVec.y < _touchInput._rect3.yMax) {
                    //             _flag = true;
                    //         }
                    //         break;
                    //     case 3:
                    //         if (newVec.x >= _touchInput._rect4.xMin &&
                    //             newVec.x < _touchInput._rect4.xMax &&
                    //             newVec.y >= _touchInput._rect4.yMin &&
                    //             newVec.y < _touchInput._rect4.yMax) {
                    //             _flag = true;
                    //         }
                    //         break;
                    //     case 4:
                    //         if (newVec.x >= _touchInput._rect5.xMin &&
                    //             newVec.x < _touchInput._rect5.xMax &&
                    //             newVec.y >= _touchInput._rect5.yMin &&
                    //             newVec.y < _touchInput._rect5.yMax) {
                    //             _flag = true;
                    //         }
                    //         break;
                    //     default:
                    //         break;
                    // }
                }
            }
        }
        return _flag;
    }
}