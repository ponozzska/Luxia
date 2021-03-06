﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture button;
	public Font nightmareFont;
	public GUISkin skin;

	void OnGUI ()
	{
		if (!GetComponent<Options> ().optionsOn) {
						Cursor.visible = true;
						GUIStyle fontStyle = new GUIStyle ();
						fontStyle.font = nightmareFont;
						fontStyle.fontSize = 30;
						fontStyle.fontStyle = FontStyle.Bold;
						fontStyle.alignment = TextAnchor.MiddleCenter;
		
						int screenWidth = Screen.width;
						int screenHeight = Screen.height;

						GUI.skin = skin;
						if (GUI.Button (new Rect (screenWidth - (3 * screenWidth / 7), 0 + screenHeight / 10, screenWidth / 4, screenHeight / 7), "New Game")) {
							Cursor.visible = false;	
							Application.LoadLevel ("base");
						}
						if(GUI.Button (new Rect (screenWidth - (3 * screenWidth / 7), 0 + screenHeight / 10 + screenHeight / 7 - 10, screenWidth / 4, screenHeight / 7), "How to play")){
								Application.LoadLevel ("howToPlay");
						}
						if (GUI.Button (new Rect (screenWidth - (3 * screenWidth / 7), 0 + screenHeight / 10 + 2 * (screenHeight / 7 - 10), screenWidth / 4, screenHeight / 7), "Options")) {
								GetComponent<Options> ().musicVolume = GameObject.FindGameObjectWithTag ("Config").GetComponent<Game_Configuration> ().MusicVolume * 100;
								GetComponent<Options> ().sfxVolume = GameObject.FindGameObjectWithTag ("Config").GetComponent<Game_Configuration> ().SFXVolume * 100;
								GetComponent<Options> ().musciOn = GameObject.FindGameObjectWithTag ("Config").GetComponent<Game_Configuration> ().MusicOn;
								GetComponent<Options> ().optionsOn = true;
						}
						if(GUI.Button (new Rect (screenWidth - (3 * screenWidth / 7), 0 + screenHeight / 10 + 3 * (screenHeight / 7 - 10), screenWidth / 4, screenHeight / 7), "Credits")){
								Application.LoadLevel ("credits");
						}
						if (GUI.Button (new Rect (screenWidth - (3 * screenWidth / 7), 0 + screenHeight / 10 + 4 * (screenHeight / 7 - 10), screenWidth / 4, screenHeight / 7), "Quit")) {
								Application.Quit (); 		
						}
				}

		
		
	}
}