using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCannonUI : MonoBehaviour {
	public Sprite Level1Texture;
	public Sprite Level2Texture;
	public Sprite Level3Texture;
	
	public int toolCount;
	// Use this for initialization
	void Start () {
		GetComponent<Image> ().sprite = Level1Texture;
		toolCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		switch (toolCount) {
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
}
