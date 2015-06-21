using UnityEngine;
using System.Collections;

public class PauseAudio : MonoBehaviour {

	public static ArrayList AudioList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (AudioList.Count);
	}

	public void PauseMusic(){

		if(AudioList.Count>0)
		{
			foreach(AudioSource i in AudioList)
			{
				i.Pause();
			}
		}

//		if(GetComponent<GameEditor> ().TomVoice.isPlaying)
//		{
//			GetComponent<GameEditor> ().TomVoice.Pause ();
//			GetComponent<GameEditor> ().isTomPause = true;
//		}
//		if(GetComponent<GameEditor> ().DocVoice.isPlaying)
//		{
//			GetComponent<GameEditor> ().DocVoice.Pause ();
//			GetComponent<GameEditor> ().isDocPause = true;
//		}

	}

//	public void ResumeMusic(){
//		if(AudioList.Count>0)
//		{
//			foreach(AudioSource i in AudioList)
//			{
//
//			}
//		}
//	}
}
