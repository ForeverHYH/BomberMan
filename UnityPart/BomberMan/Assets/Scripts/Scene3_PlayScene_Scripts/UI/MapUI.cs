using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapUI : MonoBehaviour {
	public int timer;
	public bool isStartCounting;
	public Sprite mapOpenTexture; 
	public Sprite mapCloseTexture;
	public GameObject TimeCounter;
	public GameObject MapCamera;
	// Use this for initialization
	void Start () {
		timer = 0;
		isStartCounting = false;
		GetComponent<Image> ().sprite = mapCloseTexture;
		MapCamera.SetActive(false);
		TimeCounter.SetActive(false);
		InvokeRepeating("TimeReduce", 1,1);
	}
	
	// Update is called once per frame
	void Update () {
		if(isStartCounting)
		{
			TimeCounter.GetComponent<Text>().text = timer.ToString();
			if(timer==0)
			{
				GetComponent<Image> ().sprite = mapCloseTexture;
				TimeCounter.SetActive(false);
				MapCamera.SetActive(false);
				isStartCounting = false;
			}
		}
	}

	void TimeReduce()
	{
		if(timer>0) timer--;
	}

	public void startCounting()
	{
		GetComponent<Image> ().sprite = mapOpenTexture;
		isStartCounting = true;
		timer = timer + 10;
		TimeCounter.SetActive(true);
		MapCamera.SetActive(true);

	}
}
