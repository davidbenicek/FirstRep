using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class searchMachine : MonoBehaviour {
	public GameObject list;
	public GameObject bro;
	public GameObject hideBroButton;
	public GameObject scrollView;
	public GameObject panel;
	public GameObject defaultButton;

	private Dictionary<string, Exercise []> stations = new Dictionary<string, Exercise []>();
	private int[] off = { 0, -85, -145 };

	// Use this for initialization
	void Start () {
		stations.Add("squatRack",new Exercise[] {new Exercise("Highbar Squat",10, new String [] {"Quads","Lower Back"}), new Exercise("Overhead Squat",11, new String [] {"Quads","Deltoids","Lower Back"}), new Exercise("Piston Squat",12,new String [] {"Quads","Lower Back"}), new Exercise("Front Squat",13, new String [] {"Quads","Abs"})});
		stations.Add("curlStation",new Exercise []{new Exercise("Bicep Curl",20,new String []  {"Biceps"}),new Exercise("Shoulder press",21, new String [] {"Deltoids"})});
		stations.Add("deadLift",new Exercise []{new Exercise("Bentover Row",30, new String []  {"Biceps","Upper Back", "Lower Back"})});
		drawMachines ();
	}
	void drawMachines(){
		int x = 0;
		//For each station
		foreach (KeyValuePair<String,Exercise []> entry in stations) {
			//Copy the big category block
			GameObject category = Instantiate (defaultButton);
			//Move to same position as parent
			category.transform.position = defaultButton.transform.position;
			//Set parent object
			category.transform.parent = list.gameObject.transform;
			//Move it down by a predifined amount
			category.transform.localPosition += new Vector3 (0, off[x], 0);
			category.transform.localScale = defaultButton.transform.localScale;
			//Set the parent as the list
			category.transform.parent = list.gameObject.transform;
			//Set the heading as the key
			category.GetComponentsInChildren<Text>()[0].text = Regex.Replace(entry.Key, "(\\B[A-Z])", " $1");
			//Draw the exercises relating to this station
			drawExercisesForStation (entry.Key,++x);
		}
		defaultButton.SetActive(false);
	}
	void drawExercisesForStation(String s, int x){
		int i;
		Debug.Log (s+x);

		//For each exercise in the station
		for (i = 0;i<stations[s].Length;i++) {
			//Copy the initial text of the new block
			Debug.Log ("BLAAAAH "+GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1].GetComponentsInChildren<Text>()[0].text);
			RawImage created = (RawImage)Instantiate (GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1]);
			//Position in first position
			created.transform.position = GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1].transform.position;
			//Set the overall block as the parent
			created.transform.parent = GameObject.FindGameObjectsWithTag("Category")[x].transform;
			created.transform.localScale = GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1].transform.localScale;
			//Set the element down by 200 each time
			created.transform.localPosition += new Vector3(0,-200*i,0);
			//Set the name of this exercise
			String exerciseName = stations [s] [i].getName ();
			Debug.Log(s +" yo "+exerciseName);
			created.gameObject.transform.GetComponentsInChildren<Text>()[0].text = exerciseName;
			//Assign listener to show bro on lcik
			created.gameObject.transform.GetComponentsInChildren<Button>()[0].onClick.AddListener (() => showBro (s, exerciseName));	
		}
		GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1].gameObject.SetActive (false);
		//list.gameObject.GetComponentsInChildren<RawImage>()[0].GetComponentsInChildren<Text>()[1].text = "yo1";
 	}
	void showBro(int anim){
		Debug.Log ("Set animation"+anim);
		bro.SetActive (true);
		panel.SetActive (false);
		scrollView.SetActive (false);
		hideBroButton.SetActive (true);
		bro.gameObject.GetComponent<Animator> ().SetInteger ("state", anim);
		bro.transform.localPosition = new Vector3(0, -500, 377);
	}
	void showBro(String machine,String exercise)
	{
		Debug.Log (exercise + "  " + machine);

		int an = getAnimationCode (machine, exercise);
		Debug.Log (an);
		showBro (an);
	}

	public void hideBro(){
		hideBroButton.SetActive (false);
		bro.SetActive (false);
		panel.SetActive (true);
		scrollView.SetActive (true);
	}

	private int getAnimationCode(String m, String e){
		for(int i = 0; i<stations[m].Length; i++){
			Debug.Log (stations [m] [i].getName() + (e));
			if(stations[m][i].getName().Equals(e)){
				return stations[m][i].getAnimation();
			}
		}
		return -1;
	}
	private List<String> getAllMuscles(){
		List<String> muscles = new List<String>();
		foreach (KeyValuePair<String,Exercise []> entry in stations) {
			int i;
			for (i = 0; i < entry.Value.Length; i++) {
				String [] musclesTrainned = entry.Value [i].getMuscles();
				int j;
				for (j = 0; j < musclesTrainned.Length; j++) {
					if (!muscles.Contains (musclesTrainned [j])) {
						muscles.Add (musclesTrainned [j]);
					}
				}
			}
		}
		muscles.Sort ();
		return muscles;
	}
	private Exercise [] getExercisesForMuscle(String muscle){
		List<Exercise> exercises = new List<Exercise>();
		foreach (KeyValuePair<String,Exercise []> entry in stations) {
			int i;				
			for (i = 0; i < entry.Value.Length; i++) { //For each exercise
				String [] musclesTrainned = entry.Value [i].getMuscles();
				int j;
				for (j = 0; j < musclesTrainned.Length; j++) {
					if (object.Equals(musclesTrainned [j], muscle)) {
						Debug.Log ("YOOO" + entry.Value [i].getName() + "   " + muscle);
						exercises.Add (entry.Value [i]);
					}
				}
			}
		}
		return exercises.ToArray();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) { 
			goHome ();
		}
	}

	public void switchToMuscleView(){
		SceneManager.LoadScene ("searchMuscle");
	}

	public void goHome(){
		SceneManager.LoadScene("home");
	}

}
