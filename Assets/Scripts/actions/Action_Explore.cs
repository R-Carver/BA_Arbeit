using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class Action_Explore : MonoBehaviour {

    MovementController movController;

    GameObject target;
    
    //store the size of the explorable area
	float gridX;
	float gridY;

	//this makes sure that the exploration concentrates on a certain area for a while
	float alpha = 1f;

	// Use this for initialization
	void Start () {	

		movController = GetComponent<MovementController>();
        gridX = movController.gridX;
        gridY = movController.gridY;

        target = new GameObject();
		//we want to know when the target is reached, so we can set a new Destination
        movController.RegisterTargetReachedCallback(setNewDestination);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setNewDestination(){

		if(this.enabled == false){
			return;
		}
		//set destination to something in the range of the explorable area
		float targetX = alpha * (UnityEngine.Random.Range(0,gridX));
		float targetY = alpha * (UnityEngine.Random.Range(0,gridY));
		target.transform.position = new Vector3(targetX, targetY, 0);
		//Debug.Log("Destination vector: " + targetTransform.transform.position);

		movController.setNewDestination(target.transform);
		//Debug.Log("AI Target: " + myAIPath.target.position);

		//adjust alpha to let the agent explore destinations close to the current target
		if(alpha > 0.2){
			alpha -= 0.2f;
		}else{
			//if alpha is to small reset it to 1, so a new reagion will be explored
			alpha = 1;
		}
	}
}
