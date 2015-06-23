using UnityEngine;
using System.Collections;

public class GameEditor : MonoBehaviour {

	public GameObject CeilingCube;
	public GameObject GroundCube;
	public GameObject CannonCube;
	public GameObject Chip;

	public GameObject TomScript2;
	public GameObject TomScript3;

	public int clickCannonTime;
	public int seeStupidRobotTime;

	public AudioSource TomVoice;
	public AudioSource DocVoice;

	public int timer;

	private bool isCeilingCubeShow;

	public bool isSuccess;

	public bool isTomScript2Show;
	public bool isTomScript3Show;


	// Use this for initialization
	void Start () {
		isSuccess = false;
		timer = 0;
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

		DocVoice = GetComponent<AsideAudioController>().Doc1Music;
		isCeilingCubeShow = true;
		clickCannonTime = 0;
		seeStupidRobotTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!StaticComponents.HASDEAD)
		{
			TomVoice.pitch = 1 * Time.timeScale;
			DocVoice.pitch = 1 * Time.timeScale; 
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
					
					isTomScript2Show = true;
					TomScript2.SetActive(true);
				}
				if(timer==31 && !TomVoice.isPlaying )
				{
					GetComponent<MouseLook>().maxCanonNumber = 1;
					timer++;
					
					isTomScript2Show = false;
					TomScript2.SetActive(false);
				}
				//voice 4 click cannon 
				if(clickCannonTime==1 && !TomVoice.isPlaying && timer==32)
				{
					timer++;
					TomVoice.Stop();
					TomVoice = GetComponent<AsideAudioController>().Tom4Music;
					TomVoice.Play ();
					
					isTomScript3Show = true;
					TomScript3.SetActive(true);
					
				}
				// TomScript3 flase
				if(!TomVoice.isPlaying && timer==33)
				{
					timer++;
					
					isTomScript3Show = false;
					TomScript3.SetActive(false);
				}
				//voice 5 see robot
				if(seeStupidRobotTime==1 && timer ==34)
				{
					timer ++;
					TomVoice.Stop();
					TomVoice = GetComponent<AsideAudioController>().Tom5Music;
					TomVoice.Play ();
				}
				
				
				if(GetComponent<SenceLoad>().CreatureList.Count==0 && timer==35)
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
				if (!TomVoice.isPlaying && isCeilingCubeShow) {
					CeilingCube.SetActive (false);
					timer++;
				}
				if (timer == 30 && !TomVoice.isPlaying) {
					isCeilingCubeShow = false;
					CeilingCube.SetActive (true);
					
				}
				if(GetComponent<SenceLoad>().CreatureList.Count==0 && timer==30)
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
			case 3:{
				if (!TomVoice.isPlaying && isCeilingCubeShow) {
					CeilingCube.SetActive (false);
					timer++;
				}
				if (timer == 30 && !TomVoice.isPlaying) {
					isCeilingCubeShow = false;
					CeilingCube.SetActive (true);
					timer++;
					DocVoice.Play ();
				}
				
				if(GetComponent<SenceLoad>().CreatureList.Count==0 && timer==31)
				{
					timer++;
					Instantiate(Chip, new Vector3(7.0f,1.0f,7.0f), Quaternion.identity);
					Chip.transform.localEulerAngles = new Vector3(45.0f,0.0f,45.0f);
					RenderSettings.fogDensity = 0;
				}
				
				if(isSuccess && timer==32)
				{
					timer++;
					DocVoice = GetComponent<AsideAudioController>().Doc2Music;
					DocVoice.Play ();
				}
				
				if(!DocVoice.isPlaying  && timer==33)
				{
					timer++;
					GetComponent<BGMAudioController>().SuccessBGM.Play();
					GameObject.Find ("Canvas").GetComponent<Animator>().SetTrigger("Success");
					//play to be continue
				}
				if(GameObject.Find ("Canvas").GetComponent<SuccessUI>().isJumptoMainView)
				{
					GameObject.Find ("Canvas").GetComponent<SuccessUI>().isJumptoMainView = false;
					Screen.lockCursor = false;
					Application.LoadLevel("StartScene");
				}
				break;
			}
			}
		}


	}
}
