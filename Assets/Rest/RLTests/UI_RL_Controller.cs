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

	//States
	public Image State_No_No;
	public Image State_No_Target;
	public Image State_Load_Target;
	public Image State_Load_NoTarget;

	//QStates
	public Image qState_1_explore;
	public Image qState_2_explore;
	public Image qState_2_collect;
	public Image qState_3_explore;
	public Image qState_3_collect;
	public Image qState_3_eat;
	public Image qState_3_trade;
	public Image qState_4_explore;
	public Image qState_4_eat;
	public Image qState_4_trade;

	private Image oldBlinkState;
	private Image currentBlinkState;

	RL_QLerner currentQLerner;
	Game_Util currentUtil;

	RL_QState no_No;
	RL_QState no_Target;
	RL_QState load_Target;
	RL_QState load_NoTarget;

	RL_State currentState;

	//Timer for Blinking Animation
	private float timer = 0.0f;
	private float waitingTime = 1f;
	private Color oldColor = new Color(0, 204, 204, 255);
	private Color newColor = new Color(255, 165, 0, 255);
	private Color tempColor;

	private GameController gameController;
	private Momo selectedMomo;

	// Use this for initialization
	void Start () {

		gameController = GameController.Instance;

		//currentQLerner = RL_QLerner.Instance;
		//currentUtil = Game_Util.Instance;
		//Debug.Log("UI Controller: " + currentUtil.ToString());

		oldBlinkState = qState_1_explore;
		currentBlinkState = qState_1_explore;
	}
	
	// Update is called once per frame
	void Update () {

		SetCurrentMomo();
		if(currentQLerner != null){

			currentState = currentQLerner.currentState;
			UpdateStates();
			UpdateQStates();
			BlinkAnimation();
		}
	}

	private void SetCurrentMomo(){
		if(selectedMomo != null){

			if(selectedMomo != gameController.selectedMomo){

				//should only happen when a new momo has been selected
				selectedMomo = gameController.selectedMomo;
				GameObject momoGo = WorldController.Instance.getGoFromMomo(selectedMomo);
				currentQLerner = momoGo.GetComponent<RL_QLerner>();
				currentUtil = momoGo.GetComponent<Game_Util>();
			}
		}else{
			//this happens when no momo has been selected yet
			if(gameController.selectedMomo != null){

				//this is the first momo which has been selected
				selectedMomo = gameController.selectedMomo;
				GameObject momoGo = WorldController.Instance.getGoFromMomo(selectedMomo);
				currentQLerner = momoGo.GetComponent<RL_QLerner>();
				currentUtil = momoGo.GetComponent<Game_Util>();
			}
		}
		
	}

	private void UpdateQStates(){

		//state1
		no_No = currentQLerner.qStates[0];
		No_No_Explore.text = no_No.qValues["explore"].ToString();

		//state2
		no_Target = currentQLerner.qStates[1];
		No_Target_Explore.text = no_Target.qValues["explore"].ToString();
		No_Target_Collect.text = no_Target.qValues["collect"].ToString();

		//state3
		load_Target = currentQLerner.qStates[2];
		Load_Target_Explore.text = load_Target.qValues["explore"].ToString();
		Load_Target_Collect.text = load_Target.qValues["collect"].ToString();
		Load_Target_Eat.text = load_Target.qValues["eat"].ToString();
		Load_Target_Trade.text = load_Target.qValues["trade"].ToString();

		//state4
		load_NoTarget = currentQLerner.qStates[3];
		Load_NoTarget_Explore.text = load_NoTarget.qValues["explore"].ToString();
		Load_NoTarget_Eat.text = load_NoTarget.qValues["eat"].ToString();
		Load_NoTarget_Trade.text = load_NoTarget.qValues["trade"].ToString();
	}

	private void UpdateStates(){

		if(currentState.name == "NoLoadNoTarget"){
			State_No_No.color = new Color(0.4f,0.05f, 0.05f);
			if(currentUtil.executor.currentAction.name == "explore"){
				
				ChangeBlinkState(qState_1_explore);
			}
		}else{
			State_No_No.color = (Color)new Color32(55,66, 77,255);
		}

		if(currentState.name == "NoLoadSeeTarget"){
			State_No_Target.color = new Color(0.4f,0.05f, 0.05f);
			if(currentUtil.executor.currentAction.name == "explore"){
				
				ChangeBlinkState(qState_2_explore);
			}else if(currentUtil.executor.currentAction.name == "collect"){

				ChangeBlinkState(qState_2_collect);
			}
		}else{
			State_No_Target.color = (Color)new Color32(55,66, 77,255);
		}

		if(currentState.name == "LoadTarget"){
			State_Load_Target.color = new Color(0.4f,0.05f, 0.05f);
			if(currentUtil.executor.currentAction.name == "explore"){
				
				ChangeBlinkState(qState_3_explore);
			}else if(currentUtil.executor.currentAction.name == "eat"){

				ChangeBlinkState(qState_3_eat);
			}else if(currentUtil.executor.currentAction.name == "collect"){

				ChangeBlinkState(qState_3_collect);
			}else if(currentUtil.executor.currentAction.name == "trade"){
				
				ChangeBlinkState(qState_3_trade);
			}
		}else{
			State_Load_Target.color = (Color)new Color32(55,66, 77,255);
		}

		if(currentState.name == "LoadNoTarget"){
			State_Load_NoTarget.color = new Color(0.4f,0.05f, 0.05f);
			if(currentUtil.executor.currentAction.name == "explore"){
				
				ChangeBlinkState(qState_4_explore);
			}else if(currentUtil.executor.currentAction.name == "eat"){

				ChangeBlinkState(qState_4_eat);
			}else if(currentUtil.executor.currentAction.name == "trade"){
				
				ChangeBlinkState(qState_4_trade);
			}
		}else{
			State_Load_NoTarget.color = (Color)new Color32(55,66, 77,255);
		}
	}

	private void ChangeBlinkState(Image newState){

		oldBlinkState = currentBlinkState;
		currentBlinkState = newState;
	}

	private void BlinkAnimation(){

		if(oldBlinkState != currentBlinkState){

			oldBlinkState.color = new Color(0, 204, 204, 255);
		}
		timer += Time.deltaTime;
		if(timer > waitingTime){
			ChangeBlinkColor(currentBlinkState);
			timer = 0;
		}
	}

	private void ChangeBlinkColor(Image uiElem){

		uiElem.color = newColor;

		//Change the colors
		tempColor = oldColor;
		oldColor = newColor;
		newColor = tempColor;
	}
}
