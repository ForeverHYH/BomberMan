using UnityEngine;
using System.Collections;

public class WallCube : MonoBehaviour {

	private int timer = 0;
	private bool isDestory = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestory) 
		{
			timer++;
			if(timer==30) 
			{
				//Destroy(gameObject);
				gameObject.SetActive(false);
				timer = 0;
				isDestory = false;
			}
		}
	}

	void OnParticleCollision (GameObject other)
	{
		//Debug.Log (gameObject.name);
		isDestory = true;
	}
}
