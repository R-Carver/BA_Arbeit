using System;
using UnityEngine;

public class Game_Action_Eat : Game_Action_Abstract
{   
    private Game_Util util;

    //time to delay the eating
    //private float timer = 0.0f;
    //private float waitingTime = 3f;

    public Game_Action_Eat(){

        util = Game_Util.Instance;
    }

    public override void Act()
    {      
            CleanUp();
    }

    public override void Reset()
    {
        
    }

    public void CleanUp(){

        //we need 2 qStates and one action
        RL_State updateState = util.state_manager.oldState;
        RL_QState updateQState = RL_QLerner.Instance.qStateFromState[updateState.name];

        RL_State destState = util.state_manager.CurrentState;
        RL_QState destQState = RL_QLerner.Instance.qStateFromState[destState.name];

        RL_Action currentAction = util.executor.currentAction;

        //FIXME: For now we set the reward here
        currentAction.reward = -1;

        //Update the qState that you were comeing from
        updateQState.UpdateQState(currentAction, destQState);

        //FIXME: For now we just leave this action immediatly
        util.executor.hasAction = false;
    }
}