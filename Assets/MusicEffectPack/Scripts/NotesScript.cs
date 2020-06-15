using System.Collections;
using UnityEngine;

public class NotesScript : MonoBehaviour {

    public int lineNum; //Unity内Scriptから取得
    private int isInLineLevel = 0; //Perfect, Great, Bad判定の区別をする（0: 判定外, 1: Bad, 2: Great, 3: Perfect）
    private GameController _gameManager;
    private KeyCode _lineKey;
    private static TouchInput _touchInput;
    private LaneEffect[] _laneEffect = new LaneEffect[5];

    void Start () {
        _gameManager = GameObject.Find ("GameManager").GetComponent<GameController> (); //インスタンスに GameController.cs 情報を格納
        _touchInput = GameObject.Find ("TouchInput").GetComponent<TouchInput> ();
        _lineKey = GameUtil.GetKeyCodeByLineNum (lineNum); //ノーツに割り当てられているキーを取得
        _laneEffect[0] = GameObject.Find ("Lane1").GetComponent<LaneEffect> ();
        _laneEffect[1] = GameObject.Find ("Lane2").GetComponent<LaneEffect> ();
        _laneEffect[2] = GameObject.Find ("Lane3").GetComponent<LaneEffect> ();
        _laneEffect[3] = GameObject.Find ("Lane4").GetComponent<LaneEffect> ();
        _laneEffect[4] = GameObject.Find ("Lane5").GetComponent<LaneEffect> ();
    }

    void Update () {
        this.transform.position += Vector3.down * 10 * Time.deltaTime; //落下させる

        if (this.transform.position.y < -3.8f) { //下枠よりも下に落ちた場合
            Destroy (this.gameObject); //オブジェクト削除
            _gameManager._combo = 0; //コンボ数を初期化 by natsu-dev
        }
        if (isInLineLevel >= 1) { //Bad判定内にノーツあれば
            CheckInput (_lineKey); //キーを押されるかのチェック
        }
    }

    void OnTriggerEnter (Collider other) { //BoxColliderコンポーネントの isTrigger アクション (Unity標準関数)
        if (other.gameObject.tag == "BadJudge") {
            isInLineLevel++; //Bad判定内にノーツ有り
            // Debug.Log("Bad OK.");
        }
        if (other.gameObject.tag == "GreatJudge") {
            isInLineLevel++; //Great判定内にノーツ有り
            // Debug.Log("Great OK.");
        }
        if (other.gameObject.tag == "PerfectJudge") {
            isInLineLevel++; //Perfect判定内にノーツ有り
            // Debug.Log("Perfect OK.");
        }
    }

    void OnTriggerExit (Collider other) { //BoxColliderコンポーネントの isTrigger アクション (Unity標準関数)
        if (other.gameObject.tag == "BadJudge") {
            isInLineLevel--; //Bad判定内にノーツ無し
            // Debug.Log("Bad No.");
        }
        if (other.gameObject.tag == "GreatJudge") {
            isInLineLevel--; //Great判定内にノーツ無し
            // Debug.Log("Great No.");
        }
        if (other.gameObject.tag == "PerfectJudge") {
            isInLineLevel--; //Perfect判定内にノーツ無し
            // Debug.Log("Perfect No.");
        }
    }

    void CheckInput (KeyCode key) {
        if (Input.GetKeyDown (key) || TouchCheck.CheckTouch (lineNum, _touchInput)) { //キーの入力が確認できたら
            switch (isInLineLevel) {
                case 1:
                    _gameManager.GameSoundEffect (2);
                    Destroy (this.gameObject); //オブジェクト削除
                    break;
                case 2:
                    _gameManager.GreatTimingFunc (lineNum); //エフェクト＆スコア加算
                    Destroy (this.gameObject); //オブジェクト削除
                    break;
                case 3:
                    _gameManager.PerfectTimingFunc (lineNum); //エフェクト＆スコア加算
                    Destroy (this.gameObject); //オブジェクト削除
                    break;
            }
            _laneEffect[lineNum].PlayLaneEffect ();
        }
    }
}