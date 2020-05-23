using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] notes;
	private float[] _timing; //譜面のイベント時間 (配列, グローバル変数)
	private int[] _lineNum; //譜面のイベントレーン (配列, グローバル変数)

	public string filePass; //CSVのパス
	private int _notesCount = 0; //ノーツの通し番号

	private AudioSource _audioSource;
	private float _startTime = 0; //開始時刻を保存しておく (仮の初期化, グローバル変数)

	public float timeOffset = -1; //ノーツが落下してくるまでの時間

	private bool _isPlaying = false; //ゲームが行われているかどうか (Boolean, グローバル変数)
	public GameObject startButton;

	public Text scoreText;
	private float _score = 0; //得点 (グローバル変数)
	private int _combo = 0; //コンボ数 (グローバル変数) by natsu-dev

	private int _maxcombo = 0; //最大コンボ数 (グローバル変数) by natsu-dev
	private float _basescore = 0; //基礎点:ノーツ1つあたりのスコア (グローバル変数) by natsu-dev

	void Start () {
		_audioSource = GameObject.Find ("GameMusic").GetComponent<AudioSource> (); //インスタンスにAudioClip情報を格納
		_timing = new float[1024]; //要素数指定してグローバル変数を初期化
		_lineNum = new int[1024]; //要素数指定してグローバル変数を初期化
		LoadCSV (); //CSV読み込みを行い譜面データを _timing, _lineNum 配列に格納
	}

	void Update () {
		if (_isPlaying) { //プレイ中であれば
			CheckNextNotes (); //次のノーツを確認して生成
			scoreText.text = _score.ToString (); //_score(グローバル変数)を取得して表示を更新
		}
	}

	public void StartGame () {
		startButton.SetActive (false); //startButtonを非表示
		_startTime = Time.time; //ゲームを開始した時刻をメモリー
		_audioSource.Play (); //音楽再生
		_isPlaying = true; //プレイ中
	}

	void CheckNextNotes () {
		while (_timing[_notesCount] + timeOffset < GetMusicTime () && _timing[_notesCount] != 0) { //次に流れてくるノーツが1秒以内である時 かつ 次のノーツがある時
			SpawnNotes (_lineNum[_notesCount]); //SpawnNotes(int NUM) NUM番目のレーンにノーツを生成
			_notesCount++; //ノーツの通し番号を次のものに更新 (グローバル変数)
		}
	}

	void SpawnNotes (int num) { //Self Containment
		Instantiate (notes[num], //Instantiate(ORIGINAL, POSITION, ROTATION) :Unityライブラリ関数 -> 引数(コピー元のオブジェクト名, 生成する位置, 向き)
			new Vector3 (-4.0f + (2.0f * num), 10.0f, 0), //Vector3(x, y, z) :Unityライブラリ関数
			Quaternion.identity); // 回転なし (親の軸と同じ)
	}

	void LoadCSV () { //Self Containment
		int i = 0, j;
		TextAsset csv = Resources.Load (filePass) as TextAsset; //ファイル読み込み
		StringReader reader = new StringReader (csv.text); //CSV内容格納
		while (reader.Peek () > -1) { //文字がある間

			string line = reader.ReadLine (); //1行読み込み
			string[] values = line.Split (','); //カンマで区切って配列に格納
			for (j = 0; j < values.Length; j++) { //区切った要素数ループ (2回)
				_timing[i] = float.Parse (values[0]); //_timing[]配列に格納 (グローバル変数)
				_lineNum[i] = int.Parse (values[1]); //_lineNum[]配列に格納 (グローバル変数)
			}
			i++;
		}
		_maxcombo = _lineNum.Length; //最大コンボ数を_lineNum[]配列の要素数から取得 by natsu-dev

		if (_maxcombo >= 30) //コンボ数が30以上のとき
			_basescore = 1000000 / (_maxcombo - 15); //基礎点は1000000点を最大コンボ数-15で割った値
		else //コンボ数が30未満のとき
			_basescore = 1000000 / _maxcombo; // 基礎点は1000000点を最大コンボ数で割った値 以上 by natsu-dev
	}

	float GetMusicTime () { //Self Containment
		return Time.time - _startTime; //開始からのタイムを返す
	}

	public void AddScore(float magni) { //加点のための関数,引数magniは判定ごとのスコア倍率 by natsu-dev

		if (_maxcombo >= 30) { //コンボ数が30以上のときにはスコアは以下の通り傾斜加算
			if (_combo <= 10) //コンボ数が10以下のとき
				_score = _score + _basescore * 0.25 * magni; //スコアに基礎点の25％を加算
			else if (_combo <= 20) //コンボ数が20以下のとき
				_score = _score + _basescore * 0.5 * magni; //スコアに基礎点の50％を加算
			else if (_combo <= 30) //コンボ数が30以下のとき
				_score = _score + _basescore * 0.75 * magni; //スコアに基礎点の75％を加算
			else //コンボ数が31以上のとき
				_score = _score + _basescore * magni; //スコアに基礎点を加算
		}
		else //コンボ数が30未満のときには以下の通り単に基礎点を加算
			_score = _score + _basescore * magni;
	}
	
	public void PerfectTimingFunc (int num) {
		Debug.Log ("Line:" + num + " Perfect!"); //ログ出力
		Debug.Log (GetMusicTime ()); //ログ出力
		EffectManager.Instance.PlayEffect (num); //num番目のエフェクトを表示
		_combo++; //コンボ数を1加算
		AddScore(1); //スコア加算(倍率はPerfectなので1)
	}
}