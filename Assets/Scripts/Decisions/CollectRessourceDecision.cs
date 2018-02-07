
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TradeGame/Decisions/CollectRessource")]
public class CollectRessourceDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if(controller.foodFinder.colliders.Length > 0){
            //some ressource is in sight
            return true;
        }
        //no ressource found so explore
        return false;
    }
}