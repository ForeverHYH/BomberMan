using UnityEngine;
using System.Collections;
using System.IO;

public class NewGame : MonoBehaviour {
	private GUITexture tempGuiTexture;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		if (GUI.Button (new Rect (350, 160, 100, 30), "Start")) 
		{
			Application.LoadLevel("PlayScene");
		}
		if (GUI.Button (new Rect (350, 220, 120, 30), "Editor")) 
		{
			Application.LoadLevel("MapEditor");
		}
		tempGuiTexture = GetComponent<GUITexture>();
		//tempGuiTexture.pixelInset = new Rect(0, 0,0,0);

	}
}
