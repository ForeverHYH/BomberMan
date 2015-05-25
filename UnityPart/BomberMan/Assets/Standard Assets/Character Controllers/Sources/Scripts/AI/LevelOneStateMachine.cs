using UnityEngine;
using System.Collections;

public class LevelOneStateMachine : MonoBehaviour {
	
	private AIAction levelOne;
	private RaycastHit hit;
	private int restTimer;
	private int restTime;

	private int slideTimer;
	private int slideTime;
	private bool isTurn;
	// Use this for initialization
	void Awake() {
		levelOne = GetComponent<AIAction> ();
	}

	void Start () {
		restTime = 20;
		restTimer = 0;
		slideTimer = 0;
		slideTime = 20;
	}
	
	// Update is called once per frame
	void Update () {
		if(levelOne.isInDanger())
		{
			//Debug.Log("danger!");
			levelOne.StopState();
			levelOne.TurnState(true);
			levelOne.TurnState(true);
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

		// turn in intersection still have smoe problems
//		else if(!Physics.Raycast(transform.position,transform.forward ,out hit,0.6f)&&!Physics.Raycast(transform.position,transform.right ,out hit,0.6f))
//		{
//			if(slideTimer<slideTime){
//				slideTimer++;
//				levelOne.WalkState();
//			}
//			else if(slideTimer==slideTime){
//				levelOne.StopState();
//				levelOne.TurnState(true);
//				slideTimer++;
//			}
//			else if(slideTimer<2*slideTime&&slideTimer>slideTime)
//			{
//				slideTimer++;
//				levelOne.WalkState();
//			}
//			else if(slideTimer==2*slideTime){
//				slideTimer=0;
//			}
//		}

		else if(levelOne.isDead())
		{
			levelOne.DeadState();
		}
		else
		{
			levelOne.WalkState();
		}
	}

	void OnParticleCollision (GameObject other)
	{
		//Debug.Log ("Dead!");
		levelOne.DeadState ();
	}

}

