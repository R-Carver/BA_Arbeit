using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TradeGame/Decisions/TradeRessources")]
public class TradeRessourcesDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if(controller.myMomo.GetRessourceCount() >= 4){

            //change to Tradeaction
            return true;
        }
        return false;
    }
}