using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

	private bool isShow;
	public bool isGameStop;  // Only controlled by animator:GameOver

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
			isShow = true;
			GetComponent<Animator>().SetTrigger("GameOver");
		}
		if(isGameStop)
		{
			Time.timeScale = 0;
			GameObject.Find("First Person Controller").GetComponent<PauseAudio>().PauseMusic();
		}
	}

}
