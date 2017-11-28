using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class ExplorationController : MonoBehaviour {

	AIPath myAIPath;
	GameObject targetTransform;

	//store the size of the explorable area
	float gridX;
	float gridY;

	//this makes sure that the exploration concentrates on a certain area for a while
	float alpha = 1f;

	// Use this for initialization
	void Start () {
		myAIPath = GetComponent<AIPath>();
		targetTransform = new GameObject();

		//when explore is called, the character is staying, so the target
		//position is the char position
		targetTransform.transform.position = this.transform.position;
		myAIPath.target = targetTransform.transform;
		setSize();	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isTargetReached() == true){

			//set new Destination and adjust alpha
			setNewDestination();
		}
		
	}

	void setNewDestination(){

		//set destination to something in the range of the explorable area
		float targetX = alpha * (UnityEngine.Random.Range(0,gridX));
		float targetY = alpha * (UnityEngine.Random.Range(0,gridY));
		targetTransform.transform.position = new Vector3(targetX, targetY, 0);
		//Debug.Log("Destination vector: " + targetTransform.transform.position);

		myAIPath.target = targetTransform.transform;
		//Debug.Log("AI Target: " + myAIPath.target.position);

		//adjust alpha to let the agent explore destinations close to the current target
		if(alpha > 0.2){
			alpha -= 0.2f;
		}else{
			//if alpha is to small reset it to 1, so a new reagion will be explored
			alpha = 1;
		}
		


	}

	bool isTargetReached(){
		if((myAIPath.target.position - transform.position).magnitude < 0.2f ){
			
			Debug.Log("Target Reached");
			return true;
		}else{

			return false;
		}
	}

	void explore(){

		
		

		
	}

	//calculate position which is scaled by alpha to make
	//the character search certain areas for a while before
	//resetting alpa and exploring more distant positions
	void CalculateRandomAlphaPos(){

	}

	
	void CalculateNewPos(Vector3 newPos){

		targetTransform.transform.position = newPos;
	}


	void setSize(){
		AstarData data = AstarPath.active.data;
		GridGraph graph = data.gridGraph;

		//Debug.Log("This is the graph size:" + graph.size);
		Vector2 dimensions = graph.size;
		this.gridX = dimensions.x;
		this.gridY = dimensions.y;
	}
	
}
