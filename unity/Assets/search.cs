using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;
using System;

public class search : MonoBehaviour {
	public GameObject list;
	public GameObject bro;
	public GameObject hideBroButton;
	public GameObject scrollView;
	public GameObject panel;

	private Dictionary<string, Exercise []> stations = new Dictionary<string, Exercise []>();
	private bool muscleView = true;

	private int[] off = { 0, -110, -170 };
	// Use this for initialization
	void Start () {
		stations.Add("squatRack",new Exercise[] {new Exercise("Highbar Squat",10, new String [] {"Quads","Lower Back"}), new Exercise("Overhead Squat",11, new String [] {"Quads","Deltoids","Lower Back"}), new Exercise("Piston Squat",12,new String [] {"Quads","Lower Back"}), new Exercise("Front Squat",13, new String [] {"Quads","Abs"})});
		stations.Add("curlStation",new Exercise []{new Exercise("Bicep Curl",20,new String []  {"Biceps"}),new Exercise("Shoulder press",21, new String [] {"Deltoids"})});
		stations.Add("deadlift",new Exercise []{new Exercise("Bentover Row",30, new String []  {"Biceps","Upper Back", "Lower Back"})});
		populateSearch ();
	}
	void populateSearch(){
		List<String> muscles = getAllMusclesExercised ();
		int x = 0;
		//For each station
		foreach (KeyValuePair<String,Exercise []> entry in stations) {
			//Copy the big category block
			GameObject category = Instantiate (GameObject.Find("Category"));
			//Move to same position as parent
			category.transform.position = GameObject.Find("Category").transform.position;
			//Set parent object
			category.transform.parent = list.gameObject.transform;
			//Move it down by a predifined amount
			category.transform.localPosition += new Vector3 (0, off[x], 0);
			category.transform.localScale = GameObject.Find ("Category").transform.localScale;
			//Set the parent as the list
			category.transform.parent = list.gameObject.transform;
			//Set the heading as the key
			category.GetComponentsInChildren<Text>()[0].text = entry.Key;
			//Draw the exercises relating to this station
			drawExercisesForStation (entry.Key,++x, category);
		}
		GameObject.Find("Category").SetActive(false);
	}
	void drawExercisesForStation(String s, int x, GameObject parent){
		int i;
		Debug.Log (s);

		//For each exercise in the station
		for (i = 0;i<stations[s].Length;i++) {
			//Copy the initial text of the new block
			RawImage created = (RawImage)Instantiate (GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1]);
			//Position in first position
			created.transform.position = GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1].transform.position;
			//Set the overall block as the parent
			created.transform.parent = GameObject.FindGameObjectsWithTag("Category")[x].transform;
			created.transform.localScale = GameObject.FindGameObjectsWithTag("Category")[x].GetComponentsInChildren<RawImage>()[1].transform.localScale;
			//Set the element down by 120 each time
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

	void showBro(String machine,String exercise)
	{
		Debug.Log (exercise + "  " + machine);
		bro.SetActive (true);
		panel.SetActive (false);
		scrollView.SetActive (false);
		hideBroButton.SetActive (true);
		int an = getAnimationCode (machine, exercise);
		Debug.Log (an);
		bro.gameObject.GetComponent<Animator> ().SetInteger ("state", an);
		bro.transform.localPosition = new Vector3(0, -500, 377);
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
	private List<String> getAllMusclesExercised(){
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
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) { 
			goHome ();
		}
	}

	public void setSearchView(bool view){
		muscleView = view;
	}

	public void goHome(){
		SceneManager.LoadScene("home");
	}
}
