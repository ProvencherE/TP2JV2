using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    // Start is called before the first frame update
    void Start()
    {
        int numberSelector = UnityEngine.Random.Range(0, 2);
        towersToTarget = new GameObject[3];
        speed = 3f;
        attackRange = 5f;
        if (isBlue)
        {
            towersToTarget = GameObject.FindGameObjectsWithTag("GreenTower");
        }
        if (isGreen)
        {
            towersToTarget = GameObject.FindGameObjectsWithTag("BlueTower");
        }
        if(numberSelector == 0 && towersToTarget[0].activeSelf)
        {
            targetedTower = towersToTarget[0];
        }
        else if (numberSelector == 1 && towersToTarget[1].activeSelf)
        {
            targetedTower = towersToTarget[1];
        }
        else if (numberSelector == 1 && towersToTarget[1].activeSelf)
        {
            targetedTower = towersToTarget[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveWizard();
        CheckRange();
    }

    private void CheckRange()
    {
        if (isBlue)
        {
            allEnemyWizards = new GameObject[GameObject.FindGameObjectsWithTag("GreenWizard").Length];
            allEnemyWizards = GameObject.FindGameObjectsWithTag("GreenWizard");
        }
        if (isGreen)
        {
            allEnemyWizards = new GameObject[GameObject.FindGameObjectsWithTag("BlueWizard").Length];
            allEnemyWizards = GameObject.FindGameObjectsWithTag("BlueWizard");
        }
        if(Vector2.Distance(transform.position, targetedTower.transform.position) < attackRange)
        {
            shootProjectile(targetedTower.transform.position);
            inBattle = true;
        }
        else
        {
            for (int i = 0; i < allEnemyWizards.Length; i++)
            {
                if (allEnemyWizards[i].activeSelf && Vector2.Distance(transform.position, allEnemyWizards[i].transform.position) < attackRange)
                {
                    shootProjectile(allEnemyWizards[i].transform.position);
                    inBattle = true;
                    break;
                }
            }
        }
    }

    private void shootProjectile(Vector2 position)
    {
        
    }

    public override void ManageStateChange()
    {
        
    }

    public override void MoveWizard()
    {
        if (!inBattle)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetedTower.transform.position, speed * Time.deltaTime);
            float angle = Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
