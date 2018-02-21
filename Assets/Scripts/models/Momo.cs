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

	private int tradeValue = 0;

	public void addNewRessource(System.Object res){

		Food currentFood = null;
		if(res.GetType() == typeof(Food)){

			currentFood = (Food)res;

			ressources.Add((currentFood));
			Debug.Log("I have some food");
			if(cbRessourcesChanged != null){
				//So far the MomoSpriteCOntroller is listening
				cbRessourcesChanged(this, ressources.Count);
			}
		}

		if(currentFood != null){

			//add the value of the ressource to the total of this momo
			tradeValue += currentFood.getValue();
		}
	}

	//returns the Trade Value of this momo
	public int SellRessources(){

		int sellValue = tradeValue;

		//remove the ressources
		ressources.Clear();
		if(cbRessourcesChanged != null){
				//So far the MomoSpriteCOntroller is listening
				cbRessourcesChanged(this, ressources.Count);
		}

		//set the total value to 0
		tradeValue = 0;

		//return the trade value
		return sellValue; 
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

	public int getTradeValue(){

		return this.tradeValue;
	}
}
