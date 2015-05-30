using UnityEngine;
using System.Collections;

public class LevelThreeStateMachine : MonoBehaviour {

	private AIAction levelThree;
	private RaycastHit hit;
	private int canonID = -1;
	private bool isCollisiontoRobot;
	private bool hasTurn;
	private int restTimer;
	private int restTime;
	private int nearTimer;
	private int nearTime;
	private bool catchSleep;
	// Use this for initialization
	void Start () {
		catchSleep = false;
		restTime = 20;
		restTimer = 0;
		nearTimer = 0;
		nearTime = 100;
	}
	void Awake() {
		levelThree = GetComponent<AIAction> ();
	}
	// Update is called once per frame
	void Update () {
		if(levelThree.isInDanger())
		{
			levelThree.StopState();
			levelThree.TurnState(true);
			levelThree.TurnState(true);
		}
		else if(isCollisiontoRobot&&restTimer<=restTime&&!hasTurn)
		{
			levelThree.StopState();
			if(restTimer==restTime){
				levelThree.TurnState(true);
				levelThree.TurnState(true);
				hasTurn=true;
				isCollisiontoRobot = false;
				restTimer = 0;
			}
			else restTimer++;
		}
		else if(levelThree.isNear()&&!levelThree.isInDanger()&&restTimer<=restTime && !catchSleep)
		{
			catchSleep = true;
			levelThree.StopState();
			if(restTimer==restTime){
				switch(levelThree.CatchState())
				{
					case -1:
						Debug.Log("Path List Error");
						break;
					case 0: 
						Debug.Log("stright");
						break;
					case 1:
						Debug.Log("1");
						levelThree.TurnState(true);
						break;
					case 2:
						Debug.Log("2");
						levelThree.TurnState(true);	
						levelThree.TurnState(true);
						break;
					case 3:
						Debug.Log("3");
						levelThree.TurnState(true);	
						levelThree.TurnState(true);
						levelThree.TurnState(true);
						break;
					case 4:
						Debug.Log("position error");
						break;
					default:
						Debug.Log("error"); 
						break;
				}
				restTimer = 0;
			}
			else restTimer++;
		}
		else if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,0.55f)&&hit.collider.transform.name.Equals("wall")&& levelThree.isNear())
		{
			hit.collider.gameObject.SetActive(false);
		}
		else if(Physics.Raycast(transform.position,transform.forward ,out hit,0.55f)&&restTimer<=restTime)
		{
			levelThree.StopState();
			if(restTimer==restTime){
				levelThree.TurnState(false);
				restTimer = 0;
			}
			else restTimer++;
		}
		else
		{
			levelThree.WalkState();
			hasTurn = false;
		}

		nearTimer++;
		if(nearTimer>nearTime)
		{
			nearTimer=0;
			catchSleep = false;
		}
	}

	void OnParticleCollision (GameObject other)
	{
		levelThree.DeadState ();
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
