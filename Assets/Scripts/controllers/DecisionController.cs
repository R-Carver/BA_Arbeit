/*
    The purpose of this script is to enable and disable the action script
    depending on the stateMachine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(StateMachine))]
public class DecisionController : MonoBehaviour {

    StateMachine dfa;

    //This is a mapping of the actions to their respective scirpt-components. So they can be en/disabled
    Dictionary<DecisionState, string> actionScripts;

	// Use this for initialization
	void Start () {

		dfa = GetComponent<StateMachine>();
        dfa.RegisterDecisionChangedCallback(OnStateChanged);

        //set the Dictionary
        InitializeActionScriptDictionary();
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    //This function is a callback for a changed Decision.
    //It disables the action-script for the old action and enables the script for the new one
    public void OnStateChanged(DecisionState newState, DecisionState oldState){

        //Dissable old action component
        if(actionScripts.ContainsKey(oldState)){
            
            //here we need to grad the MonoBehaviour because it has the enabled property
            MonoBehaviour mono = (MonoBehaviour)GetComponent(actionScripts[oldState]);
            mono.enabled = false;
        }else{
            Debug.LogError("DecisionController OnStateChanged - oldState was not in the Dictionary");
            return;
        }
        
        //Enable new action component
        if(actionScripts.ContainsKey(newState)){
            
            //here we need to grad the MonoBehaviour because it has the enabled property
            MonoBehaviour mono = (MonoBehaviour)GetComponent(actionScripts[newState]);
            mono.enabled = true;
        }else{
            Debug.LogError("DecisionController OnStateChanged - newState was not in the Dictionary");
            return;
        }
    }

    private void InitializeActionScriptDictionary(){

        actionScripts = new Dictionary<DecisionState, string>();

        //FIXME: Maybe those states should be configured somehow different in the beginning
        //For now we just hardcode them here

        actionScripts[DecisionState.Explore] = "Action_Explore";
        actionScripts[DecisionState.GetFood] = "Action_GetFood";    
    }



}
