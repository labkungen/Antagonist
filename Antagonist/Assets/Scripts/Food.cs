using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int nutrition { get; }
    [SerializeField] private int amount = 100;
    private int currentAmount;

    private void Awake()
    {
        RefreshItems();
    }

    public virtual void ReduceFoodItemByPortion(int portion)
    {
        currentAmount -= portion;

        if (currentAmount <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void RefreshItems()
    {
        currentAmount = amount;
    }
}
