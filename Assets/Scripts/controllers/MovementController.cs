using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class MovementController : MonoBehaviour {

	AIPath myAIPath;
	GameObject targetTransform;

	//store the size of the explorable area
	public float gridX{get; protected set;}
	public float gridY{get; protected set;}

    bool targetReached;

    Action cbTargetReached;

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
		
		if(myAIPath.target != null){
			isTargetReached();
		}
	}

	public void setNewDestination(Transform target){

		myAIPath.target = target;
	}

	void isTargetReached(){
		if(((myAIPath.target.position - transform.position).magnitude < 0.2f)){
			if(targetReached == false){
				Debug.Log("Target Reached");
				cbTargetReached();
			}
			targetReached = true;
			
		}else{
			targetReached = false;
		}
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

    public void RegisterTargetReachedCallback(Action callback){
		cbTargetReached += callback;
	}

	public void UnRegisterTargetReachedCallback(Action callback){
		cbTargetReached -= callback;
	}
}
