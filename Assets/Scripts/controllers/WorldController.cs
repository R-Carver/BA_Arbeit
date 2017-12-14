using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public static WorldController Instance{ get; protected set;}

	public World world{get; protected set;}

	public Transform foodPrefab;
	public Transform momoPrefab;

	Dictionary<GameObject, Food> foodGameObjects;

	void OnEnable(){
		if(Instance != null){
			Debug.LogError("There should never be more than one world controller");
		}
		Instance = this;

		world = new World();
	}

	// Use this for initialization
	void Start () {

		foodGameObjects = new Dictionary<GameObject, Food>();

		InitFood();
		//InitMomo();
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
			Transform food_go;


			food_go = Instantiate(foodPrefab, new Vector3(food.XPos, food.YPos, 0f), Quaternion.identity);
			
			//Adjust the color-alpha to the value of the food
			SpriteRenderer ren = food_go.GetComponent<SpriteRenderer>();
			Color col = Color.yellow;
			col.a = Utility.convertValueIntoAlpha(food.getValue());
			ren.color = col;
			
			//Add the newly created food to the Dictionary
			foodGameObjects.Add(food_go.gameObject, food);
			food_go.parent = foodParent.transform;
		}
	}

	private void InitMomo(){

		Transform momo = Instantiate(momoPrefab, new Vector3(5,5,0), Quaternion.identity);
		GameObject goMomo = momo.gameObject;

		goMomo.name = "Momo";
	}

	public Food getFoodfromGo(GameObject food_go){

		return foodGameObjects[food_go];
	}
}
