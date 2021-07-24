using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    //identity data
    public GameObject homeAnthill;
    private GameManager gameManager;

    //limitations
   
    [SerializeField] private float movementSpeed;
    [SerializeField] private int hitpoints;


    //activities
    Vector3 movementVector;
    bool isInHive;
    bool isReturningFromMission;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //set gamemanager?
        //set home anthill
        //isInHive = true;  //all ants wake in hive
        //movementVector =
        NewRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    //protected abstract void Scout();

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        //check for edge of world
        //if()
    }

    protected void NewRandomDirection()
    {
        //return new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        transform.Rotate(new Vector3(0, Random.Range(transform.rotation.y - 15, transform.rotation.y + 15), 0));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Found food! - collision");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("Found food!");

            //found food - stop
            movementVector = Vector3.zero;

            //do stuff with food

            //back to stack
            transform.LookAt(homeAnthill.transform);
        }
    }
}
