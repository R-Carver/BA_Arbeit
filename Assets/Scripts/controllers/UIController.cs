using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	GameObject agent;

	Text debug1;
	Text debug2;
	Text debug3;
	Text debug4;
	Text debug5;
	
	MovementController mov;
	StateMachine dfa;

	// Use this for initialization
	void Start () {

		initializeTextFields();

		//register Listeners
		//FIXME: This is a hack to make sure that the agent on which we
		//register the callbacks does allready exist, change this when you
		//are smarter
		Invoke("GetAgent", 0.5f);
		Invoke("GetScriptsForDebug", 0.5f);
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		UpdateDebug1();
		UpdateDebug2();
		UpdateDebug3();
		UpdateDebug5();
	}

//Update the text fields ==============================================================================

	private void UpdateDebug1(){

		//Currently: DFA.CURRENTSTATE

		if(dfa != null){
			debug1.text = dfa.CurrentState.ToString();
		}
	}

	private void UpdateDebug2(){

		//Currently: MOV.TARGETREACHED

		if(mov != null){
			debug2.text = "MovementContr targetReached: " + mov.targetReached;
		}
	}

	private void UpdateDebug3(){

		//Currently: MOV DISTANCE TO TARGET

		if(mov != null && mov.myAIPath.target != null){
			debug3.text = "Dist to target: " + (mov.myAIPath.target.position - agent.transform.position).magnitude;
		}
	}

	private void UpdateDebug5(){

		//Currently: DFA.HASACTION

		if(dfa != null ){
			debug5.text = "Statemachine hasAction: " + dfa.hasAction;
		}
	}

//=============================================================================================

	private void GetAgent(){

		agent = GameObject.Find("Momo");
	}

	private void GetScriptsForDebug(){

		mov = agent.GetComponent<MovementController>();
		dfa = agent.GetComponent<StateMachine>();
	}

	private void initializeTextFields(){
		Transform text1 = this.transform.Find("Debug1");
		debug1 = text1.GetComponent<Text>();
		debug1.text = "-";

		Transform text2 = this.transform.Find("Debug2");
		debug2 = text2.GetComponent<Text>();

		Transform text3 = this.transform.Find("Debug3");
		debug3 = text3.GetComponent<Text>();

		Transform text4 = this.transform.Find("Debug4");
		debug4 = text4.GetComponent<Text>();

		Transform text5 = this.transform.Find("Debug5");
		debug5 = text5.GetComponent<Text>();
	}
}
