﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Obsticle_Stats))]
public class Obsticle_Trigger : MonoBehaviour {

	public GameObject Bridge;

	private Obsticle_Stats stats;
	private MeshRenderer mesh;

	// Use this for initialization
	void Start () {
		stats = gameObject.GetComponent<Obsticle_Stats>();
	}
	
	void OnTriggerStay(Collider col){
		if(col.tag == Global_Variables.PLAYER_TAG && Input.GetButtonUp(Global_Variables.BYPASS_OBSTICLE)){
			GameObject player = GameObject.FindGameObjectWithTag(Global_Variables.PLAYER_TAG);

			if(player.GetComponent<Player_Stats>().GetEnergy() >= stats.EnergyCost){
				Bridge_AnimHash hash = Bridge.GetComponent<Bridge_AnimHash>();

				Bridge.GetComponent<Animator>().SetBool(hash.Activated, true);

				BoxCollider[] colliders = transform.parent.GetComponentsInChildren<BoxCollider>();
				foreach(BoxCollider c in colliders){
					c.enabled = !c.enabled;
				}

				gameObject.layer = Global_Variables.COLLISION_LAYER;


				player.GetComponent<Player_Stats>().ChangeEnergy(-1 * stats.EnergyCost);
			}else{
				//not enough energy!
			}
		}
	}
}
