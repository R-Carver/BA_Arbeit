using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class World{

	//World size. HAS TO MATCH WITH THE SIZE OF THE ASTAR AREA
	public float Width{get; protected set;}
	public float Height{get; protected set;}

	public List<Food> food;

	private int momoCount;
	public Momo[] theMomos {get; protected set;}

	public World(int width, int height, int momoCount){
		
		//FIXME: This doenst currently work because the worldcontroller creates the world
		//before the Astar grid is created
		//Check is the dimension respect the dimensions of the Pathfinding grid
		/*if(checkGridDimensions(Width, Height) == false){
			Debug.LogError("World dimensions dont match Grid dimensions");
			return;
		}*/
		this.Width = width;
		this.Height = height;
		this.food = new List<Food>();

		SetMomoCount(momoCount);
		theMomos = new Momo[momoCount];

		GenerateMomos();
		GenerateFood();
	}

	private void GenerateMomos(){

		for (int i = 0; i < momoCount; i++)
		{
			theMomos[i] = new Momo();
		}
	}

	private void GenerateFood(){
	
		//Generate food such that there is plenty of cheap food in the
		//spwan area, but sparse valuable food. The further you get out from
		//the spawn area the less food you find in general but the bigger the
		//chance to find valuable food

		//this is in invesre proportion to the distance of the midpoint of the map
		float foodProb;
		Vector2 midpoint = new Vector2(Width/2, Height/2);
		Vector2 currPoint;

		//this must be equal to the biggest possible distance form a point to the midpoint
		float probRange = Mathf.Sqrt(Mathf.Pow(Width/2, 2) + Mathf.Pow(Height/2, 2));

		Debug.Log("Midpoint: " + midpoint);
		Debug.Log("probRange: " + probRange);

		for (int x = 1; x < Width-1; x++) {
			for (int y = 1; y < Height-1; y++)
			{	
				
				//Calculate the distance from the current point to the midpoint
				currPoint = new Vector2(x, y);
				foodProb = Mathf.Abs((midpoint - currPoint).magnitude);

				//FIXME: For now to balance the food generation adjust here
				float balancer = Random.Range(1f, 512f);

				if(Random.Range(0f,probRange) > (/*foodProb*/1 * balancer))
				{

					//dont generate food inside the spawn area
					//To prevent to many ressources close to the trade Post we make the
					//area on which no ressources will be generated bigger than the spawn area
					if(foodProb < Mathf.Sqrt(Mathf.Pow(12f/2, 2) + Mathf.Pow(12f/2, 2)))
						continue;
					
					int foodValue = determineFoodValue(foodProb, probRange);
					food.Add(new Food(x, y, foodValue));
				}
			}
		}
	}

	int determineFoodValue(float distanceToMid, float range){

		//Use some random value to distribute the values, otherwisr
		//it would be exactly proportional to the distance which would
		//seem a bit to mechanic
		float distributor = (Random.Range(0f, 2f));

		float newDistance = distanceToMid * distributor;
		float newRange = range * distributor;

		//Divide the range into 3 Parts
		if(newDistance < range * 0.33){

			//generate cheap food
			return 1;
		
		//0.66 is about 2/3
		}else if(newDistance >= range * 0.33 && newDistance <= range * 0.66){

			return 3;
		}else{ 

			return 5;
		}

	}

	//check if the dimensions of the Astar Grid match the Width and Height values
	bool checkGridDimensions(int Width, int Height){

		/*AstarData data = AstarPath.active.data;
		GridGraph graph = data.gridGraph;

		//Debug.Log("This is the graph size:" + graph.size);
		Vector2 dimensions = graph.size;
		float gridWidth = dimensions.x;
		float gridHeight = dimensions.y;

		if(gridWidth == Width && gridHeight == Height){
			return true;
		}*/
		return false;
	}

	public void SetMomoCount(int number){

		if(number < 1 || number > 8){

			Debug.Log("Cannot set less than 1 or more than 8 Momos for now");
		}
		this.momoCount = number;
		Debug.Log("MomoCount set with: " + number);
	}
}
