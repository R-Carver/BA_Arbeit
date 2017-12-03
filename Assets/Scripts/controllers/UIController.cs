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
	Text debug6;
	Text debug7;
	Text debug8;
	Text debug9;
	
	MovementController mov;
	StateMachine dfa;
	Action_GetFood action_GetFood;
	QLearner qLearner;

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
		UpdateDebug4();
		UpdateDebug5();
		UpdateDebug6();
		UpdateDebug7();
		UpdateDebug8();
		UpdateDebug9();
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

	private void UpdateDebug4(){

		//Currently: MOV.AIPATH.TARGET
		if(mov.myAIPath.target != null ){
			debug4.text = "A* Target: " + mov.myAIPath.target.position;
		}
	}

	private void UpdateDebug5(){

		//Currently: DFA.HASACTION

		if(dfa != null ){
			debug5.text = "Statemachine hasAction: " + dfa.hasAction;
		}
	}

	private void UpdateDebug6(){

		//Currently: QLearner. RedQState Eat

		if(action_GetFood != null ){
			qLearner = action_GetFood.getQLearner();
			debug6.text = "QState Red Eat: " + qLearner.getQEatRed();
		}
	}

	private void UpdateDebug7(){

		//Currently: QLearner. RedQState Dont Eat

		if(action_GetFood != null ){
			qLearner = action_GetFood.getQLearner();
			debug7.text = "QState Red Dont Eat: " + qLearner.getQDontEatRed();
		}
	}

	private void UpdateDebug8(){

		//Currently: QLearner. GreenQState Eat

		if(action_GetFood != null ){
			qLearner = action_GetFood.getQLearner();
			debug8.text = "QState Green Eat: " + qLearner.getQEatGreen();
		}
	}

	private void UpdateDebug9(){

		//Currently: QLearner. GreenQState Dont Eat

		if(action_GetFood != null ){
			qLearner = action_GetFood.getQLearner();
			debug9.text = "QState Green Dont Eat: " + qLearner.getQDontEatGreen();
		}
	}

//=============================================================================================

	private void GetAgent(){

		agent = GameObject.Find("Momo");
	}

	private void GetScriptsForDebug(){

		mov = agent.GetComponent<MovementController>();
		dfa = agent.GetComponent<StateMachine>();
		action_GetFood = agent.GetComponent<Action_GetFood>();
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

		Transform text6 = this.transform.Find("Debug6");
		debug6 = text6.GetComponent<Text>();

		Transform text7 = this.transform.Find("Debug7");
		debug7 = text7.GetComponent<Text>();

		Transform text8 = this.transform.Find("Debug8");
		debug8 = text8.GetComponent<Text>();

		Transform text9 = this.transform.Find("Debug9");
		debug9 = text9.GetComponent<Text>();
	}
}
