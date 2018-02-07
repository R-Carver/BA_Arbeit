using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TradingGame/Actions/TradeWithBase")]
public class TradeWithBaseAction : Action
{
    public override void Act(StateController controller){
        
        //set the destination to the base coordinates
        controller.myAIPath.target = WorldController.Instance.tradePost;
    }

    public override void CleanUp(StateController controller){}
}