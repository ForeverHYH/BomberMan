using UnityEngine;
using System.Collections;

public class DeadAudio : MonoBehaviour {

	public AudioSource DeadMusic;
	private bool isPlayMusic;
	// Use this for initialization
	void Start () {
		isPlayMusic = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(StaticComponents.HASDEAD && !isPlayMusic)
		{
			isPlayMusic = true;
			DeadMusic.Play();
		}
	}
}
