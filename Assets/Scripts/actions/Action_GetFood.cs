using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GetFood : MonoBehaviour {

    MovementController movController;
    QLearner qLearner;

    public float visionRadius = 2f;    
    public Collider2D food;

    //The prefab for the bad food
    public Transform badFood;

    //this is true when we have seen some food and are on our way to get it, otherwise its false
    private bool foodFound = false;

    //Call this callback when getting the food is finished
    Action cbActionIsDone;

    void Start(){

        movController = GetComponent<MovementController>();
        movController.RegisterTargetReachedCallback(OnTargetReached);

        qLearner = new QLearner();
        qLearner.InitializeQLearner();
    }
    
    void FixedUpdate(){
        
        
        if(food != null && foodFound == false){
            setFoodDestination();
            foodFound = true;
        }else{
            //We dont set the food here anymore but instead we set it in the movementController
            //food = Physics2D.OverlapCircle(this.transform.position, visionRadius, LayerMask.GetMask("FoodLayer"));
        }
    }

    void setFoodDestination(){

        movController.setNewDestination(food.transform);
    }

    private void OnTargetReached(){

        if(this.enabled == false){
            Debug.Log("In Action_GetFood OnTargetReached");
			return;
		}


        //here comes the RL Part ====================================================================
        
        //Get the Food data
        Food foodData = WorldController.Instance.getFoodfromGo(food.gameObject);
        
        if(qLearner.getEstimatedAction(foodData) == QLearner.Action.eat){

            //Eat the food
            Destroy(food.gameObject);
        }else{

            //Mark the food as not nice (for now a black dot)
            Instantiate(badFood, food.transform.position, Quaternion.identity);
            Destroy(food.gameObject);
            //The food should be set to a different Layer, so its not again searched afterwards
        }

        //===========================================================================================

        //For now just mark that the action is done 
        foodFound = false;
        cbActionIsDone();
        Debug.Log("Action Get Food is done");
    }

    public void RegisterActionIsDoneCallback(Action callback){
        cbActionIsDone += callback;
    }

    public void UnRegisterActionIsDoneCallback(Action callback){
        cbActionIsDone-= callback;
    }

    //Just for debugging
    public QLearner getQLearner(){

        return this.qLearner;
    }
}
