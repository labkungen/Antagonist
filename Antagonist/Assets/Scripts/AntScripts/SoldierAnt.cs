using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnt : Ant
{
    private GameObject targetOfAttack;
    private int damageOutputMax;

    // Start is called before the first frame update
 /*   void Start()
    {
        NewRandomTargetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }*/

    protected override void ResolveContactWithAnt(Collider other)
    {
        Debug.Log("Soldier ant met other ant (" + homeAnthill.GetInstanceID() + " / " + other.gameObject.GetComponent<Ant>().homeAnthill.GetInstanceID());

        if (!GameObject.ReferenceEquals(other.gameObject.GetComponent<Ant>().homeAnthill,homeAnthill))
        {
            if (other.gameObject.GetComponent<Ant>().antType == Ant.AntType.Worker)
            {
                Debug.Log("Attacker: " + GetInstanceID() + "(" + antType + ") Target: " + other.gameObject.GetComponent<Ant>().homeAnthill.GetInstanceID() + " (" + other.gameObject.GetComponent<Ant>().antType +")" );

                //kill ant and replace with food
                other.GetComponent<Ant>().ChangeAntToFood();
            }

            if (other.gameObject.GetComponent<Ant>().antType == Ant.AntType.Soldier)
            {
                Attack(other.gameObject);
            }
        }
    }

    private void Attack(GameObject attackTarget)
    {
        attackTarget.GetComponent<SoldierAnt>().ManageAttackFrom(this.gameObject, Random.Range(0,damageOutputMax));
        targetOfAttack = attackTarget;
    }

    public void ManageAttackFrom(GameObject attacker, int attackHitPoints)
    {
        if (targetOfAttack == null)
        {
            targetOfAttack = attacker;
        }

        hitPoints -= attackHitPoints;

        if (hitPoints <= 0)
        {
            //kill ant and replace with food
            ChangeAntToFood();
        }

    }

}
