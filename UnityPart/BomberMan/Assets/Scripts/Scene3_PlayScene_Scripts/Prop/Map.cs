using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public AudioSource music;
	private bool startPlay;
	private int timer;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(timer==1)
		{
			Debug.Log("play");
			music.Play();
		}
		if(startPlay)
		{
			timer++;
		}
		if(timer==8)
		{
			gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider collisionInfo)
	{
		startPlay = true;
		Debug.Log("碰撞到的物体的名字是：" + collisionInfo.gameObject.name);
		if(collisionInfo.gameObject.name.Equals("sturdyRobot")||collisionInfo.gameObject.name.Equals("fastRobot"))
		{
		}
		else if(collisionInfo.gameObject.name.Equals("First Person Controller"))
		{
			GameObject.Find("Map").GetComponent<MapUI>().startCounting();

		}
	} 
}
