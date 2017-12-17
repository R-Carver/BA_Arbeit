
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TradeGame/Decisions/CollectRessource")]
public class CollectRessourceDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if(controller.foodFinder.colliders.Length > 0){

            return true;
        }
        return false;
    }
}