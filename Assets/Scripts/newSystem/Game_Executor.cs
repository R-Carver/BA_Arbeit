
using UnityEngine;

public class Game_Executor : MonoBehaviour{

    private RL_QLerner qLerner;

    public bool hasAction = false;
    public RL_Action currentAction{get; protected set;}

    Game_Action_Explore action_explore;
    Game_Action_Collect action_collect;
    Game_Action_TradeWithBase action_trade;
    Game_Action_Eat action_eat;

    void Start(){

        qLerner = RL_QLerner.Instance;

        action_explore = new Game_Action_Explore();
        action_collect = new Game_Action_Collect();
        action_trade = new Game_Action_TradeWithBase();
        action_eat = new Game_Action_Eat();
    }

    void Update(){

        if(hasAction == false ){
            //ResetActions();
            ChooseAction();
        }
        ExecuteAction();
        
    }

    private void ChooseAction(){
        currentAction = qLerner.chooseAction(qLerner.currentState);
        hasAction = true;
    }

    private void ExecuteAction(){

        if(currentAction != null){
            if(currentAction.name == "explore"){
                action_explore.Act();
            }

            if(currentAction.name == "collect"){
                action_collect.Act();
            }

            if(currentAction.name == "trade"){
                action_trade.Act();
            }

            if(currentAction.name == "eat"){
                action_eat.Act();
            }
        }
    }

    public void ResetActions(){

        //Reset Actions if necessary. Its necesarry for example for explore to reset the
        //target. Its not necessary for trade for example
        if(currentAction != null){
            if(currentAction.name == "explore"){
                action_explore.CleanUp();
                action_explore.Reset();
            }

            if(currentAction.name == "collect"){
                action_collect.Reset();
            }

            if(currentAction.name == "trade"){
                action_trade.Reset();
            }
        }
        
    }


}