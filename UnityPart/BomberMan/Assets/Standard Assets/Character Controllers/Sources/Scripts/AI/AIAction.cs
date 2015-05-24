using UnityEngine;
using System.Collections;

public class AIAction : MonoBehaviour {

	public int speed;
	public int life;
	private int[] angles;
	private RaycastHit hit;
	private ArrayList canonList = new ArrayList();
	private int maxCanonNumber;
	// Use this for initialization
	void Start () {
		transform.Rotate (0,0,0);
		angles = new int[]{-90,90,-90,90};
		maxCanonNumber = 1;
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
	
	public void ShotState(int x,int z)
	{
		//This function is excute shot of robot, get back one cube when shot
		object[] gameObjects = GameObject.FindSceneObjectsOfType(typeof(Transform)) as object[];
		foreach(Transform tempObjects in gameObjects)
		{
			if(tempObjects.transform!=null)
			{
				if(tempObjects.name=="Canon")
				{
					if((int)tempObjects.transform.position.x==x && (int)tempObjects.transform.position.z==z)
					{
						tempObjects.GetComponent<FloorCube>().ChangeMaterial();
						//Debug.Log("x is:" + x + "z is:" + z);
						if(tempObjects.GetComponent<FloorCube>().isMoving==0&&tempObjects.GetComponent<FloorCube>().canMove)
						{
							if(canonList.Count<maxCanonNumber)
							{
								int tempCanonID = (int)(tempObjects.transform.position.x*100 + tempObjects.transform.position.z);
								canonList.Add(tempCanonID); // add canon
								//Debug.Log("cannon ID is: " + tempCanonID);
								tempObjects.GetComponent<FloorCube>().moving(1.0f,1);
							}
						}
					}
				}
			}
		}
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

	void getMessage(GameObject floorCube)
	{
		//Debug.Log ("position is" + floorCube.transform.position.x + "and" + floorCube.transform.position.z);
		int currentCanonID = (int)(floorCube.transform.position.x * 100 + floorCube.transform.position.z);
		if (canonList.Contains (currentCanonID)) {
			canonList.Remove(currentCanonID);
			
		}
	}
	
	public bool Canshot()
	{
		if (canonList.Count == 0)
						return true;
				else
						return false;
	}
}
