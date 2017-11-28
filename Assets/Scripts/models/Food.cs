using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food{

	public float XPos{get; protected set;}
	public float YPos{get; protected set;}

	int value;

	public Food(float x, float y, int value){

		this.XPos = x;
		this.YPos = y;
		this.value = value;
	}
}
