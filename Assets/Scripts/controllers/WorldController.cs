using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public static WorldController Instance{ get; protected set;}

	public World world{get; protected set;}

	public Transform foodPrefab;
	public Transform momoPrefab;
	public Transform[] spawnPoints;
	public Transform tradePost;

	Dictionary<GameObject, Food> foodGameObjects;
	Dictionary<GameObject, Momo> momoGameObjects;
	Dictionary<Momo, Transform> momoSelectioncircleMap;
	//the second dictionary is for example needed in the MomoSpriteController
	Dictionary<Momo, GameObject> momoGameObjectMap;

	private Queue<Transform> spawnQueue;

	private GameController gameController;

	void OnEnable(){
		if(Instance != null){
			Debug.LogError("There should never be more than one world controller");
		}
		Instance = this;

		gameController = GetComponent<GameController>();

		world = new World(gameController.mapWidth, gameController.mapHeight, gameController.momoCount);
	}

	// Use this for initialization
	void Start () {

		foodGameObjects = new Dictionary<GameObject, Food>();
		momoGameObjects = new Dictionary<GameObject, Momo>();
		momoGameObjectMap = new Dictionary<Momo, GameObject>();
		momoSelectioncircleMap = new Dictionary<Momo, Transform>();

		InitSpawnQueue();
		InitFood();
		InitMomos();
		InitSelectionCircles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void InitSelectionCircles(){

		foreach(Momo momo in world.theMomos){

			GameObject momoGo = getGoFromMomo(momo);
			Transform selectionCircle = momoGo.transform.GetChild(2);

			if(selectionCircle.gameObject.name != "SelectionCircle"){

				Debug.Log("Something is wrong with the order of the SelectionCircle, did you add another Object as Child of the momos");
			}else{

				momoSelectioncircleMap.Add(momo, selectionCircle);
			}
		}
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

	private void InitMomos(){

		foreach(Momo momo in world.theMomos ){

			//grab the first spawn point
			Transform currSpawnPoint = spawnQueue.Dequeue();

			//Instantiate the GO
			Transform momoTransform = Instantiate(momoPrefab, currSpawnPoint.position, Quaternion.identity);
			GameObject goMomo = momoTransform.gameObject;

			goMomo.name = "Momo";

			momoGameObjects.Add(goMomo, momo);
			momoGameObjectMap.Add(momo, goMomo);

			//Enqueue the spawnPoint back to the queue so it can be reused
			spawnQueue.Enqueue(currSpawnPoint);
		}
	}

	public Food getFoodfromGo(GameObject food_go){

		return foodGameObjects[food_go];
	}

	public Momo getMomoFromGo(GameObject momo_go){

		return momoGameObjects[momo_go];
	}

	public GameObject getGoFromMomo(Momo momo){

		return momoGameObjectMap[momo];
	}

	public Transform GetCircleFromMomo(Momo momo){

		return momoSelectioncircleMap[momo];
	}

	public GameController GetGameController(){

		return this.gameController;
	}	

	//Makes a Queue out of the spawnPoints, so they can be used and then
	//appended back to the queue, when a new character wants to spawn
	private void InitSpawnQueue(){

		spawnQueue = new Queue<Transform>();

		foreach(Transform spawnArea in spawnPoints){

			spawnQueue.Enqueue(spawnArea);
		}
	}
}
