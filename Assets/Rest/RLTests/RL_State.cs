using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_State{

    public string name;

    public int actionCount;
    public ArrayList actions;

    public RL_State(string name, int actionCount){

        this.name = name;
        this.actionCount = actionCount;
        actions = new ArrayList();
    }

    public void addAction(RL_Action action){

        actions.Add(action);
    }

    public RL_Action GetActionFromName(string actionName){

        foreach(RL_Action action in actions){
            if(action.name == actionName){
                return action;
            }
        }
        Debug.Log("GetActionFromName did not find a action with this name - RL_State Line 30");
        return null;
    }
}