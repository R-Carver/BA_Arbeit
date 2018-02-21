
using System;
using UnityEngine;

public class Game_Action_Explore : Game_Action_Abstract
{     
    public float targetThreshold = 0.4f;

    private Game_Util util;
    private bool hasTarget = false;

    public Game_Action_Explore(){

        util = Game_Util.Instance;
        
    }

    public override void Act()
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

        Debug.Log("Set Destination called");
        //set destination to something in the range of the explorable area
        float targetX = (float)UnityEngine.Random.Range(0, WorldController.Instance.world.Width);
        float targetY = (float)UnityEngine.Random.Range(0, WorldController.Instance.world.Height);

        GameObject tempGo = new GameObject();
        tempGo.transform.position = new Vector3(targetX, targetY, 0);
        util.aiPath.target = tempGo.transform;
    }

    public override void Reset(){
        
        util.aiPath.target = null;
        hasTarget = false;
    }

    public void CleanUp(){

        //Update the qState
        if(util.state_manager.oldState != null){

                //we need 2 qStates and one action
                RL_State updateState = util.state_manager.oldState;
                RL_QState updateQState = RL_QLerner.Instance.qStateFromState[updateState.name];

                RL_State destState = util.state_manager.CurrentState;
                RL_QState destQState = RL_QLerner.Instance.qStateFromState[destState.name];

                RL_Action currentAction = util.executor.currentAction;

                //Update the qState that you were comeing from
                updateQState.UpdateQState(currentAction, destQState);
        }

        hasTarget = false;
    }
}