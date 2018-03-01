using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Momo{

	int livePoints = 100;
	string id;
	public bool chosen = false;

	//We use an arrayList so we can use different types of ressources
	ArrayList ressources = new ArrayList();
	
	//for now we only care about the number of the ressources,
	//this might change
	Action<Momo, int> cbRessourcesChanged;

	//this is the just the value of the current resources
	private int tradeValue = 0;

	//this is the value of all the money made so far
	private int _totalTradeValue = 0;
	public int TotalTradeValue{

		get{ return _totalTradeValue;}
		set{
			_totalTradeValue = value;

			if(cbTotalChanged != null){
				cbTotalChanged(this);
			}
		}
	}

	Action<Momo> cbTotalChanged;
	

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

		//Update the player total
		if(chosen == true){
			GameController.Instance.playerTotal += sellValue;
		}
	
		if(cbRessourcesChanged != null){
				//So far the MomoSpriteCOntroller is listening
				cbRessourcesChanged(this, ressources.Count);
		}

		//Update the total
		TotalTradeValue += tradeValue;

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

	public void RegisterCbTotalChanged(Action<Momo> callback){

		cbTotalChanged += callback;
	}

	public void UnRegisterCbTotalChanged(Action<Momo> callback){

		cbTotalChanged -= callback;
	}

	public int GetRessourceCount(){

		return ressources.Count;
	}

	public int getTradeValue(){

		return this.tradeValue;
	}

	public void SetId(string id){

		this.id = id;
	}

	public string GetId(){

		if(this.id != null){

			return id;
		}
		return null;
	}
}
