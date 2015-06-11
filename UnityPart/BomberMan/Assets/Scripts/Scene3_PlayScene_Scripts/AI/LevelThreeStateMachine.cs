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
	private int hitTimer;
	private int hitTime;
	private int catchTimer;
	private int catchTime;
	// Use this for initialization
	void Start () {
		restTime = 20;
		restTimer = 0;
		hitTime = 10;
		hitTimer = 0;
		catchTimer = 0;
		catchTime = 20;
	}
	void Awake() {
		levelThree = GetComponent<AIAction> ();
	}
	// Update is called once per frame
	void Update () {
		if(levelThree.isInDanger())
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
			levelThree.StopState();
			levelThree.TurnState(true);
			levelThree.TurnState(true);
		}
		else if(isCollisiontoRobot&&restTimer<=restTime&&!hasTurn)
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
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
		else if(levelThree.isNear()&&!levelThree.isInDanger() && levelThree.isInCenter())
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
			levelThree.StopState();
			if(catchTimer==catchTime){
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
			}
			else if(catchTimer>=catchTime+5){
				gameObject.GetComponentInChildren<Animation>().Play("loop_walk_funny");
				levelThree.WalkState();
			}
			catchTimer++;
		}
		else if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,0.55f)&&hit.collider.transform.name.Equals("wall")&& levelThree.isNear())
		{
			levelThree.StopState();
			gameObject.GetComponentInChildren<Animation>().Play("punch_hi_right");
			if(hitTimer>=hitTime)
			{
				hit.collider.gameObject.SetActive(false);
				hitTimer = 0;
			}
			hitTimer++;

		}
		else if(Physics.Raycast(transform.position,transform.forward ,out hit,0.55f)&&restTimer<=restTime)
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
			levelThree.StopState();
			if(restTimer==restTime){
				levelThree.TurnState(false);
				restTimer = 0;
			}
			else restTimer++;
		}
		else
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_walk_funny");
			levelThree.WalkState();
			catchTimer = 0;
			hasTurn = false;
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
