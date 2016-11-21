using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class home_screen : MonoBehaviour {
	public GameObject loading;
	public void LoadScene(string name){
			Debug.Log("Switch to "+name);
			
			loading.SetActive (true);
			SceneManager.LoadScene(name);
	}
	public void Start(){

	}
	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)) { 
			Application.Quit(); 
		}
	}


	public void QuitApp(){
		Application.Quit();
	}

}
