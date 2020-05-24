using System.Collections;
using UnityEngine;

public class NotesScript : MonoBehaviour {

	public int lineNum; //Unity内Scriptから取得
	private GameController _gameManager;
	private bool isInLine = false; //Line上にノーツがあるか
	private KeyCode _lineKey;

	void Start () {
		_gameManager = GameObject.Find ("GameManager").GetComponent<GameController> (); //インスタンスに GameController.cs 情報を格納
		_lineKey = GameUtil.GetKeyCodeByLineNum (lineNum); //ノーツに割り当てられているキーを取得
	}

	void Update () {
		this.transform.position += Vector3.down * 10 * Time.deltaTime; //落下させる

		if (this.transform.position.y < -3.8f) { //下枠よりも下に落ちた場合
			Debug.Log ("false"); //ログ出力
			Destroy (this.gameObject); //オブジェクト削除
			_gameManager._combo = 0; //コンボ数を初期化 by natsu-dev
		}

		if (isInLine) { //Line上にノーツあれば
			CheckInput (_lineKey); //キーを押されるかのチェック
		}
	}

	void OnTriggerEnter (Collider other) { //BoxColliderコンポーネントの isTrigger アクション (Unity標準関数)
		isInLine = true; //Line上にノーツ有り
	}

	void OnTriggerExit (Collider other) { //BoxColliderコンポーネントの isTrigger アクション (Unity標準関数)
		isInLine = false; //Line上にノーツ無し
	}

	void CheckInput (KeyCode key) {

		if (Input.GetKeyDown (key) || CheckTouch ()) { //キーの入力が確認できたら
			_gameManager.PerfectTimingFunc (lineNum); //エフェクト＆スコア加算
			Destroy (this.gameObject); //オブジェクト削除
		}
	}

	bool CheckTouch () { //By smpny7 (仮)
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				Debug.Log ("押した瞬間");
				return true;
			} else {
				return false;
			}

			// if (touch.phase == TouchPhase.Ended) {
			// Debug.Log("離した瞬間");
			// }

			// if (touch.phase == TouchPhase.Moved) {
			// 	Debug.Log("押しっぱなし");
			// }
		} else {
			return false;
		}
	}
}