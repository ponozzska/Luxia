﻿using UnityEngine;
using System.Collections;

public class Swarm_Movement : MonoBehaviour {

	public float Speed;
	public float StartScale;
	public float MaxScale;
	public float ToReachMaxScale;
	public float ToDieFromMaxScale;
	public float PlayerOffset;
	public float StartCharging;
	public float ObsticleCheck;
	public LayerMask CollisionMasks;
	
	private float sizeIncreasePerSec;
	private float sizeDecreasePerSec;
	private GameObject player;
	private Light playerLight;

	void Start(){
		Vector3 scale = new Vector3(StartScale, StartScale, transform.localScale.z);

		gameObject.transform.localScale = scale;

		sizeIncreasePerSec = MaxScale / ToReachMaxScale;
		sizeDecreasePerSec = MaxScale / ToDieFromMaxScale;

		player = GameObject.FindGameObjectWithTag(Global_Variables.PLAYER_TAG);
		playerLight = player.GetComponentInChildren<Light>();
	}

	void FixedUpdate(){
		if(!Global_Variables.Instance.PlayerInSecondLevel){
			IncreaseScale();
			MoveToPlayer(Speed, Mathf.Sqrt(playerLight.range));

		}
	}

	private void CheckDeath(){
		if(gameObject.transform.localScale.x <= 0f){
			Destroy(this.gameObject);
		}
	}

	private float DistanceFromPlayer(){
		return Vector3.Distance(gameObject.transform.position, player.transform.position);
	}

	private bool IsInOffsetRange(){
		float distToPlayer = DistanceFromPlayer();
		return distToPlayer + PlayerOffset <= playerLight.range && distToPlayer >= playerLight.range;
	}

	private bool IsInLightRange(){
		return DistanceFromPlayer() < playerLight.range;
	}

	private void MoveToPlayer(float speed, float offset){
		float dist = DistanceFromPlayer();
		float direction = 0;
		float currentX = transform.position.x;
		float currentY = transform.position.y;
		
		direction = player.transform.position.x - transform.position.x;

		if(!CheckYObsticle(player.transform.position.y - transform.position.y)){
			currentY = Mathf.Lerp(currentY, player.transform.position.y, Time.deltaTime * speed);
		}

		if(!CheckXObsticle(player.transform.position.x - transform.position.x)){
			currentX =  Mathf.Lerp(currentX, player.transform.position.x - offset, Time.deltaTime * speed);
		}

		Vector3 scale = transform.localScale;
		
		if((currentX - transform.position.x != 0) && Mathf.Sign(scale.x) != Mathf.Sign(currentX - transform.position.x)){
			scale.x = -1 * scale.x;
			transform.localScale = scale;
		}

		Vector3 moveTo = new Vector3(currentX, currentY, transform.position.z);
		
		rigidbody.MovePosition(moveTo);		
	}

	private bool CheckYObsticle(float dir){
		RaycastHit hit;
		Ray ray;
		Vector2 direction = new Vector2(0f, Mathf.Sign(dir));

		ray = new Ray(this.transform.position, direction);
			
		if(Physics.Raycast(ray, out hit, ObsticleCheck, CollisionMasks)){
			return true;
		}

		
		return false;
	}

	private bool CheckXObsticle(float dir){
		RaycastHit hit;
		Ray ray;
		Vector2 direction = new Vector2(Mathf.Sign(dir), 0f);

		ray = new Ray(this.transform.position, direction);
			
		if(Physics.Raycast(ray, out hit, ObsticleCheck, CollisionMasks)){
			return true;
		}

		return false;
	}

	private void IncreaseScale(){
		float scale = transform.localScale.y + sizeIncreasePerSec * Time.deltaTime;

		if(scale >= MaxScale){
			scale = MaxScale;
		}

		Vector3 newScale = new Vector3(Mathf.Sign(transform.localScale.x) * scale, scale, transform.localScale.z);

		transform.localScale = newScale;
	}

	private void DecreaseScale(){
		float scale = transform.localScale.x - sizeDecreasePerSec * Time.deltaTime;

		Vector3 newScale = new Vector3(scale, scale, transform.localScale.z);
		
		transform.localScale = newScale;
	}
}
