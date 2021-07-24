using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int nutrition { get; }
    [SerializeField] private int amount = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ReduceFoodItemByPortion(int portion)
    {
        amount -= portion;

        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }


}
