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

	private int deadCount;
	private bool isDead;

	void Awake() {
		levelTwo = GetComponent<AIAction> ();
	}

	void Start () {
		isDead = false;
		restTime = 20;
		restTimer = 0;
		shotTime = 50;
		shotTimer = 0;
	}

	void Update () {
		if(levelTwo.isInDanger())
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
			levelTwo.StopState();
			levelTwo.TurnState(true);
			levelTwo.TurnState(true);
		}

		else if(isCollisiontoRobot&&restTimer<=restTime&&!hasTurn)
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
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
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
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
				gameObject.GetComponentInChildren<Animation>().Play("punch_hi_left");
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
			gameObject.GetComponentInChildren<Animation>().Play("loop_walk_funny");
			levelTwo.WalkState();
			hasTurn = false;
		}
		shotTimer++;


		if(deadCount==10)
		{
			levelTwo.life--;
		}
		if(deadCount==20)
		{
			levelTwo.life--;
		}
		if(deadCount==30)
		{
			levelTwo.life--;
		}
		if(levelTwo.life==0&&!isDead)
		{
			isDead = true;
			GetComponent<AIAudioController>().RobotDeadAudioSource.Play();
		}
		if(!GetComponent<AIAudioController>().RobotDeadAudioSource.isPlaying && isDead)
		{
			levelTwo.DeadState();
		}
	}

	void OnParticleCollision (GameObject other)
	{
		deadCount++;
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