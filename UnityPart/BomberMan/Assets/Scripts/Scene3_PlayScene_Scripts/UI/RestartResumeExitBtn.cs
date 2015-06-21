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
		Screen.lockCursor = true;
	}
	
	public void ExitBtnClick()
	{
		//Application.Quit ();
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
		Screen.lockCursor = true;
		Time.timeScale = 1;

		if(GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().isTomScript2Show)
		{
			GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().TomScript2.SetActive (true);
		}
		if(GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().isTomScript3Show)
		{
			GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().TomScript3.SetActive (true);
		}
//		if(GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().isTomPause)
//		{
//			GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().TomVoice.Play ();
//			GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().isTomPause = false;
//		}
//		if(GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().isDocPause)
//		{
//			GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().DocVoice.Play ();
//			GameObject.Find ("First Person Controller").GetComponent<GameEditor> ().isDocPause = false;
//		}

		//GetComponentInParent<Animator> ().SetTrigger ("Resume");
	}
}
