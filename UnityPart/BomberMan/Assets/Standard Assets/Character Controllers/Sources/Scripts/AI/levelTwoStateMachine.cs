using UnityEngine;
using System.Collections;

public class LevelTwoStateMachine : MonoBehaviour {
	
	private AIAction levelTwo;
	private RaycastHit hit;
	private int canonID = -1;

	private int restTimer;
	private int restTime;

	void Awake() {
		levelTwo = GetComponent<AIAction> ();
	}

	// Use this for initialization
	void Start () {
		restTime = 20;
		restTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(levelTwo.isInDanger())
		{
			Debug.Log("danger!");
			levelTwo.StopState();
			levelTwo.TurnState(true);
			levelTwo.TurnState(true);
		}

		else if(Physics.Raycast(transform.position,transform.forward ,out hit,0.65f)&&restTimer<=restTime)
		{
			levelTwo.StopState();
			if(restTimer==restTime){
				levelTwo.TurnState(false);
				restTimer = 0;
			}
			else restTimer++;
		}
		else if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,2)&&hit.collider.transform.name.Equals("wall")&&levelTwo.Canshot())
		{
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
				Debug.Log("robot x is:"+ robotX + "target z is:"+ targetZ);
			}
			else
			{
				targetZ = canonZ;
				targetX = (int)((canonX+robotX+1)/2);
			}
			//levelTwo.StopState();
			levelTwo.ShotState(targetX,targetZ);
		}
		else if(levelTwo.isDead())
		{
			levelTwo.DeadState();
		}
		else
		{
			levelTwo.WalkState();
		}

	}

	void addCanonID()
	{

	}

	void OnParticleCollision (GameObject other)
	{
		Debug.Log ("Dead!");
		levelTwo.DeadState ();
	}
}