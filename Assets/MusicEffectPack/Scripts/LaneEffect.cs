using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LaneEffect : MonoBehaviour { //By smpny7

    private KeyCode _lineKey;

    public int _lineNum;

    private Image m_Image; //コンポーネントを格納
    public Sprite[] m_Sprite; //オブジェクト格納

    void Start () {
        m_Image = GetComponent<Image> (); //Imageリンク
        _lineKey = GameUtil.GetKeyCodeByLineNum (_lineNum); //割り当てキーをGameUtilから取得
    }

    async void Update () {
        if (Input.GetKeyDown (_lineKey)) { //キーボードが押された時
            m_Image.sprite = m_Sprite[1]; //画像変更
            await Task.Delay (150); //0.15秒間待機
            m_Image.sprite = m_Sprite[0]; //画像変更
        }
    }
}