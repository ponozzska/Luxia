﻿using UnityEngine;
using System.Collections;

public class Elevator_MoveFollowers : MonoBehaviour {

	public GameObject Elevator;
	public GameObject MoveTo;
	[HideInInspector]
	public bool CanMove;

	private int followersIn = 0;
	private bool allIn;

	void Update(){
		if(allIn){
			for(int i = 0; i < Camera.main.GetComponent<Game_FollowerDeath>().NumberOfLivingFollowers();i++){
				Camera.main.GetComponent<Game_Manager>().followerInstances[i].transform.parent = Elevator.transform;
			}

			this.gameObject.GetComponent<BoxCollider>().enabled = false;

			CanMove = true;
			allIn = false;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == Global_Variables.FOLLOWER_TAG){
			followersIn++;
			if(followersIn <= 1){
				col.gameObject.GetComponent<Follow_Player>().Follows = MoveTo;
				col.gameObject.GetComponent<Follow_Player>().followsPlayer = false;
				allIn = true;
			}
		}
	}
}
