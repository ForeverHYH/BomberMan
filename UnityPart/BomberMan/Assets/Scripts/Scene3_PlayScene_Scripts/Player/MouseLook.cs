using UnityEngine;
using System.Collections;
using Ovr;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {
	/// <summary>  
	/// 鼠标射线  
	/// </summary>  
	private Ray m_ray;
	/// <summary>  
	/// 射线碰撞的结构  
	/// </summary>  
	private RaycastHit m_rayhit;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;
	
	public Material changeMaterail;

	public float xrayDistance = 1.0f;
	private int explosiveSpeed = 1;
	public int maxCanonNumber = 1;
	private int fireDistance = 2;
	private ArrayList canonList = new ArrayList();
	public bool couldClicke;

	private Vector3 centerPoint = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);

	void Update ()
	{
		if(!StaticComponents.HASDEAD)
		{
			if (couldClicke) 
			{
				m_ray = Camera.main.ScreenPointToRay(centerPoint);
						
				if (Physics.Raycast(m_ray,out m_rayhit))
				{
					//打印射线碰撞到的对象的名称  
					if(m_rayhit.collider.transform.name.Equals("stupidRobot"))
					{
						GetComponent<GameEditor>().seeStupidRobotTime++;
					}
					if(m_rayhit.collider.transform.name.Equals("Canon"))
					{
						if(m_rayhit.collider.gameObject.GetComponent<FloorCube>().isMoving==0 && m_rayhit.collider.gameObject.GetComponent<FloorCube>().canMove)
						{
							float xDistance =(float)System.Math.Pow(m_rayhit.collider.gameObject.transform.position.x-gameObject.transform.position.x,2); //round is 4 down 6 up 5 to double
							float zDistance =(float)System.Math.Pow(m_rayhit.collider.gameObject.transform.position.z-gameObject.transform.position.z,2);
							int firePowDistance = (int)System.Math.Pow(fireDistance,2);
							if(xDistance+zDistance<=firePowDistance*2 && xDistance<=firePowDistance && zDistance<=firePowDistance && xDistance+zDistance>=0.5f)
							{
								m_rayhit.collider.gameObject.GetComponent<FloorCube>().ChangeMaterial();
								
								if(Input.GetMouseButtonUp (0)){
									//Debug.Log (canonList.Count);
									if(canonList.Count<maxCanonNumber)
									{
										canonList.Add(m_rayhit.collider.gameObject.transform); // add canon
										//Debug.Log("cannon ID is: " + tempCanonID);
										//Debug.Log("add success:"+canonList.Count);
										m_rayhit.collider.gameObject.GetComponent<FloorCube>().moving(xrayDistance,explosiveSpeed);
										GetComponent<GameEditor>().clickCannonTime ++;
									}
								}
							}
						}
					}
					
				}
			}
			
			
			if (axes == RotationAxes.MouseXAndY)
			{
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}

	}
	
	void Start ()
	{
		//Screen.showCursor = false;
		Screen.lockCursor = true;

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	public void getMessage(GameObject floorCube)
	{
		Debug.Log (canonList.Count);
		if (canonList.Contains (floorCube.transform)) {
			canonList.Remove(floorCube.transform);
			//Debug.Log("success"+canonList.Count);

		}
		else Debug.Log("unseccess");
	}
}