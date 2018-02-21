using System;
using UnityEngine;

public class Game_Action_TradeWithBase : Game_Action_Abstract
{   
    private Game_Util util;

    private bool onWayToTradePost = false;

    public Game_Action_TradeWithBase(){

        util = Game_Util.Instance;
    }

    public override void Act()
    {   
        if(onWayToTradePost == false){

            util.aiPath.target = WorldController.Instance.tradePost;
            onWayToTradePost = true;
        }
        
        if(checkTradePostReached() == true){

            //The Trading system for now must be implemented in the CleanUp function because
            //otherwise the momoe gets reset to early

            CleanUp();
        }

    }

    private bool checkTradePostReached(){

        if((WorldController.Instance.tradePost.position - util.transform.position).magnitude < 0.4f){

            //we reached the trade post
            
            return true;
        }
        return false;
    }

    public override void Reset()
    {
        throw new NotImplementedException();
    }

    public void CleanUp(){

        //this should be called after the Momo has sold his gods because its
        //only then that we will change the state to state 0 which is
        //the destination state for the trade action

        //we need 2 qStates and one action
        RL_State updateState = util.state_manager.oldState;
        RL_QState updateQState = RL_QLerner.Instance.qStateFromState[updateState.name];

        RL_State destState = util.state_manager.CurrentState;
        RL_QState destQState = RL_QLerner.Instance.qStateFromState[destState.name];

        RL_Action currentAction = util.executor.currentAction;

        //set the reward to the value of the ressources which the momo has at this moment
        currentAction.reward = util.myMomo.getTradeValue();

        //Update the qState that you were comeing from
        updateQState.UpdateQState(currentAction, destQState);

        //Special Case for State 3:
        //Here we have to manually set the state to the same value as state 4 because
        //the real trading is always executed in state 4
        if(updateState.name == "LoadNoTarget"){

            //then get the 3rd state and set it to the same qValue
            RL_QState state_3 = RL_QLerner.Instance.qStates[2];
            state_3.qValues[currentAction.name] = updateQState.qValues[currentAction.name];
        }

        //TODO: Implement the trading system
        util.myMomo.SellRessources();
        

        util.aiPath.target = null;
        util.executor.hasAction = false;
        onWayToTradePost = false;
    }
}