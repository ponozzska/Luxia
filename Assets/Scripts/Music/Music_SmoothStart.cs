﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Music_SmoothStart : MonoBehaviour {

	public float SmoothingTime;
	
	private float t;
	private float originalVolume;
	private bool playMusic;
	private float musicModif;
	private Game_Configuration con;

	void Start(){
		GameObject config = GameObject.FindGameObjectWithTag(Global_Variables.CONFIG_TAG);

		playMusic = true;
		musicModif = 1f;

		if(config != null){
			con = config.GetComponent<Game_Configuration>();
			playMusic = con.MusicOn;
			musicModif = con.MusicVolume;
		}

		originalVolume = GetComponent<AudioSource>().volume;
		GetComponent<AudioSource>().volume = 0f;
		GetComponent<AudioSource>().Play();
	}

	void Update(){
		if(con != null){
			musicModif = con.MusicVolume;
			playMusic = con.MusicOn;
		}

		if(playMusic){
			GetComponent<AudioSource>().volume = Mathf.SmoothDamp(GetComponent<AudioSource>().volume, originalVolume * musicModif, ref t, SmoothingTime);
		}else{
			GetComponent<AudioSource>().volume = 0f;
		}
	}


}
