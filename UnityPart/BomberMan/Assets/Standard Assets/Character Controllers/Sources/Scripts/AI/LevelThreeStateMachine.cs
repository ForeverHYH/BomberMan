using UnityEngine;
using System.Collections;

public class LevelThreeStateMachine : MonoBehaviour {

	private AIAction levelThree = new AIAction ();
	private RaycastHit hit;
	private int canonID = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(levelThree.isNear()&&!levelThree.isInDanger())
		{
			levelThree.CatchState();
		}
		if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,1)&&hit.collider.transform.name.Equals("wall")&& canonID == -1)
		{
			levelThree.ShotState();
		}
		else if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,1)&&hit.collider.transform.name.Equals("steel")||levelThree.isInDanger())
		{
			levelThree.TurnState(false);
		}
		else if(levelThree.isDead())
		{
			levelThree.DeadState();
		}
		else
		{
			levelThree.WalkState();
		}
		
	}
	
	void addCanonID()
	{
		
	}
}
