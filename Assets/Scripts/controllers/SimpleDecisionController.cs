using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleDecisionController : MonoBehaviour {

	AIPath myAIPath;
	GameObject targetTransform;

	private Dictionary<string, Action> namedActions = new Dictionary<string, Action>();
	private string[] decisionNames = new string[3];
	// Use this for initialization
	void Start () {
		myAIPath = GetComponent<AIPath>();
		targetTransform = new GameObject();
		
		AddDecisionNames();
		CreateDictionary();
		InvokeRepeating("ChooseAction", 3, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//3 Simple "Decisions" which in fact only choose a different target
	void GoToPos1(){
		CalculateNewPos(new Vector3(-3, 3, 0));
		myAIPath.target = targetTransform.transform;
		Debug.Log("Action GoToPos1 selected");
	}

	void GoToPos2(){
		CalculateNewPos(new Vector3(3.5f, 3.5f, 0));
		myAIPath.target = targetTransform.transform;
		Debug.Log("Action GoToPos2 selected");
	}

	void GoToPos3(){
		CalculateNewPos(new Vector3(2, -3, 0));
		myAIPath.target = targetTransform.transform;
		Debug.Log("Action GoToPos3 selected");
	}

	void CalculateNewPos(Vector3 newPos){

		targetTransform.transform.position = newPos;
	}

	void AddDecisionNames(){

		decisionNames[0] = "GoToPos1";
		decisionNames[1] = "GoToPos2";
		decisionNames[2] = "GoToPos3";
	}

	void CreateDictionary(){

		namedActions["GoToPos1"] = () => GoToPos1();
		namedActions["GoToPos2"] = () => GoToPos2();
		namedActions["GoToPos3"] = () => GoToPos3();
	}

	void ChooseAction(){
		int randomActionNum = UnityEngine.Random.Range(0,3);
		string actionName = decisionNames[randomActionNum];

		namedActions[actionName]();
	}
}
