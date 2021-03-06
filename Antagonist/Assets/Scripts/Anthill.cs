using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthill : MonoBehaviour
{
    //pointers
    //private GameManager gameManager;

    public GameObject antPrefab;
    public GameObject soldierPrefab;

    //Material teamMaterial;
    public Color teamColor;// = new Color(0.27f, 0.03f, 0.04f, 1f);
    public bool isPlayer;
    //locations
    public Vector3 entrancePosition; // { get; }
    private List<Vector3> listOfKnownFoodSources = new List<Vector3>();

    [SerializeField] private int antCount = 0;
    private float startDelay = 1.0f;
    private float spawnInterval = 1.0f;

    private int percentageSoldiers = 15;
    private int storedFood;

    private int anthillStructurePoints = 1000;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   //TODO: Validate need. Not needed atm

        entrancePosition = new Vector3(transform.position.x, 0.15f, transform.position.z);

        InvokeRepeating("SpawnNewAnt", startDelay, spawnInterval);
    }


    private void SpawnNewAnt()
    {
        if (antCount < 20)
        {
            GameObject newAnt;
            antCount++;

            

            int roll = Random.Range(0,100);

            if (roll < percentageSoldiers)
            {
                newAnt = Instantiate(soldierPrefab, entrancePosition, soldierPrefab.transform.rotation);
                newAnt.GetComponent<Ant>().antType = Ant.AntType.Soldier;
                //newAnt.transform.localScale += new Vector3(0.2f,0.2f,0.2f);
            }
            else
            {
                newAnt = Instantiate(antPrefab, entrancePosition, antPrefab.transform.rotation);
                newAnt.GetComponent<Ant>().antType = Ant.AntType.Worker;
            }

            //general settings
            newAnt.GetComponent<Ant>().homeAnthill = gameObject;
            newAnt.GetComponent<Renderer>().material.color = teamColor;
        }
        
    }

    public void ManageStructureDamage()
    {
        anthillStructurePoints -= 10;

        if (anthillStructurePoints < 1)
        {
            if (isPlayer)
            {
                Debug.Log("GAME OVER!");
            }

            //pack upp all the ants
            Ant[] ants = FindObjectsOfType<Ant>();

            foreach (Ant ant in ants)
            {
                if (ReferenceEquals(ant.homeAnthill, gameObject))
                {
                    ant.ChangeAntToFood();
                }
            }

                Destroy(gameObject,1f);  //TODO: Fix!  
        }
    }

    public void StoreFood(int amount)
    {
        storedFood += amount;
    }

    public void ReduceNumberOfAnts(int numberToReduce)
    {
        antCount -= numberToReduce;

        if (antCount < 0)
        {
            antCount = 0;
        }
    }

    public void AddToListOfKnownFoodsources(Vector3 newLocation)   //TODO: How to make this clear when food is gone?
    {
        if (!listOfKnownFoodSources.Contains(newLocation))
        {
            listOfKnownFoodSources.Add(newLocation);
        }
    }

    public string ReportHillStatus()
    {
        return ("Anthill1: Resource: " + storedFood + " " + antCount + "Ants (" + percentageSoldiers + "% soldiers)");
    }

    public void ChangePercentageSoldiers(int amount)
    {
        percentageSoldiers += amount;

        if (percentageSoldiers < 0)
        {
            percentageSoldiers = 0;
        }

        if (percentageSoldiers > 100)
        {
            percentageSoldiers = 100;
        }
    }
}
