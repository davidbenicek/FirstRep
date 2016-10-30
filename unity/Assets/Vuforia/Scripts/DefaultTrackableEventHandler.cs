/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
		float native_width= 1920f;
		float native_height= 1080f;
		public Texture btntexture;
		//public Texture LogoTexture;
		//public Texture MobiliyaTexture;
		private TrackableBehaviour mTrackableBehaviour;

		private int mShowGUIButton = 0;
		public GameObject obj;
        #region UNTIY_MONOBEHAVIOUR_METHODS
        void Start()
        {

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
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
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
			if(mTrackableBehaviour.TrackableName.Equals("squatRack"))
			{
				Debug.Log("Squaaat");
				mShowGUIButton = 1;
				obj.transform.parent = mTrackableBehaviour.transform;
				obj.transform.position = mTrackableBehaviour.transform.position;
				obj.gameObject.GetComponent<Animator> ().SetInteger ("state", 1);
			}
			else
			{
				Debug.Log("Currrrrrl");
				mShowGUIButton = 2;
				obj.transform.parent = mTrackableBehaviour.transform;
				obj.transform.position = mTrackableBehaviour.transform.position;
				obj.gameObject.GetComponent<Animator> ().SetInteger ("state", 2);
			}

            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");


        }


        private void OnTrackingLost()
        {
			mShowGUIButton = 0;

            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

			obj.transform.parent = null;
			obj.gameObject.GetComponent<Animator> ().SetInteger ("state", 0);

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
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
			case 0:
					GUI.Label(new Rect(0, (Screen.height/2)-50, Screen.width, 100), "<b> Scan a marker! </b>",myTextStyle);
				break;
			

				//GUI.Box (new Rect (1920 - 100,0,100,50), "Top-right");
				//GUI.Box (new Rect (0,1080- 50,100,50), "Bottom-left");
				//GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), "Bottom right");

				// draw the GUI button
			//if (GUI.Button(mButtonRect, btntexture)) {
					// do something on button click 
			//		OpenVideoActivity();
			//	}
			}
		}
			
        #endregion // PRIVATE_METHODS
    }
}
