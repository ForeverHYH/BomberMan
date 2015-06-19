using UnityEngine;
using System.Collections;

public class AIAction : MonoBehaviour {

	public int speed;
	public int life;
	
	private int[] angles;
	private RaycastHit hit;
	private ArrayList robotCanonList = new ArrayList();
	private int maxCanonNumber;
	private int[,] floorGrid = new int[15, 15];
	private ArrayList pathIDList = new ArrayList();
	// Use this for initialization
	void Start () {
		transform.Rotate (0,0,0);
		angles = new int[]{-90,90,-90,90};
		maxCanonNumber = 1;
		initFloorGrid ();

	}

	void initFloorGrid(){
		System.Array.Clear (floorGrid,0,floorGrid.Length);
		GameObject charactor = GameObject.Find ("First Person Controller");
		foreach(Transform j in charactor.GetComponent<SenceLoad>().HinderList)
		{
			floorGrid[(int)j.position.x,(int)j.position.z] = -1;
		}
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
		object[] gameObjects = GameObject.FindObjectsOfType(typeof(Transform)) as object[];
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
							tempObjects.GetComponent<FloorCube>().moving(1.0f,1);
						}
					}
				}
			}
		}
	}


	public int CatchState()
	{
		//This function is excute catch of robot, when robot 'feel' 
		//which means the distance is at least 3 cube, they will "Find Way" and catch player
		initFloorGrid ();
		GameObject charactor = GameObject.Find ("First Person Controller");
		PathFind (charactor);
		if(pathIDList.Count>=4)
		{
			int positionX = (int)pathIDList[pathIDList.Count-4];
			int positionY = (int)pathIDList[pathIDList.Count-3];
			Transform tempCanon = null;
			
			object[] gameObjects = GameObject.FindObjectsOfType(typeof(Transform)) as object[];
			foreach(Transform tempObjects in gameObjects)
			{
				if(tempObjects!=null)
				{
					if(tempObjects.name=="Canon")
					{
						if((int)tempObjects.transform.position.x==positionX && (int)tempObjects.transform.position.z==positionY)
						{
							tempCanon = tempObjects;
							tempObjects.GetComponent<FloorCube>().ChangeMaterial();
							return direction(transform,tempCanon);
						}
					}
				}
			}

		}
		else if(pathIDList.Count==2)
		{
			int positionX = (int)System.Math.Round(charactor.transform.position.x);
			int positionY = (int)System.Math.Round(charactor.transform.position.z);
			Transform tempCanon = null;
			
			object[] gameObjects = GameObject.FindObjectsOfType(typeof(Transform)) as object[];
			foreach(Transform tempObjects in gameObjects)
			{
				if(tempObjects!=null)
				{
					if(tempObjects.name=="Canon")
					{
						if((int)tempObjects.transform.position.x==positionX && (int)tempObjects.transform.position.z==positionY)
						{
							tempCanon = tempObjects;
							tempObjects.GetComponent<FloorCube>().ChangeMaterial();
							return direction(transform,tempCanon);
						}
					}
				}
			}
		}
		return -1;

	}

	int direction(Transform robotTrans, Transform cannonTrans)
	{
		int robotX = (int)System.Math.Round (robotTrans.position.x);
		int robotZ = (int)System.Math.Round (robotTrans.position.z);
		int cannonX = (int)System.Math.Round (cannonTrans.position.x);
		int cannonZ = (int)System.Math.Round (cannonTrans.position.z);
		if(robotX == cannonX)
		{
			if(robotZ == cannonZ+1)
			{
				switch((int)robotTrans.localEulerAngles.y)
				{
				case 0:
					return 2;
				case 90:
					return 1;
				case 180:
					return 0;
				case 270:
					return 3;
				case 360:
					return 2;
				case -90:
					return 3;
				}
			}
			else if(robotZ == cannonZ-1)
			{
				switch((int)robotTrans.localEulerAngles.y)
				{
				case 0:
					return 0;
				case 90:
					return 3;
				case 180:
					return 2;
				case 270:
					return 1;
				case 360:
					return 0;
				case -90:
					return 1;
				}
			}
		}
		else if(robotZ == cannonZ)
		{
			if(robotX == cannonX+1)
			{
				switch((int)robotTrans.localEulerAngles.y)
				{
				case 0:
					return 3;
				case 90:
					return 2;
				case 180:
					return 1;
				case 270:
					return 0;
				case 360:
					return 3;
				case -90:
					return 0;
				}
			}
			else if(robotX == cannonX-1)
			{
				switch((int)robotTrans.localEulerAngles.y)
				{
				case 0:
					return 1;
				case 90:
					return 0;
				case 180:
					return 3;
				case 270:
					return 2;
				case 360:
					return 1;
				case -90:
					return 2;
				}
			}
		}
		return 4;
	}

	public void DeadState()
	{
		Vector3 speedVector = new Vector3 (0,0,0);
		rigidbody.velocity = transform.rotation * speedVector;
		gameObject.SetActive (false);

		GameObject charactor = GameObject.Find ("First Person Controller");
		charactor.GetComponent<SenceLoad> ().CreatureList.Remove (gameObject.transform);
		//play dead animation.
	}
	
	public void PathFind(GameObject goalObject)
	{
		pathIDList.Clear ();
		//find path, maybe return next point or some thing else
		int transX = System.Convert.ToInt32(transform.position.x);
		int transZ = System.Convert.ToInt32(transform.position.z);
		int goalX = System.Convert.ToInt32(goalObject.transform.position.x);
		int goalZ = System.Convert.ToInt32(goalObject.transform.position.z);
		FloodFill (transX,transZ,goalX,goalZ,1);
		FloodPath (goalX, goalZ, transX, transZ);
		Debug.Log ("list count is:"+pathIDList.Count);
	}

	void FloodFill(int x, int y,int goalX, int goalY, int tempTag)
	{
		if(x==goalX&&y==goalY)
		{
			if(floorGrid[x,y]<tempTag && floorGrid[x,y]!=0){}
			else floorGrid[x,y] = tempTag;
		}
		else if(x>=0 && x<15 && y>=0 && y<15 && floorGrid[x,y]!=-1 && tempTag<=8 )
		{
			if(floorGrid[x,y]<tempTag && floorGrid[x,y]!=0){}
			else floorGrid[x,y] = tempTag;
			FloodFill(x+1,y,  goalX,goalY,tempTag+1);
			FloodFill(x-1,y,  goalX,goalY,tempTag+1);
			FloodFill(x,  y+1,goalX,goalY,tempTag+1);
			FloodFill(x,  y-1,goalX,goalY,tempTag+1);
		}
	}

	void FloodPath(int x, int y,int orignalX, int orignalY)
	{
		if(x<14&&floorGrid[x+1,y] == floorGrid[x,y]-1)
		{
			pathIDList.Add(x+1);
			pathIDList.Add(y);
			FloodPath(x+1,y,orignalX,orignalY);
		}
		else if(x>0&&floorGrid[x-1,y] == floorGrid[x,y]-1)
		{
			pathIDList.Add(x-1);
			pathIDList.Add(y);
			FloodPath(x-1,y,orignalX,orignalY);
		}
		else if(y<14&&floorGrid[x,y+1] == floorGrid[x,y]-1)
		{
			pathIDList.Add(x);
			pathIDList.Add(y+1);
			FloodPath(x,y+1,orignalX,orignalY);
		}
		else if(y>0&&floorGrid[x,y-1] == floorGrid[x,y]-1)
		{
			pathIDList.Add(x);
			pathIDList.Add(y-1);
			FloodPath(x,y-1,orignalX,orignalY);
		}
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
		GameObject charactor = GameObject.Find ("First Person Controller");
		int transX = System.Convert.ToInt32(transform.position.x);
		int transZ = System.Convert.ToInt32(transform.position.z);
		int goalX = System.Convert.ToInt32(charactor.transform.position.x);
		int goalZ = System.Convert.ToInt32(charactor.transform.position.z);

		int xPowDistance = (int)System.Math.Pow (transX-goalX,2);
		int zPowDistance = (int)System.Math.Pow (transZ-goalZ,2);

		if(xPowDistance+zPowDistance<=28)
		{
			Debug.Log("Near!");
			return true;
		}
		else return false;
	}

	public bool isInCenter()
	{
		float xPart = System.Math.Abs ((float)System.Math.Round(gameObject.transform.position.x) - gameObject.transform.position.x);
		float zPart = System.Math.Abs ((float)System.Math.Round(gameObject.transform.position.z) - gameObject.transform.position.z);
		//Debug.Log ("xPart is:"+xPart+"zPart is:"+zPart);
		if(xPart<0.1&&zPart<0.1)
		{
			Debug.Log("CENTER!!!");
			return true;
		}
		else return false;
	}

}
