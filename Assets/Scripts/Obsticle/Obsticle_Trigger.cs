﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Obsticle_Stats))]
public class Obsticle_Trigger : MonoBehaviour {

	public GameObject Obsticle;

	private Obsticle_Stats stats;
	private MeshRenderer mesh;

	// Use this for initialization
	void Start () {
		stats = gameObject.GetComponent<Obsticle_Stats>();
	}
	
	void OnTriggerStay(Collider col){
		if(col.tag == Global_Variables.PLAYER_TAG && Input.GetButtonDown(Global_Variables.BYPASS_OBSTICLE)){
			if(col.gameObject.GetComponent<Player_Stats>().GetEnergy() >= stats.EnergyCost){
				Obsticle_AnimHash hash = Obsticle.GetComponent<Obsticle_AnimHash>();

				Obsticle.GetComponent<Animator>().SetBool(hash.Activated, true);

				BoxCollider[] colliders = transform.parent.GetComponentsInChildren<BoxCollider>();
				foreach(BoxCollider c in colliders){
					c.enabled = !c.enabled;
				}

				gameObject.layer = Global_Variables.COLLISION_LAYER;


				col.gameObject.GetComponent<Player_Stats>().ChangeEnergy(-1 * stats.EnergyCost);
			}else{
				GameObject.FindGameObjectWithTag("GUI").GetComponent<gui>().timer = 0;
			}
		}
	}
}
