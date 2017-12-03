using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class World{

	//World size. HAS TO MATCH WITH THE SIZE OF THE ASTAR AREA
	public float Width{get; protected set;}
	public float Height{get; protected set;}

	public List<Food> food;

	Momo theMomo;

	public World(int Width = 30, int Height = 30){
		
		//FIXME This doenst currently work because the worldcontroller creates the world
		//before the Astar grid is created
		//Check is the dimension respect the dimensions of the Pathfinding grid
		/*if(checkGridDimensions(Width, Height) == false){
			Debug.LogError("World dimensions dont match Grid dimensions");
			return;
		}*/
		this.Width = Width;
		this.Height = Height;
		this.food = new List<Food>();

		this.theMomo = new Momo();
		GenerateFood();
	}

	private void GenerateFood(){

		//offset by one to the middle because the pathfinding grid doesnt touch the
		//borders
		for (var x = 1; x < Width-1; x++) {
			for (int y = 1; y < Height-1; y++)
			{	

				if(Random.Range(0f,1f) > 0.87f)
				{
					//Choose random sort of generated food
					if(Random.Range(0,2) > 0){

						food.Add(new Food(x, y, 1, Food.FoodSort.green));
					}else{

						food.Add(new Food(x, y, 0, Food.FoodSort.red));
					}
					
				}
					
			}
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
}
