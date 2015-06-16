using UnityEngine;
using System.Collections;

public class AIAudioController : MonoBehaviour {

	public AudioSource RobotWalkingAudioSource;
	public AudioSource RobotDeadAudioSource;

	private bool isWalking;
	// Use this for initialization
	void Start () {
		isWalking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.rigidbody.velocity.z!=0&&!isWalking)
		{

			isWalking = true;
			RobotWalkingAudioSource.pitch =System.Math.Abs(transform.rigidbody.velocity.z)/2.0f;  //the pitch is depend on velocity
			RobotWalkingAudioSource.Play();
		}
		if(transform.rigidbody.velocity.z==0)
		{
			isWalking = false;
			RobotWalkingAudioSource.Stop();
		}

	}
}
