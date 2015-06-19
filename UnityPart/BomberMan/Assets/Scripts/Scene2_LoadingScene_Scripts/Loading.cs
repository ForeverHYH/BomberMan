using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	private float width;
	private float height;
	// Use this for initialization
	void Start () {
		width = GetComponent<RectTransform>().rect.width;
		height = GetComponent<RectTransform>().rect.height;
		LoadGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGame() {
		StartCoroutine(StartLoading_1());
	}
	
	private IEnumerator StartLoading_1() {
		int displayProgress = 0;
		int toProgress = 0;
		AsyncOperation op = Application.LoadLevelAsync(2);
		//yield return op;
		// Metod 01
//		op.allowSceneActivation = false;
//		while(op.progress < 0.9f) {
//			toProgress = (int)op.progress * 100;
//			while(displayProgress < toProgress) {
//				++displayProgress;
//				SetLoadingPercentage(displayProgress);
//				yield return new WaitForEndOfFrame();
//			}
//		}
//		toProgress = 100;
//		while(displayProgress < toProgress){
//			++displayProgress;
//			SetLoadingPercentage(displayProgress);
//			yield return new WaitForEndOfFrame();
//		}
//		op.allowSceneActivation = true;
//		yield return op;

		// Method 02 and it ok
		while(!op.isDone) {            
			SetLoadingPercentage((int)op.progress * 100);
			yield return new WaitForEndOfFrame();
		}

		// Method 03 and it is ok!
//		op.allowSceneActivation = false;
//		while(op.progress < 0.9f) {
//			SetLoadingPercentage((int)op.progress * 100);
//			yield return new WaitForEndOfFrame();
//		}
//		SetLoadingPercentage(100);
//		yield return new WaitForEndOfFrame();
//		op.allowSceneActivation = true; 
	}
	
	void SetLoadingPercentage(int progress){
		//Debug.Log (progress);
		double processWidth = width*(progress/100.0);
		//Debug.Log (processWidth);
		GetComponent<RectTransform> ().sizeDelta = new Vector2 ((float)processWidth,height);
	}

}
