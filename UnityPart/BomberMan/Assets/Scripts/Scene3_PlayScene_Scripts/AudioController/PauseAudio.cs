using UnityEngine;
using System.Collections;

public class PauseAudio : MonoBehaviour {

	public static ArrayList AudioList = new ArrayList();

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
