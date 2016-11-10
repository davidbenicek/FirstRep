using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class default_view : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)) { 
			SceneManager.LoadScene("home");
		}
	}

	public void goBackHome(){
		Debug.Log("Clicked the home button");
		SceneManager.LoadScene("home");
	}
	public void searchToggle(){
		Debug.Log("Search toggle");
	}
}
