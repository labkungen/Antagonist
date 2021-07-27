using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE

public class WorkerAnt : Ant
{
    

    protected override void ResolveContactWithFood(Collider other)
    {
        harvestingFoodAtLocation = other.gameObject.transform.position;
        hasAFoodSource = true;
        isAtFoodSource = true;

        //do stuff with food
        other.gameObject.GetComponent<Food>().ReduceFoodItemByPortion(10);

        //back to stack
        targetLocation = homeAnthill.GetComponent<Anthill>().entrancePosition;
        isAtFoodSource = false;
        isReturningFromMission = true;
    }

    protected override void ResolveContactWithAnt(Collider other)
    {
        if (!hasAFoodSource && other.GetComponent<Ant>().hasAFoodSource &&
            GameObject.ReferenceEquals(other.gameObject.GetComponent<WorkerAnt>().homeAnthill,homeAnthill))
        {
            hasAFoodSource = true;
            harvestingFoodAtLocation = other.GetComponent<Ant>().harvestingFoodAtLocation;
            targetLocation = harvestingFoodAtLocation;
        }
    }

    protected override void ResolveContactWithAnthill(Collider other)
    {
        if (!hasAFoodSource && GameObject.ReferenceEquals(other, homeAnthill))
        {
            //hasAFoodSource = false;  //Todo: Set elsewhere but should be here 
            other.GetComponent<Anthill>().AddToListOfKnownFoodsources(harvestingFoodAtLocation);
            other.GetComponent<Anthill>().StoreFood(10);
        }

        if (!GameObject.ReferenceEquals(other.gameObject, homeAnthill))
        {
            ChangeAntToFood();
        }
    }

}
