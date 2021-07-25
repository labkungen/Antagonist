using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthill : MonoBehaviour
{
    //pointers
    //private GameManager gameManager;

    public GameObject antPrefab;
    //Material teamMaterial;
    public Color teamColor;// = new Color(0.27f, 0.03f, 0.04f, 1f);

    //locations
    public Vector3 entrancePosition; // { get; } 

    [SerializeField] private int antCount = 0;
    private float startDelay = 1.0f;
    private float spawnInterval = 1.0f;

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
            GameObject newAnt = Instantiate(antPrefab, entrancePosition, antPrefab.transform.rotation);
            antCount++;

            newAnt.GetComponent<Ant>().homeAnthill = gameObject;
            newAnt.GetComponent<Renderer>().material.color = teamColor;
        }
        
    }

}
