using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GetFood : MonoBehaviour {

    MovementController movController;

    public float visionRadius = 2f;    
    public Collider2D food;

    //this is true when we have seen some food and are on our way to get it, otherwise its false
    private bool foodFound = false;

    //Call this callback when getting the food is finished
    Action cbActionIsDone;

    void Start(){

        movController = GetComponent<MovementController>();
        movController.RegisterTargetReachedCallback(OnTargetReached);
    }
    
    void FixedUpdate(){
        
        
        if(food != null && foodFound == false){
            setFoodDestination();
            foodFound = true;
        }else{
            food = Physics2D.OverlapCircle(this.transform.position, visionRadius, LayerMask.GetMask("FoodLayer"));
        }
    }

    void setFoodDestination(){

        movController.setNewDestination(food.transform);
    }

    //TODO: Set the foodFound back to false after food is collected
    private void OnTargetReached(){

        if(this.enabled == false){
            Debug.Log("In Action_GetFood OnTargetReached");
			return;
		}
        //TODO: Eat the food
        Destroy(food.gameObject);

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
}
