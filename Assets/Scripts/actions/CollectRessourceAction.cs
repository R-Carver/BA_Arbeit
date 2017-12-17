using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TradingGame/Actions/CollectRessource")]
public class CollectRessourceAction : Action
{
    public override void Act(StateController controller)
    {   
        if(controller.foodFinder.foodTarget == null){
            controller.foodFinder.foodTarget = ChooseFood(controller);
            controller.myAIPath.target = controller.foodFinder.foodTarget.transform;
        }
        

        //TODO:Add the food to the players "inventory" and adjust the sprites

        //TODO: Remove the food Go from the world since it is in the player cart
    }

    public override void CleanUp(StateController controller)
    {
        
    }

    private GameObject ChooseFood(StateController ctr){

        //Randomly choose one of the food objects that is currently visible
        //FIXME: For now we assume the colliders are only food objects
        int foodIndex = UnityEngine.Random.Range(0,ctr.foodFinder.colliders.Length);
        return ctr.foodFinder.colliders[foodIndex].gameObject;
    }
}