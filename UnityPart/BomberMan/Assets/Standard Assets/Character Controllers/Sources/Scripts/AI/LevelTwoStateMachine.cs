using UnityEngine;
using System.Collections;

public class LevelTwoStateMachine : MonoBehaviour {
	
	private AIAction levelTwo;
	private RaycastHit hit;
	private int canonID = -1;

	private int restTimer;
	private int restTime;
	private int shotTimer;
	private int shotTime;
	private bool isCollisiontoRobot;
	private bool hasTurn;
	void Awake() {
		levelTwo = GetComponent<AIAction> ();
	}

	void Start () {
		restTime = 20;
		restTimer = 0;
		shotTime = 50;
		shotTimer = 0;
	}

	void Update () {
		if(levelTwo.isInDanger())
		{
			levelTwo.StopState();
			levelTwo.TurnState(true);
			levelTwo.TurnState(true);
		}

		else if(isCollisiontoRobot&&restTimer<=restTime&&!hasTurn)
		{
			levelTwo.StopState();
			if(restTimer==restTime){
				levelTwo.TurnState(true);
				levelTwo.TurnState(true);
				hasTurn=true;
				isCollisiontoRobot = false;
				restTimer = 0;
			}
			else restTimer++;
		}

		else if(Physics.Raycast(transform.position,transform.forward ,out hit,0.55f)&&restTimer<=restTime)
		{
			levelTwo.StopState();
			if(restTimer==restTime){
				levelTwo.TurnState(false);
				restTimer = 0;
			}
			else restTimer++;
		}

		else if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,2)&&hit.collider.transform.name.Equals("wall")&&shotTimer>=shotTime)
		{
			shotTimer=0;
			Debug.DrawLine(transform.position,hit.point,Color.red);
			int canonX = (int)hit.collider.transform.position.x;
			int canonZ = (int)hit.collider.transform.position.z;
			int robotX = (int)transform.position.x;
			int robotZ = (int)transform.position.z;
			int targetX;
			int targetZ;
			if(canonX==robotX)
			{
				targetX = canonX;
				targetZ = (int)((canonZ+robotZ+1)/2);
				levelTwo.ShotState(targetX,targetZ);
			}
			else if(canonZ==robotZ)
			{
				targetZ = canonZ;
				targetX = (int)((canonX+robotX+1)/2);
				levelTwo.ShotState(targetX,targetZ);
			}
		}

		else
		{
			levelTwo.WalkState();
			hasTurn = false;
		}
		shotTimer++;


	}

	void OnParticleCollision (GameObject other)
	{
		levelTwo.DeadState ();
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