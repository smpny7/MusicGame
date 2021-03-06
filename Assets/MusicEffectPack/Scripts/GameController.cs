﻿using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int _notesCount = 0; //ノーツの通し番号
    private float[] _timing; //譜面のイベント時間 (配列, グローバル変数)
    private int[] _lineNum; //譜面のイベントレーン (配列, グローバル変数)
    public int _combo = 0; //コンボ数
    public int _maxcombo = 0; //最大コンボ数
    public double _score = 0; //得点
    public double _basescore = 0; //基礎点:ノーツ1つあたりのスコア
    public string filePass; //CSVのパス
    private float _startTime = 0; //開始時刻を保存しておく (仮の初期化, グローバル変数)
    public float timeOffset = -1; //ノーツが落下してくるまでの時間
    private bool _isPlaying = false; //ゲームが行われているかどうか (Boolean, グローバル変数)

    public GameObject[] notes;
    private AudioSource _audioSource;
    public GameObject startButton;
    private AudioSource[] _GameSoundEffects; //Perfect, Great, BadのSE情報を格納（グローバル変数）．複数音あるため配列 by chishige
    public Text scoreText;

    void Start () {
        _GameSoundEffects = GameObject.Find ("GameSoundEffect").GetComponents<AudioSource> (); //インスタンスにAudioClip情報を格納
        _audioSource = GameObject.Find ("GameMusic").GetComponent<AudioSource> (); //インスタンスにAudioClip情報を格納
        _timing = new float[1024]; //要素数指定してグローバル変数を初期化
        _lineNum = new int[1024]; //要素数指定してグローバル変数を初期化
        LoadCSV (); //CSV読み込みを行い譜面データを _timing, _lineNum 配列に格納
    }

    void Update () {
        if (_isPlaying) { //プレイ中であれば
            CheckNextNotes (); //次のノーツを確認して生成
        }
    }

    public async void StartGame () {
        startButton.SetActive (false); //startButtonを非表示
        _startTime = Time.time; //ゲームを開始した時刻をメモリー
        _isPlaying = true; //プレイ中
        await Task.Delay (1000);
        _audioSource.Play (); //音楽再生
    }

    void CheckNextNotes () {
        while (_timing[_notesCount] + timeOffset < GetMusicTime () && _timing[_notesCount] != 0) { //次に流れてくるノーツが1秒以内である時 かつ 次のノーツがある時
            SpawnNotes (_lineNum[_notesCount]); //SpawnNotes(int NUM) NUM番目のレーンにノーツを生成
            _notesCount++; //ノーツの通し番号を次のものに更新 (グローバル変数)
        }
    }

    void SpawnNotes (int num) {
        Instantiate (notes[num], //Instantiate(ORIGINAL, POSITION, ROTATION) :Unityライブラリ関数 -> 引数(コピー元のオブジェクト名, 生成する位置, 向き)
            new Vector3 (-5f + (2.5f * num), 9f, 0), //Vector3(x, y, z) :Unityライブラリ関数
            Quaternion.identity); // 回転なし (親の軸と同じ)
    }

    void LoadCSV () {
        int i = 0;
        TextAsset csv = Resources.Load (filePass) as TextAsset; //ファイル読み込み
        StringReader reader = new StringReader (csv.text); //CSV内容格納
        while (reader.Peek () > -1) { //文字がある間
            string line = reader.ReadLine (); //1行読み込み
            string[] values = line.Split (','); //カンマで区切って配列に格納
            _timing[i] = float.Parse (values[0]) + 1; //_timing[]配列に格納 (グローバル変数)
            _lineNum[i] = int.Parse (values[1]); //_lineNum[]配列に格納 (グローバル変数)
            i++;
        }
        _maxcombo = i;

        if (_maxcombo >= 30) { //コンボ数が30以上のとき
            _basescore = 1000000 / ((double) _maxcombo - 15); //基礎点は1000000点を最大コンボ数-15で割った値
        } else { //コンボ数が30未満のとき
            _basescore = 1000000 / (double) _maxcombo; // 基礎点は1000000点を最大コンボ数で割った値
        }
    }

    float GetMusicTime () {
        return Time.time - _startTime; //開始からのタイムを返す
    }

    public async void AddScore (double magni) { //加点のための関数,引数magniは判定ごとのスコア倍率 by natsu-dev
        double ScoreTemp = 0;
        if (_maxcombo >= 30) { //コンボ数が30以上のときにはスコアは以下の通り傾斜加算
            if (_combo <= 10) //コンボ数が10以下のとき
                ScoreTemp = _basescore * 0.25 * magni; //スコアに基礎点の25％を加算
            else if (_combo <= 20) //コンボ数が20以下のとき
                ScoreTemp = _basescore * 0.5 * magni; //スコアに基礎点の50％を加算
            else if (_combo <= 30) //コンボ数が30以下のとき
                ScoreTemp = _basescore * 0.75 * magni; //スコアに基礎点の75％を加算
            else //コンボ数が31以上のとき
                ScoreTemp = _basescore * magni; //スコアに基礎点を加算
        } else { //コンボ数が30未満のときには以下の通り単に基礎点を加算
            ScoreTemp = _basescore * magni;
        }
        for (int i = 0; i < 15; i++) //100分割したものを5ミリ秒ごとに100回加算()
        {
            _score += ScoreTemp / 15;
            scoreText.text = ((int) Math.Round (_score, 0, MidpointRounding.AwayFromZero)).ToString ("D7"); //四捨五入して型変換を行い表示を更新
            await Task.Delay (33);
        }
    }

    public void GameSoundEffect (int num) { //by chishige
        _GameSoundEffects[num].PlayOneShot (_GameSoundEffects[num].clip); //PlayOneShotは効果音で使う（引数にClip情報が必要）
        // Debug.Log ("GameSoundEffect Played.");
    }

    public void PerfectTimingFunc (int num) {
        // Debug.Log ("Line:" + num + " Perfect!"); //ログ出力
        // Debug.Log (GetMusicTime ()); //ログ出力
        EffectManager.Instance.PlayEffect (num); //num番目のエフェクトを表示
        GameSoundEffect (0); //Perfectサウンド（引数0）を再生
        _combo++; //コンボ数を1加算
        AddScore (1); //スコア加算(倍率はPerfectなので1)
    }

    public void GreatTimingFunc (int num) {
        GameSoundEffect (1); //Greatサウンド再生
        EffectManager.Instance.PlayEffect (num); //num番目のエフェクトを表示
        _combo++; //コンボ数を1加算
        AddScore (0.75f); //スコア加算(倍率はGreatなので0.75)
    }
}