using System;
using UnityEngine;

public class Game_Action_Collect : MonoBehaviour{

    public float targetThreshold = 0.4f;

    private Game_Util util;
    private RL_QLerner qLerner;

    void Start(){

        util = GetComponent<Game_Util>();
        qLerner = GetComponent<RL_QLerner>();
    }

    public void Act(){

        if(util.foodFinder.foodTarget == null && util.foodFinder.colliders.Length > 0){
            util.foodFinder.foodTarget = ChooseFood();

            //TODO: Remove Log
            string message = util.foodFinder.foodTarget.transform.position.ToString();
            LogController.Instance.AddLogMessage(this.transform.gameObject, "Next Food destination: " + message);

            message = util.foodFinder.colliders.Length.ToString();
            LogController.Instance.AddLogMessage(this.transform.gameObject, "***See " + message + " Food just after choosing Food");

            util.aiPath.target = util.foodFinder.foodTarget.transform;

            message = util.foodFinder.colliders.Length.ToString();
            LogController.Instance.AddLogMessage(this.transform.gameObject, "***See " + message + " Food after setting target");
        }

        if(util.aiPath.target != null){

            //check if you allready reached the ressource
            //Debug.Log("Target position: " + util.aiPath.target.position.ToString());
            //Debug.Log("Momo position: " + util.transform.position.ToString());
            if((util.aiPath.target.position - util.transform.position).magnitude < targetThreshold ){

                //TODO: Remove Log
                LogController.Instance.AddLogMessage(this.transform.gameObject, "Reached some food");
                string message = util.foodFinder.foodTarget.transform.position.ToString();
                LogController.Instance.AddLogMessage(this.transform.gameObject, "Food Position: " + message);
                //The food finder knows about the current food
                Food currFood = WorldController.Instance.getFoodfromGo(util.foodFinder.foodTarget);

                //TODO: Remove Log
                message = util.myMomo.GetRessourceCount().ToString();
                LogController.Instance.AddLogMessage(this.transform.gameObject, "RessourceCount before: " + message);
                
                //now add the food to the ressources of this Momo
                util.myMomo.addNewRessource(currFood);

                //TODO: Remove Log
                message = util.myMomo.GetRessourceCount().ToString();
                LogController.Instance.AddLogMessage(this.transform.gameObject, "RessourceCount after: " + message);
                util.destroyGameObject(util.foodFinder.foodTarget);

                util.state_manager.CheckTransitionsExtra();

                CleanUp();
            }
        }
    }

    private void CleanUp(){

        //TODO: Remove Log
        LogController.Instance.AddLogMessage(this.transform.gameObject, "Cleanup running");

        //we need 2 qStates and one action
        RL_State updateState = util.state_manager.oldState;
        //Debug.Log("<b><color=blue>oldState: </color></b>" + updateState.name);
        RL_QState updateQState = qLerner.qStateFromState[updateState.name];

        RL_State destState = util.state_manager.CurrentState;
        //Debug.Log("<b><color=blue>destState: </color></b>" + destState.name);
        RL_QState destQState = qLerner.qStateFromState[destState.name];

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

    public void Reset(){

    }
}