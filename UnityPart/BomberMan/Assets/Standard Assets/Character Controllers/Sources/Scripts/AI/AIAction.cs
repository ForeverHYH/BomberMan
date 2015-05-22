using UnityEngine;
using System.Collections;

public class AIAction : MonoBehaviour {

	public int speed;
	public int life;
	private int[] angles;
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
		Debug.Log ("I'm walking!");
		//This function is excute walk of robot, Contain danger area juge
		Vector3 speedVector = new Vector3 (0,0,speed);
		rigidbody.velocity = transform.rotation * speedVector;

		
	}
	
	public void TurnState()
	{
		Debug.Log ("I'm turning!");
		//This function is excute turn of robot, just turn pai/4

		transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y - angles[Random.Range (0, 3)] , 0f);
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
		//if(gameObject.transform.position) get danger area from floor cube
		return false;
	}
	
	public bool isNear()
	{
		//get player position
		return true;
	}
}
