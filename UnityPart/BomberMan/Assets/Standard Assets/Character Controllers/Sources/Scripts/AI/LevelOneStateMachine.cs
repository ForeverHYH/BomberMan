using UnityEngine;
using System.Collections;

public class LevelOneStateMachine : MonoBehaviour {
	
	private Action levelOne = new Action ();
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,1)||levelOne.isInDanger())
		{
			//if there is something before it turn
			levelOne.TurnState();
		}
		else if(levelOne.isDead())
		{
			levelOne.DeadState();
		}
		else
		{
			//walk
			levelOne.WalkState();
		}
	}
	
	
}