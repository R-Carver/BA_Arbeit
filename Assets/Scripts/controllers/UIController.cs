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

	StateController stateCtr;
	
	// Use this for initialization
	void Start () {

		initializeTextFields();

		//register Listeners

		Invoke("GetAgent", 0.5f);
		Invoke("GetScriptsForDebug", 0.5f);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//UpdateDebug1();
		UpdateDebug2();
		//UpdateDebug3();
		//UpdateDebug4();
		//UpdateDebug5();
		//UpdateDebug6();
		//UpdateDebug7();
		//UpdateDebug8();
		//UpdateDebug9();
	}

//Update the text fields ==============================================================================

	private void UpdateDebug1(){


	}

	private void UpdateDebug2(){

		//if(stateCtr.currentState != null){
		if(stateCtr != null){
			debug2.text = stateCtr.currentState.ToString();
		}
	}

	private void UpdateDebug3(){

	}

	private void UpdateDebug4(){

	}

	private void UpdateDebug5(){

	}

	private void UpdateDebug6(){

	}

	private void UpdateDebug7(){

	}

	private void UpdateDebug8(){

	}

	private void UpdateDebug9(){

	}

//=============================================================================================

	private void GetAgent(){

		agent = GameObject.Find("Momo");
	}

	private void GetScriptsForDebug(){

		stateCtr = agent.GetComponent<StateController>();
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
