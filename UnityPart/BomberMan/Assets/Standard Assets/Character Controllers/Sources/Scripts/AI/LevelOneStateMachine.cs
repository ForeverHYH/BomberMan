using UnityEngine;
using System.Collections;

public class LevelOneStateMachine : MonoBehaviour {
	
	private AIAction levelOne;
	private RaycastHit hit;
	private int restTimer;
	private int restTime;

	private bool isTurn;
	private bool isCollisiontoRobot;

	void Awake() {
		levelOne = GetComponent<AIAction> ();
	}

	void Start () {
		restTime = 20;
		restTimer = 0;
	}

	void Update () {
		if(levelOne.isInDanger())
		{
			levelOne.StopState();
			levelOne.TurnState(true);
			levelOne.TurnState(true);
		}

		else if(isCollisiontoRobot&&restTimer<=restTime)
		{
			levelOne.StopState();
			if(restTimer==restTime){
				levelOne.TurnState(true);
				levelOne.TurnState(true);

				restTimer = 0;
			}
			else restTimer++;
		}

		else if(Physics.Raycast(transform.position,transform.forward ,out hit,0.6f)&&restTimer<=restTime)
		{
			levelOne.StopState();
			if(restTimer==restTime){
				levelOne.TurnState(false);
				restTimer = 0;
			}
			else restTimer++;
		}
		else
		{
			levelOne.WalkState();
		}
	}

	void OnParticleCollision (GameObject other)
	{
		levelOne.DeadState ();
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		Debug.Log("碰撞到的物体的名字是：" + collisionInfo.gameObject.name);
		if(collisionInfo.gameObject.name.Equals("sturdyRobot")||collisionInfo.gameObject.name.Equals("fastRobot"))
		{
			isCollisiontoRobot = true;
		}
	} 

}

