using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public static WorldController Instance{ get; protected set;}

	public World world{get; protected set;}

	public Transform foodPrefab;
	public Transform momoPrefab;

	void OnEnable(){
		if(Instance != null){
			Debug.LogError("There should never be more than one world controller");
		}
		Instance = this;

		world = new World();
	}

	// Use this for initialization
	void Start () {
		InitFood();
		InitMomo();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void InitFood(){

		//Create a GO as Parent for the food just for organization
		GameObject foodParent = new GameObject("foodParent");
		foodParent.transform.position = new Vector3(0f, 0f, 0f);

		foreach (Food food in world.food)
		{
			Transform food_go = Instantiate(foodPrefab, new Vector3(food.XPos, food.YPos, 0f), Quaternion.identity);
			food_go.parent = foodParent.transform;
		}
	}

	private void InitMomo(){

		Instantiate(momoPrefab, new Vector3(5,5,0), Quaternion.identity);
	}
}
