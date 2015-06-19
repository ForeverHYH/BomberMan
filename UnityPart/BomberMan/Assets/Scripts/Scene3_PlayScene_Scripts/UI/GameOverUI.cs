using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

	private bool isShow;
	public bool isGameStop;  // Only controlled by animator:GameOver

	public AudioSource GameOverMusic;

	// Use this for initialization
	void Start () {
		isShow = false;

	}

	void Awak(){
		isGameStop = false;
	}

	// Update is called once per frame
	void Update () {
		if(StaticComponents.HASDEAD && !isShow)
		{
			Screen.lockCursor = false;
			isShow = true;
			GetComponent<Animator>().SetTrigger("GameOver");
			GameOverMusic.Play();
		}
		if(isGameStop)
		{
			Time.timeScale = 0;
			GameObject.Find("First Person Controller").GetComponent<PauseAudio>().PauseMusic();
			GameOverMusic.Stop();
		}
	}

}
