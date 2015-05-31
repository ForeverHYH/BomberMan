using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {

	// Use this for initialization
	public int typeOfWall;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("OnTriggerEnter:" + other.gameObject.name);
	}
}
