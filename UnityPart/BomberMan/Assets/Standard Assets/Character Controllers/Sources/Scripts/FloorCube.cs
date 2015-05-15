using UnityEngine;
using System.Collections;
using System.Threading;

public class FloorCube : MonoBehaviour {

	public int isMoving = 0;
	private bool isShut = true;
	private int timer = 0;
	private bool isChangeMaterail = false;
	private int changeMaterailCount = 0;

	public Material changeMaterail;
	public Material originalMaterail;

	private float xrayDistance = 1.0f;
	private int explosiveSpeed = 1;   //explosive speed related to time; only take 1, 2, 5
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
		{
			child.gameObject.SetActive(false);
			child.gameObject.particleSystem.Stop();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (isMoving == 1 ) {
			renderer.material = originalMaterail;
			isChangeMaterail = false;
			int upTime =50/explosiveSpeed;
			if(timer==0) rigidbody.velocity = new Vector3(rigidbody.velocity.x,explosiveSpeed,rigidbody.velocity.z);
			else if(timer==upTime)
			{
				foreach(Transform child in transform)
				{
					child.gameObject.SetActive(true);
					child.gameObject.particleSystem.startLifetime = xrayDistance;      //xrayDistance related to start life time
					child.gameObject.particleSystem.Play();
				}
				rigidbody.velocity = new Vector3(0,0,0);

			}
			else if(timer==upTime+100) 
			{
				foreach(Transform child in transform)
				{
					child.gameObject.SetActive(false);
					child.gameObject.particleSystem.Stop();
				}
				rigidbody.velocity = new Vector3(0,-explosiveSpeed,0);
			}
			else if(timer==2*upTime +100) 
			{
				rigidbody.velocity = new Vector3(0,0,0);
				isMoving = 0;
				SendMessagetoCharactor();
			}
			timer++; 
			if(isMoving == 0){
				timer=0;
			}
		}
		if(isChangeMaterail)
		{
			changeMaterailCount++;
			if(changeMaterailCount==10) 
			{
				renderer.material = originalMaterail;
				changeMaterailCount=0;
			}
		}
	}

	public void moving(float xray, int explosive){
//		if (Physics.Raycast (transform.position, Vector3.up, out hit, 1)) 
//		{
//			isMoving = 0;
//			Debug.Log("NOT MOVING!");
//		} 
		//else 
		//{
			Debug.Log("MOVING!");
			isMoving = 1;
			xrayDistance = xray;
			explosiveSpeed = explosive;
		//}

	}

	public void ChangeMaterial(){
		renderer.material = changeMaterail;
		isChangeMaterail = true;
	}

	void SendMessagetoCharactor(){
		GameObject charactor = GameObject.Find ("First Person Controller");
		if (charactor) {
						charactor.SendMessage ("getMessage", gameObject);
				} else {
						Debug.Log ("cannot find");
				}

	}
}
