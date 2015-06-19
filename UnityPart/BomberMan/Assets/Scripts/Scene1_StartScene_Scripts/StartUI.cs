using UnityEngine;
using System.Collections;

public class StartUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickStartBtn()
	{
		Application.LoadLevel("InterludeScene");
	}
}
