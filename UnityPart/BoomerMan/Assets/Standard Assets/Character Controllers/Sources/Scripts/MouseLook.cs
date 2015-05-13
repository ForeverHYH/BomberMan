using UnityEngine;
using System.Collections;

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

	private float xrayDistance = 2.0f;
	private int explosiveSpeed = 1;
	private int maxCanonNumber = 3;
	private int fireDistance = 2;
	private ArrayList canonList = new ArrayList();

	void Update ()
	{
		//Input.GetMouseButtonDown (0)
		if (true) 
		{
			m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(m_ray,out m_rayhit))
			{
				//打印射线碰撞到的对象的名称  
				//Debug.Log("I'm looking at"+ m_rayhit.collider.transform.name);
				//m_rayhit.collider.transform.renderer.material.shader = Shader.Find (" Glossy");
				if(m_rayhit.collider.transform.name.Equals("Canon")&&m_rayhit.collider.gameObject.GetComponent<FloorCube>().isMoving==0)
				{
					int xDistance = (int)System.Math.Pow(m_rayhit.collider.gameObject.transform.position.x-(int)System.Math.Round(gameObject.transform.position.x),2); //round is 4 down 6 up 5 to double
					int zDistance = (int)System.Math.Pow(m_rayhit.collider.gameObject.transform.position.z-(int)System.Math.Round(gameObject.transform.position.z),2);
					int firePowDistance = (int)System.Math.Pow(fireDistance,2);
					if(xDistance+zDistance<=firePowDistance*2 && xDistance<=firePowDistance && zDistance<=firePowDistance && xDistance+zDistance>=1)
					{
						m_rayhit.collider.gameObject.GetComponent<FloorCube>().ChangeMaterial();

						if(Input.GetMouseButtonDown (0)){
							if(canonList.Count<maxCanonNumber)
							{
								int tempCanonID = (int)(m_rayhit.collider.gameObject.transform.position.x*100 + m_rayhit.collider.gameObject.transform.position.z);
								canonList.Add(tempCanonID); // add canon
								Debug.Log("cannon ID is: " + tempCanonID);
								m_rayhit.collider.gameObject.GetComponent<FloorCube>().moving(xrayDistance,explosiveSpeed);
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
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		canonList = new ArrayList();
	}

//	void OnTriggerExit( Collider other )
//	{
//		Debug.Log ("hahahaha");
//		Debug.Log("collider thing is :" + other.gameObject.name);
//	}

	void getMessage(GameObject floorCube)
	{
		Debug.Log ("position is" + floorCube.transform.position.x + "and" + floorCube.transform.position.z);
		int currentCanonID = (int)(floorCube.transform.position.x * 100 + floorCube.transform.position.z);
		if (canonList.Contains (currentCanonID)) {
			canonList.Remove(currentCanonID);

		}
	}
}