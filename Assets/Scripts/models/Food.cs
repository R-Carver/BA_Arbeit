using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food{

	//public enum FoodSort {red, green};
	//public FoodSort sort{get; protected set;}

	public float XPos{get; protected set;}
	public float YPos{get; protected set;}

	//for now this is assumed to be 1, 3, or 5
	int value;

	public Food(float x, float y, int value){

		this.XPos = x;
		this.YPos = y;
		this.value = value;
	}

	public int getValue(){

		return this.value;
	}
}
