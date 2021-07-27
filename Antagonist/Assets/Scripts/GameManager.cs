using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameManager : MonoBehaviour
{
    //game world information
    float gameWorldLimit = 100.0f;

    //GUI
    public TextMeshProUGUI anthill1Text;
    


    //team colors
    Color redTeamColor = new Color(0.27f, 0.03f, 0.04f, 1f);
    Color blueTeamColor = new Color(0.06f, 0.07f, 0.35f, 1f);
    Color greenTeamColor = new Color(0.07f, 0.3f, 0.04f, 1f);
    Color yellowTeamColor = new Color(0.07f, 0.3f, 0.3f, 1f);

    [SerializeField] int numberOfFoodSources = 60;

    //prefabs
    public GameObject foodPrefab;
    public GameObject anthillPrefab;

    //data
    List<Food> listOfInactiveFoods = new List<Food>();
    List<GameObject> anthills = new List<GameObject>();
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //create anthills
        /*for (int i = 0; i < 3; i++)
        {
            SpawnAnthill();
        }*/

        anthills.Add(SpawnAnthill(yellowTeamColor, true));
        anthills.Add(SpawnAnthill(redTeamColor, false));
        anthills.Add(SpawnAnthill(blueTeamColor, false));
        anthills.Add(SpawnAnthill(greenTeamColor,false));
        

        //spawn starting food
        SpawnFoodItems(numberOfFoodSources);

        //Regular update check on world items
        InvokeRepeating("WorldUpdate", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
       /* //enough food?
        if (numberOfFoodSources < 50)
        {
            int numberToAdd = Random.Range(5, 20);
            SpawnFoodItems(numberToAdd);
            numberOfFoodSources += numberToAdd;
        }*/

    }

  

    private GameObject SpawnAnthill(Color teamColor, bool isPlayer)
    {
        GameObject newHill = Instantiate(anthillPrefab, MiniWoldlocation()/*RandomGameWorldLocation()*/, anthillPrefab.transform.rotation);
        newHill.GetComponent<Anthill>().teamColor = teamColor;
        //newHill.GetComponent<Anthill>().antPrefab = FindObjectOfType

        if (isPlayer)
        {
            player = newHill;
        }

        return newHill;
    }

    public void SpawnFoodItems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(foodPrefab, RandomGameWorldLocation(), foodPrefab.transform.rotation);
        }
    }

    private Vector3 MiniWoldlocation()
    {
        return new Vector3(Random.Range(-gameWorldLimit/2, gameWorldLimit/2), 0, Random.Range(-gameWorldLimit/2, gameWorldLimit/2));
    }

    public Vector3 RandomGameWorldLocation()
    {
        return new Vector3(Random.Range(-gameWorldLimit, gameWorldLimit), 0, Random.Range(-gameWorldLimit, gameWorldLimit));
    }

    private void WorldUpdate()
    {
        //check available foodsources
        Food[] foods = FindObjectsOfType<Food>();
        int numberOfActiveFoodSoures = 0;

        //Debug.Log("GM: Counted " + foods.Length + " food items");

        foreach (Food food in foods)
        {
            //Debug.Log("GM: Food is called " + food.name + " and is " + food.gameObject.activeInHierarchy);
            if (food.gameObject.activeInHierarchy == true)
            {
                numberOfActiveFoodSoures++;
            }
            else
            {
                listOfInactiveFoods.Add(food);
            }
        }
        //update active
        numberOfFoodSources = numberOfActiveFoodSoures;
        //Debug.Log("GM checking in #active = " + numberOfActiveFoodSoures);

        //respawn food
        if (numberOfActiveFoodSoures < 50)
        {
            int numberToSpawn = Random.Range(0, listOfInactiveFoods.Count - 1);

            for (int i = 0; i < numberToSpawn; i++)
            {
                Food food = listOfInactiveFoods.Last<Food>();
                food.RefreshItems();
                food.gameObject.SetActive(true);
                food.transform.position = RandomGameWorldLocation();
            }
        }
        //Update GUI
        UpdateGUI();
    }

    public void UpdateGUI()
    {
        anthill1Text.text = player.GetComponent<Anthill>().ReportHillStatus();
    }

    public void QuitButtonPressed()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }

}
