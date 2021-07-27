using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INHERITANCE
public class SoldierAnt : Ant
{
    private SoldierAnt targetOfAttack;
    private int damageOutputMax = 3;

 //ABSTRACTION
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
                Attack(other.gameObject.GetComponent<SoldierAnt>());
            }
        }
    }

    protected override void ResolveContactWithAnthill(Collider other)
    {
        if (!GameObject.ReferenceEquals(other.gameObject, homeAnthill))
        {
            Attack(other.gameObject.GetComponent<Anthill>());
        }
    }

    private void Attack(SoldierAnt target)
    {
        target.ManageAttackFrom(gameObject, Random.Range(0, damageOutputMax));
        targetOfAttack = target;
    }

    private void NewAttackLocation(Vector3 location)
    {
        targetLocation = location;
    }

    private void Attack(Anthill target)
    {
        Debug.Log("Solidier attacking enemy fort!");
        SoldierAnt[] soldiers = FindObjectsOfType<SoldierAnt>();
        
        foreach (SoldierAnt soldier in soldiers)
        {
            if (targetOfAttack == null && ReferenceEquals(target, homeAnthill.GetComponent<Anthill>()))
            {
                NewAttackLocation(target.gameObject.transform.position);
            }

            target.ManageStructureDamage();
            ChangeAntToFood(); //Suicide mission
            
        }

    }

   /* private void Attack(GameObject attackTarget)
    {
        
    }*/

    public void ManageAttackFrom(GameObject attacker, int attackHitPoints)
    {
        if (targetOfAttack == null)
        {
            targetOfAttack = attacker.GetComponent<SoldierAnt>();
        }

        hitPoints -= attackHitPoints;

        if (hitPoints <= 0)
        {
            //kill ant and replace with food
            ChangeAntToFood();
        }

    }

}
