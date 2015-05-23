using UnityEngine;
using System.Collections;

public class LevelTwoStateMachine : MonoBehaviour {
	
	private AIAction levelTwo = new AIAction ();
	private RaycastHit hit;
	private int canonID = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,1)&&hit.collider.transform.name.Equals("wall")&& canonID == -1)
		{
			levelTwo.ShotState();
		}
		else if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,1)&&hit.collider.transform.name.Equals("steel")||levelTwo.isInDanger())
		{
			levelTwo.TurnState(false);
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
}