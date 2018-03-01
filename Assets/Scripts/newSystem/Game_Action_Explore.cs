
using System;
using UnityEngine;

public class Game_Action_Explore : MonoBehaviour
{     
    public float targetThreshold = 0.4f;

    private Game_Util util;
    private RL_QLerner qLerner;
    private bool hasTarget = false;

    private float lowerSearchBoundX = 0;
    private float upperSearchBoundX;
    private float lowerSearchBoundY = 0;
    private float upperSearchBoundY;

    private GameObject tempGo;

    void Start(){

        //Debug.Log("<b><color=lime>Calling Explore Start </color></b>");
        util = GetComponent<Game_Util>();
        //Debug.Log("<b><color=fuchsia>Util in Explore: </color></b>" + util.ToString());
        qLerner = GetComponent<RL_QLerner>();

        upperSearchBoundX = WorldController.Instance.world.Width;
        upperSearchBoundY = WorldController.Instance.world.Height;

    }

    public void Act()
    {
        Explore();
    }

    private void Explore(){

        if(checkTargetReached() == true){

            if(hasTarget == false){
                SetNewDestination();
                hasTarget = true;
            }
            //util.executor.hasAction = false;
        }
    }

    private bool checkTargetReached(){
        //Debug.Log("<b><color=fuchsia>Util in Explore:" + util.ToString());
        if(util.aiPath.target != null){
            //If TargetPosition - CurrentPosition < Threshold
            if((util.aiPath.target.position - util.transform.position).magnitude < targetThreshold){

                //we reached the target
                CleanUp();
                return true;
            }
            return false;
        }
        return true;
    }

    private void SetNewDestination(){

        //Debug.Log("Set Destination called");
        //set destination to something in the range of the explorable area

        //Idea: Only search for coordinates which are further away from the current position
        //this should be reset when another action is chosen, so you can also get back to the
        //center. For example with the trade action
        float targetX = (float)UnityEngine.Random.Range(lowerSearchBoundX, upperSearchBoundX);
        float targetY = (float)UnityEngine.Random.Range(lowerSearchBoundY, upperSearchBoundY);

        if(util.characterPosition.x > 0){
            lowerSearchBoundX = util.characterPosition.x;
        }else{
            upperSearchBoundX = util.characterPosition.x;
        }

        if(util.characterPosition.y > 0){
            lowerSearchBoundY = util.characterPosition.y;
        }else{
            upperSearchBoundY = util.characterPosition.y;
        }

        //We destroy the object which was the destination object so far,
        //because they are not needed any longer and just pollute the inspector
        Destroy(tempGo);
        tempGo = new GameObject();
        tempGo.transform.position = new Vector3(targetX, targetY, 0);
        util.aiPath.target = tempGo.transform;
    }

    public void Reset(){
        
        //this is a hack to fix the bug that sometimes occures that when collecting the state switches
        //to state 1 and explore
        util.foodFinder.foodTarget = null;

        util.aiPath.target = null;
        hasTarget = false;

        ResetBounds();
    }

    private void ResetBounds(){
        
        lowerSearchBoundX = 0;
        upperSearchBoundX = WorldController.Instance.world.Width;
        lowerSearchBoundY = 0;
        upperSearchBoundY = WorldController.Instance.world.Height;
    }

    public void CleanUp(){

        //Update the qState
        if(util.state_manager.oldState != null){

                //we need 2 qStates and one action
                RL_State updateState = util.state_manager.oldState;
                RL_QState updateQState = qLerner.qStateFromState[updateState.name];

                RL_State destState = util.state_manager.CurrentState;
                RL_QState destQState = qLerner.qStateFromState[destState.name];

                RL_Action currentAction = util.executor.currentAction;

                //Update the qState that you were comeing from
                updateQState.UpdateQState(currentAction, destQState);
        }

        hasTarget = false;
    }
}