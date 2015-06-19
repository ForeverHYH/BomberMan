using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {


	public GameObject Panel;
	public GameObject Restart;
	public GameObject Exit;
	public GameObject Resume;

	public bool isPause;
	private int timer;
	// Use this for initialization
	void Start () {
		Panel.SetActive (false);
		Restart.SetActive (false);
		Exit.SetActive (false);
		Resume.SetActive (false);
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			isPause = true;
			Pause();
		}
		else{
			isPause = false;
		}

	}

	void Pause()
	{
		Screen.lockCursor = false;
		Panel.SetActive (true);
		Restart.SetActive (true);
		Exit.SetActive (true);
		Resume.SetActive (true);

		Time.timeScale = 0;
		GameObject.Find("First Person Controller").GetComponent<PauseAudio>().PauseMusic();

	}


}
