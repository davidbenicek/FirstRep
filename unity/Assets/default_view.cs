using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Timers;
using System;

public class default_view : MonoBehaviour {
	public GameObject warning; 	
	DateTime nextInterupt; 
	bool interupted = false;
	// Use this for initialization
	void Start () {
		nextInterupt = DateTime.Now.AddMinutes(0.2);
		Debug.Log ("Set the timer");

	}
	// Update is called once per frame
	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)) { 
			SceneManager.LoadScene("home");
		}
		if (DateTime.Compare (DateTime.Now, nextInterupt) >= 0 && interupted == false) {
			interupted = true;
			Debug.Log ("Time");
			warning.SetActive (true);
		}
	}

	public void hideTimeWarning(){
		warning.SetActive(false);
	}
	public void goBackHome(){
		Debug.Log("Clicked the home button");
		SceneManager.LoadScene("home");
	}
	public void searchToggle(){
		Debug.Log("Search toggle");
		SceneManager.LoadScene("search");
	}
}
