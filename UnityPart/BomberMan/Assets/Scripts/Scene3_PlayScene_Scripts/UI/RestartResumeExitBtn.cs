using UnityEngine;
using System.Collections;

public class RestartResumeExitBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartBtnClick()
	{
		StaticComponents.HASDEAD = false;
		GameObject.Find("HPLevel").GetComponent<HPUI>().HPCount=3;
		Application.LoadLevel("LoadingScene");
	}
	
	public void ExitBtnClick()
	{
		StaticComponents.HASDEAD = false;
		GameObject.Find("HPLevel").GetComponent<HPUI>().HPCount=3;
		Application.LoadLevel("StartScene");
	}

	public void ResumeBtnClick()
	{
		GameObject.Find ("PausePanel").SetActive (false);
		GameObject.Find ("RestartPause").SetActive (false);
		GameObject.Find ("ExitPause").SetActive (false);
		GameObject.Find ("Resume").SetActive (false);

		Time.timeScale = 1;
		//GetComponentInParent<Animator> ().SetTrigger ("Resume");
	}
}
