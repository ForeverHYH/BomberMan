using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	// Use this for initialization
	public GameObject Player;
	private int timer;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if(StaticComponents.ISWALKING)
		{
			Player.GetComponent<Animation>().Play("Walking");
		}
		if(!StaticComponents.ISWALKING)
		{
			Player.GetComponent<Animation>().Play("Stand");
		}
		
	}
}
