using UnityEngine;
using System.Collections;

public class LevelOneStateMachine : MonoBehaviour {

	private Behaviour levelOne = new Behaviour ();
	private RaycastHit hit;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward ,out hit,1)||isInDanger())
		{
			//if there is something before it turn
			levelOne.WalkState();
		}
		else
		{
			//walk
			levelOne.TurnState();
		}
	}

	bool isInDanger ()
	{
		//if(gameObject.transform.position) get danger area from floor cube
		return true;
	}
}
