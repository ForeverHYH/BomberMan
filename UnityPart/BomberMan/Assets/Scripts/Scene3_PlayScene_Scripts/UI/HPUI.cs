using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPUI : MonoBehaviour {

	public Sprite Level0Texture;
	public Sprite Level1Texture;
	public Sprite Level2Texture;
	public Sprite Level3Texture;

	public int HPCount;
	// Use this for initialization
	void Start () {
		HPCount = 3;
	}
	
	// Update is called once per frame
	void Update () {
		switch (HPCount) {
		case 0:
			GetComponent<Image> ().sprite = Level0Texture;
			break;
		case 1:
			GetComponent<Image> ().sprite = Level1Texture;
			break;
		case 2:
			GetComponent<Image> ().sprite = Level2Texture;
			break;
		case 3:
			GetComponent<Image> ().sprite = Level3Texture;
			break;
		default:
			break;
		}
	}

	public void HPreduce()
	{
		HPCount--;
	}
}
