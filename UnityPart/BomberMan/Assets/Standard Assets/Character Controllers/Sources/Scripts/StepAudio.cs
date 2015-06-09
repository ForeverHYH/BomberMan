using UnityEngine;
using System.Collections;

public class StepAudio : MonoBehaviour {

	public AudioSource music;
	private bool isPlaying;
	//音量   
	//public float musicVolume;      
	
	void Start() { 
		isPlaying = false;
		//设置默认音量   
		//musicVolume = 0.5f;       
	}
	
	// Update is called once per frame
	void Update () {
		if(StaticComponents.ISWALKING)
		{
			switch((int)GetComponent<CharacterMotor>().movement.maxForwardSpeed)
			{
			case 1:
				Debug.Log("step 1");
				music.pitch = 1.0f;
				break;
			case 2:
				music.pitch = 2.0f;
				break;
			case 3:
				music.pitch = 3.0f;
				break;
			}
			if(!isPlaying)
			{
				music.Play();
				isPlaying = true;
			}
		}
		else{
			music.Stop();
			isPlaying = false;
		}
	}
}
