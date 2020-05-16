using System.Collections;
using UnityEngine;

public class NotesTimingMaker : MonoBehaviour {

	private AudioSource _audioSource;
	private float _startTime = 0;
	private CSVWriter _CSVWriter;

	private bool _isPlaying = false;
	public GameObject startButton;
	public GameObject stopButton;

	void Start () {
		_audioSource = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
		_CSVWriter = GameObject.Find ("CSVWriter").GetComponent<CSVWriter> ();
		stopButton.SetActive (false);
	}

	void Update () {
		if (_isPlaying) {
			DetectKeys ();
		}
	}

	public void StartMusic () {
		startButton.SetActive (false);
		stopButton.SetActive (true);
		_audioSource.Play ();
		_startTime = Time.time;
		_isPlaying = true;
	}

	public void StopMusic () {
		stopButton.SetActive (false);
		_audioSource.Stop ();
		_isPlaying = false;
		WriteNotesTiming (-1);
		Application.Quit();	// エディタ内実行では無視される
	}

	void DetectKeys () {
		if (Input.GetKeyDown (KeyCode.D)) {
			WriteNotesTiming (0);
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			WriteNotesTiming (1);
		}

		if (Input.GetKeyDown (KeyCode.G)) {
			WriteNotesTiming (2);
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			WriteNotesTiming (3);
		}

		if (Input.GetKeyDown (KeyCode.J)) {
			WriteNotesTiming (4);
		}
	}

	void WriteNotesTiming (int num) {
		Debug.Log (GetTiming ());
		_CSVWriter.WriteCSV (GetTiming ().ToString () + "," + num.ToString ());
	}

	float GetTiming () {
		return Time.time - _startTime;
	}
}