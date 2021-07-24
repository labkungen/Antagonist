using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //game world information
    float gameWorldLimit = 100.0f;

    //prefabs
    public GameObject foodPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //create anthills

        //spawn starting food
        SpawnFoodItems(1000);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnFoodItems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(foodPrefab, RandomGameWorldLocation(), foodPrefab.transform.rotation);
        }
    }

    public Vector3 RandomGameWorldLocation()
    {
        return new Vector3(Random.Range(-gameWorldLimit, gameWorldLimit), 0, Random.Range(-gameWorldLimit, gameWorldLimit));
    }

}
