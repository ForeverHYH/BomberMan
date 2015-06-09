﻿using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
	public GameObject Red;
	public GameObject Player;
	private int deadCount;
	private bool isHit;
	private int timer;
	// Use this for initialization
	void Start () {
		deadCount = 0;
		Red.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(!StaticComponents.HASDEAD)
		{
			if(StaticComponents.ISWALKING)
			{
				Player.GetComponent<Animation>().Play("Walking");
			}
			if(!StaticComponents.ISWALKING)
			{
				Player.GetComponent<Animation>().Play("Stand");
			}

			if(GameObject.Find("HPLevel").GetComponent<HPUI>().HPCount==0 )
			{
				StaticComponents.HASDEAD = true;
				Player.GetComponent<Animation>().Play("Deading");
				GameObject.Find("Main Camera").GetComponent<Transform>().localPosition = new Vector3(0f,0f,0f);
			}
			if(deadCount==5 && !StaticComponents.HASDEAD)
			{
				GameObject.Find("HPLevel").GetComponent<HPUI>().HPCount--;
			}
			if(deadCount==10 && !StaticComponents.HASDEAD)
			{
				GameObject.Find("HPLevel").GetComponent<HPUI>().HPCount--;
			}
			if(!isHit)
			{
				deadCount = 0;
				Red.SetActive (false);
			}
			if(timer>=10)
			{
				timer = 0;
				isHit = false;
			}
			timer++;
		}

	}

	void OnParticleCollision (GameObject other)
	{
		isHit = true;
		deadCount++;
		//Debug.Log ("dead:"+deadCount);
		Red.SetActive (true);

	}
}