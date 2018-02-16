
using System.Collections.Generic;
using UnityEngine;

public class RL_Simulator{

    public RL_Simulator(){

    }

    public void SimulateAction(RL_Action action){

        if(action.name == "explore"){

            SimulateExplore(action);
        }else if(action.name == "collect"){

            SimulateCollect(action);
        }else if(action.name == "eat"){

            SimulateEat(action);
        }else if(action.name == "trade"){

            SimulateTrade(action);
        }
    }    

    private void SimulateExplore(RL_Action action){

        Debug.Log("Executing Explore...");

        action.reward = -0.1f;
    }

    private void SimulateCollect(RL_Action action){

        Debug.Log("Executing Collect...");

        action.reward = -0.1f;

    }

    private void SimulateEat(RL_Action action){

        Debug.Log("Executing Eat...");

        //Eating should have a negative reward
        action.reward = -1f;
    }

    private void SimulateTrade(RL_Action action){

        Debug.Log("Executing Trade...");

        //Simulate the trade by choosing some Random value between 0 and 10
        int tradeValue = Random.Range(1,11);
        action.reward = tradeValue;
    }


}