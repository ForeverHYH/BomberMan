using UnityEngine;
using System.Collections;

public class LevelOneStateMachine : MonoBehaviour {
	
	private AIAction levelOne;
	private RaycastHit hit;

	// Use this for initialization
	void Awake() {
		levelOne = GetComponent<AIAction> ();
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(transform.position,transform.forward ,out hit,0.5f)||levelOne.isInDanger())
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