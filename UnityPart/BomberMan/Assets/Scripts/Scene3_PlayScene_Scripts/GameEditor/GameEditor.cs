using UnityEngine;
using System.Collections;

public class GameEditor : MonoBehaviour {

	public GameObject CeilingCube;
	public GameObject GroundCube;
	public GameObject CannonCube;
	public GameObject Chip;

	public int clickCannonTime;
	public int seeStupidRobotTime;

	public AudioSource TomVoice;
	public AudioSource DocVoice;

	private int timer;

	private bool isCeilingCubeShow;

	public bool isSuccess;

	// Use this for initialization
	void Start () {
		isSuccess = false;
		switch(StaticComponents.CURRENT_LEVEL)
		{
		case 1:
			TomVoice = GetComponent<AsideAudioController> ().Tom2Music;
			TomVoice.Play ();
			break;
		case 2:
			TomVoice = GetComponent<AsideAudioController> ().Tom6Music;
			TomVoice.Play ();
			break;
		case 3:
			TomVoice = GetComponent<AsideAudioController> ().Tom7Music;
			TomVoice.Play ();
			break;
		}


		isCeilingCubeShow = true;
		clickCannonTime = 0;
		seeStupidRobotTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch(StaticComponents.CURRENT_LEVEL)
		{
		case 1:{
			//Voice 3 ceiling cube hide
			if (!TomVoice.isPlaying && isCeilingCubeShow) {
				CeilingCube.SetActive (false);
				timer++;
			}
			if (timer == 30 && !TomVoice.isPlaying) {
				isCeilingCubeShow = false;
				CeilingCube.SetActive (true);
				TomVoice = GetComponent<AsideAudioController>().Tom3Music;
				TomVoice.Play ();
				GetComponent<MouseLook>().maxCanonNumber = 0;
				timer++;
				Debug.Log(timer);
			}
			if(timer==31 && !TomVoice.isPlaying )
			{
				GetComponent<MouseLook>().maxCanonNumber = 1;
				timer++;
			}
			//voice 4 click cannon 
			if(clickCannonTime==1 && !TomVoice.isPlaying && timer==32)
			{
				timer++;
				TomVoice.Stop();
				TomVoice = GetComponent<AsideAudioController>().Tom4Music;
				TomVoice.Play ();
			}
			//voice 5 see robot
			if(seeStupidRobotTime==1 && !TomVoice.isPlaying )
			{
				TomVoice.Stop();
				TomVoice = GetComponent<AsideAudioController>().Tom5Music;
				TomVoice.Play ();
			}
			
			
			if(GetComponent<SenceLoad>().CreatureList.Count==0 && timer==33)
			{
				timer++;
				CannonCube.SetActive(false);
				GroundCube.SetActive (false);
				GetComponent<BGMAudioController>().SuccessBGM.Play();
			}
			if((int)gameObject.transform.position.y < -2)
			{
				StaticComponents.CURRENT_LEVEL++;
				Application.LoadLevel("LoadingScene");
			}
			break;
		}
		case 2:{
			if(GetComponent<SenceLoad>().CreatureList.Count==0 && timer==0)
			{
				timer++;
				CannonCube.SetActive(false);
				GroundCube.SetActive (false);
				GetComponent<BGMAudioController>().SuccessBGM.Play();
			}
			break;
		}
		case 3:{
			if (!TomVoice.isPlaying && isCeilingCubeShow) {
				CeilingCube.SetActive (false);
				timer++;
			}
			if (timer == 30 && !TomVoice.isPlaying) {
				isCeilingCubeShow = false;
				CeilingCube.SetActive (true);
				DocVoice = GetComponent<AsideAudioController>().Doc1Music;
				DocVoice.Play ();
			}

			if(GetComponent<SenceLoad>().CreatureList.Count==0 && timer==30)
			{
				timer++;
				Instantiate(Chip, new Vector3(7.0f,1.0f,7.0f), Quaternion.identity);
			}

			if(isSuccess && timer==31)
			{
				timer++;
				DocVoice = GetComponent<AsideAudioController>().Doc2Music;
				DocVoice.Play ();
				GetComponent<BGMAudioController>().SuccessBGM.Play();
			}

			if(!DocVoice.isPlaying  && timer==32)
			{
				timer++;
				//play to be continue
			}
			break;
		}
	  }

	}
}
