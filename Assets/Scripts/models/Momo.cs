using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Momo{

	int livePoints = 100;

	//We use an arrayList so we can use different types of ressources
	ArrayList ressources = new ArrayList();
	
	//for now we only care about the number of the ressources,
	//this might change
	Action<Momo, int> cbRessourcesChanged;

	public void addNewRessource(System.Object res){

		if(res.GetType() == typeof(Food)){

			ressources.Add((Food)res);
			Debug.Log("I have some food");
			if(cbRessourcesChanged != null){
				//So far the MomoSpriteCOntroller is listening
				cbRessourcesChanged(this, ressources.Count);
			}
		}
	}

	public void RegisterCbRessourcesChanged(Action<Momo, int> callbackFunc){

		cbRessourcesChanged += callbackFunc;
	}

	public void UnRegisterCbRessourcesChanged(Action<Momo, int> callbackFunc){

		cbRessourcesChanged -= callbackFunc;
	}

	public int GetRessourceCount(){

		return ressources.Count;
	}
}
