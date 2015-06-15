using UnityEngine;
using System.Collections;

public class BGMAudioController : MonoBehaviour {

	public AudioSource LevelOneBGM;
	public AudioSource LevelTwoBGM;
	public AudioSource LevelThreeBGM;

	public AudioSource DeadBGM;
	public AudioSource SuccessBGM;

	private AudioSource CurrentBGM;

	private bool isContinue;
	private bool isStop;
	public bool isPause;
	// Use this for initialization
	void Start () {
		switch (StaticComponents.CURRENT_LEVEL) {
		case 1:
			CurrentBGM = LevelOneBGM;
			break;
		case 2:
			CurrentBGM = LevelTwoBGM;
			break;
		case 3:
			CurrentBGM = LevelThreeBGM;
			break;
				}

		CurrentBGM.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if(StaticComponents.HASDEAD && !isStop)
		{
			isStop = true;
			CurrentBGM.Stop();
		}

		if(GameObject.Find("Canvas").GetComponent<PauseUI>().isPause)
		{
			isContinue = false;
			CurrentBGM.Pause();
		}
		else if(!GameObject.Find("Canvas").GetComponent<PauseUI>().isPause && !isContinue)
		{
			isContinue = true;
			CurrentBGM.Play();
		}
	}
}
