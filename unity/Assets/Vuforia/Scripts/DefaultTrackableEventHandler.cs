/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;

namespace Vuforia{

    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {

		//public Texture LogoTexture;
		//public Texture MobiliyaTexture;
		private TrackableBehaviour mTrackableBehaviour;

		public GameObject obj;
		public GameObject list;

		public Dictionary<string, Exercise []> exercises = new Dictionary<string, Exercise []>();
	
        #region UNTIY_MONOBEHAVIOUR_METHODS
        void Start()
        {
			exercises.Add("squatRack",new Exercise[] {new Exercise("Highbar Squat",1), new Exercise("Overhead Squat",3)});
			exercises.Add("curlStation",new Exercise []{new Exercise("Bicep Curl",2)});
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
			obj.SetActive(true);
			list.SetActive (true);
			if(mTrackableBehaviour.TrackableName.Equals("squatRack"))
			{
				Debug.Log("Squaaat");
				obj.transform.parent = mTrackableBehaviour.transform;
				obj.transform.position = mTrackableBehaviour.transform.position;
				setAnimation (1);
			}
			else if(mTrackableBehaviour.TrackableName.Equals("curlStation"))
			{
				Debug.Log("Currrrrrl");
				obj.transform.parent = mTrackableBehaviour.transform;
				obj.transform.position = mTrackableBehaviour.transform.position;
				setAnimation (2);
			}
			populateList (mTrackableBehaviour.TrackableName);


            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");


        }


        private void OnTrackingLost()
        {

			setAnimation (0);
			obj.transform.parent = null;
			obj.SetActive(false);
			list.SetActive (false);

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

		private void setAnimation(int animation){
			Debug.Log("ANIMATION "+animation);
			obj.gameObject.GetComponent<Animator> ().SetInteger ("state", animation);
		}

		private void populateList(string station){
			int i;
			for(var x =0;x<list.transform.childCount;x++){
				list.transform.GetChild (x).gameObject.SetActive (true);
			}
			for (i = 0; i < exercises [station].Length; i++) {
				Debug.Log("EXERCISE: "+i+ exercises[station][i].getName());
				list.transform.GetChild(i).gameObject.GetComponentsInChildren<Text>()[0].text = exercises [station] [i].getName ();
				Debug.Log(i);
				int ani = exercises [station] [i].getAnimation ();
				Debug.Log ("animation:" + ani);
				list.transform.GetChild(i).gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => setAnimation(ani));
			}
			for(i=i;i<list.transform.childCount;i++){
				list.transform.GetChild (i).gameObject.SetActive (false);
			}
				
		}
		void OnGUI() {

			//set up scaling

			//Rect mButtonRect = new Rect(1920-215,5,210,110);
			GUIStyle myTextStyle = new GUIStyle(GUI.skin.textField);
			myTextStyle.fontSize = 100;
			myTextStyle.richText=true;

			//GUI.DrawTexture(new Rect(5,1080- 115,350,110),LogoTexture); 
			//GUI.DrawTexture (new Rect (1530, 970, 350, 110), MobiliyaTexture);


			//if (!btntexture) // This is the button that triggers AR and UI camera On/Off
			//{
			//	Debug.LogError("Please assign a texture on the inspector");
			//	return;
			//}
			/*
			switch(mShowGUIButton) {
			case 1: //squat rack
				if (GUI.Button(new Rect(50,30, 1500, 200), "High bar squat",myTextStyle))
					{
						Debug.Log("Clicked the button with text");
						obj.gameObject.GetComponent<Animator> ().SetInteger ("state", 1);

					}
				if (GUI.Button(new Rect(50, 230, 1500, 200), "Ovearhead squat",myTextStyle))
					{
						Debug.Log("Clicked the button with text");
						obj.gameObject.GetComponent<Animator> ().SetInteger ("state", 3);
					}
				break;
			case 2:
				GUI.Button (new Rect (50, 30, 1500, 200), "Curl",myTextStyle);
					obj.gameObject.GetComponent<Animator> ().SetInteger ("state", 2);
				break;
			default:
					GUI.Label(new Rect(0, (Screen.height/2)-50, Screen.width, 100), "<b> Scan a marker! </b>",myTextStyle);
				break;
			*/

				//GUI.Box (new Rect (1920 - 100,0,100,50), "Top-right");
				//GUI.Box (new Rect (0,1080- 50,100,50), "Bottom-left");
				//GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), "Bottom right");

				// draw the GUI button
			//if (GUI.Button(mButtonRect, btntexture)) {
					// do something on button click 
			//		OpenVideoActivity();
			//	}

		}
			
        #endregion // PRIVATE_METHODS
    }
}

