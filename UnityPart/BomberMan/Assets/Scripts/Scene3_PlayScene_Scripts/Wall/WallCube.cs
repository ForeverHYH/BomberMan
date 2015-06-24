using UnityEngine;
using System.Collections;

public class WallCube : MonoBehaviour {

	private int timer = 0;
	private bool isDestory = false;
	private float rate;
	public GameObject AddCannon;
	public GameObject Shoose;
	public GameObject Glass;
	public GameObject Lighting;

	public float m_fDestruktionSpeed = 0.1f;
	public Material m_Mat;
	public float m_fTime;
	
	// Use this for initialization
	void Start () {
		rate = float.Parse(GameObject.Find ("First Person Controller").GetComponent<SenceLoad> ().m_toolRate);
		m_Mat = renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestory) 
		{
			timer++;
			m_fTime += Time.deltaTime * m_fDestruktionSpeed * 2;
			if (m_fTime >= 1.5f)
				m_fTime = 0;
			m_Mat.SetFloat("_Amount", m_fTime);
			if(timer==150) 
			{
				int range = Random.Range (0, 100);
				int intrate = (int)(rate * 100);
				if(range < intrate)
				{
					switch(Random.Range(0,4))
					{
					case 0:
						Instantiate(AddCannon, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
						break;
					case 1:
						Instantiate(Shoose, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
						break;
					case 2:
						Instantiate(Glass, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
						break;
					case 3:
						Instantiate(Lighting, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
						break;
					}
				}

				//Destroy(gameObject);
				gameObject.SetActive(false);
				timer = 0;
				isDestory = false;
			}
		}
	}

	void OnParticleCollision (GameObject other)
	{
		//Debug.Log (gameObject.name);
		isDestory = true;

	}
}
