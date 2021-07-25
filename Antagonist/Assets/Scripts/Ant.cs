using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    //identity data
    public GameObject homeAnthill;
    //private GameManager gameManager;

    //limitations
    private float worldRange = 100f;
   
    [SerializeField] private float movementSpeed;
    [SerializeField] private int hitpoints;


    //activities
    Vector3 targetLocation;
    bool isInHive;
    bool isReturningFromMission;
    public bool hasAFoodSource;
    private bool isAtFoodSource;
    public Vector3 harvestingFoodAtLocation; 

    private void Awake()
    {
        NewRandomTargetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
    }

    //protected abstract void Scout();

    public virtual void Move()
    {
       
        float step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);

        if (transform.position.y != 0.15)
        {
            
            
        }

        //check reached location
        if (Vector3.Distance(transform.position, targetLocation) < 0.001f)
        {
            if (!hasAFoodSource || (!isAtFoodSource && !isReturningFromMission))
            {
                NewRandomTargetLocation();
                hasAFoodSource = false;
            }
            else
            {
                targetLocation = harvestingFoodAtLocation;
                isReturningFromMission = false;
            }
            
            
        }
    }

    protected virtual void NewRandomTargetLocation()
    {
        float newRandomX = Random.Range(transform.position.x - 2, transform.position.x + 2);
        float newRandomZ = Random.Range(transform.position.z - 2, transform.position.z + 2);

        //check vs worldborder
        if (newRandomX > worldRange)
        {
            newRandomX = worldRange;
        }

        if (newRandomX < -worldRange)
        {
            newRandomX = -worldRange;
        }

        if(newRandomZ > worldRange)
        {
            newRandomZ = worldRange;
        }

        if (newRandomZ < -worldRange)
        {
            newRandomZ = -worldRange;
        }

        targetLocation = new Vector3(newRandomX, 0.15f, newRandomZ);
        
    }

      protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
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

        if (other.gameObject.CompareTag("Ant"))
        {
            //Todo:check team!!1

            Debug.Log("Ant met ant!");

            if (!hasAFoodSource && other.GetComponent<Ant>().hasAFoodSource)
            {
                hasAFoodSource = true;
                harvestingFoodAtLocation = other.GetComponent<Ant>().harvestingFoodAtLocation;
                targetLocation = harvestingFoodAtLocation;
            }
        }
    }
}
