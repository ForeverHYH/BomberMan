using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider collisionInfo)
	{
		Debug.Log("碰撞到的物体的名字是：" + collisionInfo.gameObject.name);
		if(collisionInfo.gameObject.name.Equals("sturdyRobot")||collisionInfo.gameObject.name.Equals("fastRobot"))
		{

		}
		else if(collisionInfo.gameObject.name.Equals("First Person Controller"))
		{
			GameObject.Find("First Person Controller").GetComponent<GameEditor>().isSuccess = true;
			gameObject.SetActive(false);
		}
	} 

}
