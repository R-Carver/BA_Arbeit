using System;
using UnityEngine;

public class Game_Action_Collect : Game_Action_Abstract{

    public float targetThreshold = 0.4f;

    private Game_Util util;

    public Game_Action_Collect(){

        util = Game_Util.Instance;
    }

    public override void Act(){

        if(util.foodFinder.foodTarget == null && util.foodFinder.colliders.Length > 0){
            util.foodFinder.foodTarget = ChooseFood();
            util.aiPath.target = util.foodFinder.foodTarget.transform;
        }

        if(util.aiPath.target != null){

            //check if you allready reached the ressource
            //Debug.Log("Target position: " + util.aiPath.target.position.ToString());
            //Debug.Log("Momo position: " + util.transform.position.ToString());
            if((util.aiPath.target.position - util.transform.position).magnitude < targetThreshold ){

                //The food finder knows about the current food
                Food currFood = WorldController.Instance.getFoodfromGo(util.foodFinder.foodTarget);

                //now add the food to the ressources of this Momo
                util.myMomo.addNewRessource(currFood);
                util.destroyGameObject(util.foodFinder.foodTarget);

                util.state_manager.CheckTransitionsExtra();

                CleanUp();
            }
        }
    }

    private void CleanUp(){

        //we need 2 qStates and one action
        RL_State updateState = util.state_manager.oldState;
        Debug.Log("<b><color=blue>oldState: </color></b>" + updateState.name);
        RL_QState updateQState = RL_QLerner.Instance.qStateFromState[updateState.name];

        RL_State destState = util.state_manager.CurrentState;
        Debug.Log("<b><color=blue>destState: </color></b>" + destState.name);
        RL_QState destQState = RL_QLerner.Instance.qStateFromState[destState.name];

        RL_Action currentAction = util.executor.currentAction;

        //Update the qState that you were comeing from
        updateQState.UpdateQState(currentAction, destQState);

        //Do we have to set the target to null in order to get to the next ressource?
        util.aiPath.target = null;
        util.executor.hasAction = false;
    }

    private GameObject ChooseFood(){

        if(util.foodFinder.colliders.Length > 0){
            int foodIndex = UnityEngine.Random.Range(0, util.foodFinder.colliders.Length);
            if(util.foodFinder.colliders[foodIndex] != null)
                return util.foodFinder.colliders[foodIndex].gameObject;
        }
        return null;
    }

    public override void Reset(){

    }
}