using UnityEngine;
using System.Collections;

public class LevelOneStateMachine : MonoBehaviour {
	
	private AIAction levelOne;
	private RaycastHit hit;
	private int restTimer;
	private int restTime;
	private int deadCount;
	private bool isTurn;
	private bool isCollisiontoRobot;

	//private Animator robotAnimation;

	void Awake() {
		levelOne = GetComponent<AIAction> ();
	}

	void Start () {
		restTime = 20;
		restTimer = 0;
		//robotAnimation = GetComponent<Animation> ();
	}

	void Update () {
		if(levelOne.isInDanger())
		{
			levelOne.StopState();
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
			levelOne.TurnState(true);
			levelOne.TurnState(true);
		}

		else if(isCollisiontoRobot&&restTimer<=restTime)
		{
			levelOne.StopState();
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
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
			gameObject.GetComponentInChildren<Animation>().Play("loop_idle");
			if(restTimer==restTime){
				levelOne.TurnState(false);
				restTimer = 0;
			}
			else restTimer++;
		}
		else
		{
			gameObject.GetComponentInChildren<Animation>().Play("loop_walk_funny");
			levelOne.WalkState();
		}

		if(deadCount==10)
		{
			levelOne.life--;
		}
		if(deadCount==20)
		{
			levelOne.life--;
		}
		if(deadCount==30)
		{
			levelOne.life--;
		}
		if(levelOne.life==0)
		{
			levelOne.DeadState();
		}
	}

	void OnParticleCollision (GameObject other)
	{
		deadCount++;
		//levelOne.DeadState ();
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

