using UnityEngine;
using System.Collections;

public class Behaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WalkState()
	{
		//This function is excute walk of robot, Contain danger area juge
	}

	public void TurnState()
	{
		//This function is excute turn of robot, just turn pai/4
	}

	public void ShotState()
	{
		//This function is excute shot of robot, get back one cube when shot
	}

	public void CatchState()
	{
		//This function is excute catch of robot, when robot 'feel' 
		//which means the distance is at least 3 cube, they will "Find Way" and catch player
	}

	public void Move()
	{
		//move might be use in walkstate and catch state
	}

	public void PathFind()
	{
		//find path, maybe return next point or some thing else
	}

	public bool isInDanger ()
	{
		//if(gameObject.transform.position) get danger area from floor cube
		return true;
	}

	public bool isNear()
	{
		//get player position
		return true;
	}
}
