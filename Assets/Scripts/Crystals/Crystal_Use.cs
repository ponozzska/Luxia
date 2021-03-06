﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Obsticle_Stats))]
public class Crystal_Use : MonoBehaviour {

	public float ChannelingTime;

	private Light light;
	private BoxCollider trigger;
	private Obsticle_Stats stats;
	private float lightRangeDecrease;
	private float lightIntensityDecrease;
	private bool startDecreasing;
	private float timer;
	private float addLifePerSecond;

	// Use this for initialization
	void Start () {
		light = gameObject.GetComponentInChildren<Light>();
		stats = gameObject.GetComponent<Obsticle_Stats>();
		lightRangeDecrease = light.range / ChannelingTime;
		lightIntensityDecrease = light.intensity / ChannelingTime;
		addLifePerSecond = (-1 * stats.EnergyCost) / ChannelingTime;
	}

	void Update(){
		if(startDecreasing){
			timer += Time.deltaTime;

			light.intensity -= lightIntensityDecrease * Time.deltaTime;
			light.range -= lightRangeDecrease * Time.deltaTime;
			GameObject p = GameObject.FindGameObjectWithTag(Global_Variables.PLAYER_TAG);

			p.GetComponent<Player_Stats>().ChangeEnergy(addLifePerSecond * Time.deltaTime);

			if(timer >= ChannelingTime){
				timer = 0;
				TurnOffLight();
			}
		}
	}

	void OnTriggerStay(Collider col){
		if(col.tag == Global_Variables.PLAYER_TAG && Input.GetButtonDown(Global_Variables.BYPASS_OBSTICLE)){
			Global_Variables.Instance.FreezeTime = true;

			gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
			col.gameObject.GetComponentInChildren<Light>().intensity *= Global_Variables.PLAYER_INTENSITY_INCREASE;

			startDecreasing = true;
		}
	}

	private void TurnOffLight(){
		gameObject.GetComponentInChildren<Light>().enabled = false;
		startDecreasing = false;

		//GameObject.FindGameObjectWithTag(Global_Variables.PLAYER_TAG).GetComponent<Player_Stats>().ChangeEnergy(-1 * stats.EnergyCost);

		Global_Variables.Instance.FreezeTime = false;
	}	
}
