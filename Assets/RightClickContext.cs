using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickContext : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){

		GameObject momoGo = this.transform.parent.gameObject;
		Momo momo = WorldController.Instance.getMomoFromGo(momoGo);

		GameController.Instance.AddSelectedMomo(momo);
	}
}
