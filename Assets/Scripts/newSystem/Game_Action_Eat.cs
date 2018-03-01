using System;
using UnityEngine;

public class Game_Action_Eat : MonoBehaviour
{   
    private Game_Util util;
    private RL_QLerner qLerner;

    //time to delay the eating
    //private float timer = 0.0f;
    //private float waitingTime = 3f;

    void Start(){

        util = GetComponent<Game_Util>();
        qLerner = GetComponent<RL_QLerner>();
    }

    public void Act()
    {      
            CleanUp();
    }

    public void Reset()
    {
        
    }

    public void CleanUp(){

        //we need 2 qStates and one action
        RL_State updateState = util.state_manager.oldState;
        RL_QState updateQState = qLerner.qStateFromState[updateState.name];

        RL_State destState = util.state_manager.CurrentState;
        RL_QState destQState = qLerner.qStateFromState[destState.name];

        RL_Action currentAction = util.executor.currentAction;

        //FIXME: For now we set the reward here
        currentAction.reward = -1;

        //Update the qState that you were comeing from
        updateQState.UpdateQState(currentAction, destQState);

        //FIXME: For now we just leave this action immediatly
        util.executor.hasAction = false;
    }
}