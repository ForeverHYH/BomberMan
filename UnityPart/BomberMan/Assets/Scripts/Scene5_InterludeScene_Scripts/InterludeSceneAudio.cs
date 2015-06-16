using UnityEngine;
using System.Collections;

public class InterludeSceneAudio : MonoBehaviour {

	public AudioSource Aside1Music;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		Aside1Music.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!Aside1Music.isPlaying)
		{
			Application.LoadLevel("PlayScene");
		}
	}

}
