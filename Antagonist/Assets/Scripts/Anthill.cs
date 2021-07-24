using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthill : MonoBehaviour
{
    //pointers
    private GameManager gameManager;

    public GameObject antPrefab;

    private float startDelay = 1.0f;
    private float spawnInterval = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   //TODO: Validate need. Not needed atm
        InvokeRepeating("SpawnNewAnt", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNewAnt()
    {
        Instantiate(antPrefab, transform.position, antPrefab.transform.rotation);
    }

}
