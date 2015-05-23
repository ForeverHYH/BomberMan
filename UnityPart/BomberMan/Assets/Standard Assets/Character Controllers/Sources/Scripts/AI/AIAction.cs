using UnityEngine;
using System.Collections;

public class AIAction : MonoBehaviour {

	public int speed;
	public int life;
	private int[] angles;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
		transform.Rotate (0,0,0);
		angles = new int[]{-90,90,-90,90};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void WalkState()
	{
		//Debug.Log ("I'm walking!");
		//This function is excute walk of robot, Contain danger area juge
		Vector3 speedVector = new Vector3 (0,0,speed);
		rigidbody.velocity = transform.rotation * speedVector;

		
	}

	public void StopState()
	{
		//Debug.Log ("Stop!");
		//This function is excute walk of robot, Contain danger area juge
		Vector3 speedVector = new Vector3 (0,0,0);
		rigidbody.velocity = transform.rotation * speedVector;
	}

	public void TurnState(bool isright)
	{
		//Debug.Log ("I'm turning!");
		//This function is excute turn of robot, just turn pai/4
		if(isright) transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + 90 , 0f);
		else transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y - angles[Random.Range (0, 3)] , 0f);
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
	
	public void DeadState()
	{
		Vector3 speedVector = new Vector3 (0,0,0);
		rigidbody.velocity = transform.rotation * speedVector;
		gameObject.SetActive (false);
		//play dead animation.
	}
	
	public bool isDead()
	{

		return false;
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
		if (Physics.Raycast(transform.position,transform.forward ,out hit,2f)) 
		{
			if(hit.transform.gameObject.name.Equals("Canon")) return true;
			else return false;
		} 
		else 
		{
			return false;
		}
	}
	
	public bool isNear()
	{
		//get player position
		return true;
	}
}
