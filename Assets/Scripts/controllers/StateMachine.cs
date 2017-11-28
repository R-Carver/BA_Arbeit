using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour {

	private DecisionState currentState;
	public DecisionState CurrentState
	{
		get{return currentState;}
		protected set{

			DecisionState oldState = currentState;
			currentState = value;
			if(cbDecisionStateChanged != null){
				Debug.Log("State was changed");
				cbDecisionStateChanged(this.currentState, oldState);
			}
		}
	}

    public float visionRadius = 2f;

	//FIXME: for now all colliders which aren't food colliders are collected here
	public Collider2D[] colliders;		
	public Collider2D food;		

	//FIXME: the actions are here only to register the callbacks on them
	//Try to uncouple that later
	private Action_Explore action_Ex;
	private Action_GetFood action_GF;
	
	//true if the agent is following some action at the moment,
	//false only for the short moment after completing some action
	//maybe also some idle state might make sense here
	private bool hasAction;

	//Call this function when the Decisionstate of the Momo is chagned
	//The Callback needs the old state to disable the old action and the new state
	//to enable the new action
    Action<DecisionState, DecisionState> cbDecisionStateChanged;
	
	// Use this for initialization
	void Start () {
		
		//the default action
		currentState = DecisionState.Explore;
		//currentState = DecisionState.GetFood;

		action_Ex = GetComponent<Action_Explore>();
		action_GF = GetComponent<Action_GetFood>();
		
		action_GF.RegisterActionIsDoneCallback(OnActionFinished);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//colliders that get into the vision radius of this character
		colliders = Physics2D.OverlapCircleAll(this.transform.position, visionRadius);
		//FIXME: If we did see some food, this shouldnt be called any longer
		//only food colliders in the vision readius
		food = Physics2D.OverlapCircle(this.transform.position, visionRadius, LayerMask.GetMask("FoodLayer"));
		
		//here the actual state machine is implemented
		if(food != null && hasAction == false){
			CurrentState = DecisionState.GetFood;
			hasAction = true;
		}else if(hasAction == false){
			CurrentState = DecisionState.Explore;
			hasAction = true;
		}
		
	}

	private void OnActionFinished(){
		
		hasAction = false;
	}



	//FIXME: This does not really belong here
	void OnDrawGizmos()
     {
         Gizmos.DrawWireSphere(transform.position, visionRadius);
     }


	public void RegisterDecisionChangedCallback(Action<DecisionState, DecisionState> callback){
		cbDecisionStateChanged += callback;
	}

	public void UnRegisterDecisionChangedCallback(Action<DecisionState, DecisionState> callback){
		cbDecisionStateChanged -= callback;
	}
}
