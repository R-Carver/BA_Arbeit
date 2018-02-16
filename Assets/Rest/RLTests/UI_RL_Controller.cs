using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RL_Controller : MonoBehaviour {

	//state1
	public Text No_No_Explore;

	//state2
	public Text No_Target_Explore;
	public Text No_Target_Collect;

	//state3
	public Text Load_Target_Explore;
	public Text Load_Target_Collect;
	public Text Load_Target_Trade;
	public Text Load_Target_Eat;

	//state4
	public Text Load_NoTarget_Explore;
	public Text Load_NoTarget_Trade;
	public Text Load_NoTarget_Eat;

	public Image State_No_No;
	public Image State_No_Target;
	public Image State_Load_Target;
	public Image State_Load_NoTarget;

	RL_QLerner qLerner;

	RL_QState no_No;
	RL_QState no_Target;
	RL_QState load_Target;
	RL_QState load_NoTarget;

	RL_State currentState;

	// Use this for initialization
	void Start () {
		qLerner = RL_QLerner.Instance;
	}
	
	// Update is called once per frame
	void Update () {

		currentState = qLerner.currentState;
		UpdateStates();
		UpdateQStates();
	}

	private void UpdateQStates(){

		//state1
		no_No = qLerner.qStates[0];
		No_No_Explore.text = no_No.qValues["explore"].ToString();

		//state2
		no_Target = qLerner.qStates[1];
		No_Target_Explore.text = no_Target.qValues["explore"].ToString();
		No_Target_Collect.text = no_Target.qValues["collect"].ToString();

		//state3
		load_Target = qLerner.qStates[2];
		Load_Target_Explore.text = load_Target.qValues["explore"].ToString();
		Load_Target_Collect.text = load_Target.qValues["collect"].ToString();
		Load_Target_Eat.text = load_Target.qValues["eat"].ToString();
		Load_Target_Trade.text = load_Target.qValues["trade"].ToString();

		//state4
		load_NoTarget = qLerner.qStates[3];
		Load_NoTarget_Explore.text = load_NoTarget.qValues["explore"].ToString();
		Load_NoTarget_Eat.text = load_NoTarget.qValues["eat"].ToString();
		Load_NoTarget_Trade.text = load_NoTarget.qValues["trade"].ToString();
	}

	private void UpdateStates(){

		if(currentState.name == "NoLoadNoTarget"){
			State_No_No.color = new Color(0.4f,0.05f, 0.05f);
		}else{
			State_No_No.color = (Color)new Color32(55,66, 77,255);
		}

		if(currentState.name == "NoLoadSeeTarget"){
			State_No_Target.color = new Color(0.4f,0.05f, 0.05f);
		}else{
			State_No_Target.color = (Color)new Color32(55,66, 77,255);
		}

		if(currentState.name == "LoadTarget"){
			State_Load_Target.color = new Color(0.4f,0.05f, 0.05f);
		}else{
			State_Load_Target.color = (Color)new Color32(55,66, 77,255);
		}

		if(currentState.name == "LoadNoTarget"){
			State_Load_NoTarget.color = new Color(0.4f,0.05f, 0.05f);
		}else{
			State_Load_NoTarget.color = (Color)new Color32(55,66, 77,255);
		}
	}
}
