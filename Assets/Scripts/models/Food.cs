using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food{

	public enum FoodSort {red, green};

	public float XPos{get; protected set;}
	public float YPos{get; protected set;}

	//FIXME: This is currently used as the reward in Q-Learning
	int value;

	public FoodSort sort{get; protected set;}

	public Food(float x, float y, int value, FoodSort sort){

		this.XPos = x;
		this.YPos = y;
		this.value = value;
		this.sort = sort;
	}

	public int getReward(){

		return this.value;
	}
}
