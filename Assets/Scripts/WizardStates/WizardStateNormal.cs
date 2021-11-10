using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().flipY = false;
        int numberSelector = UnityEngine.Random.Range(0, 2);
        towersToTarget = new GameObject[3];
        speed = 3f;
        attackRange = 2f;
        if (isBlue)
        {
            towersToTarget = GameObject.FindGameObjectsWithTag("GreenTower");
            projectileBase = GameObject.FindGameObjectWithTag("BlueProjectile");
        }
        if (isGreen)
        {
            towersToTarget = GameObject.FindGameObjectsWithTag("BlueTower");
            projectileBase = GameObject.FindGameObjectWithTag("GreenProjectile");
        }
        //Trouver le code ici: https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (GameObject tower in towersToTarget)
        {
            float dist = Vector2.Distance(tower.transform.position, currentPos);
            if(dist < minDist)
            {
                targetedTower = tower;
                minDist = dist;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveWizard();
        CheckRange();
        ManageStateChange();
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

    public void addKillToCount()
    {
        killCount++;
    }

    public override void ManageStateChange()
    {
        if(gameObject.GetComponent<WizardManager>().healthLevel <= MAX_HEALTH_LEVEL * 0.25)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Flee);
        }
        else if (killCount >= 3)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Fearless);
        }
    }

    public override void MoveWizard()
    {
        if (!inBattle)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetedTower.transform.position, speed * Time.deltaTime);
        }
    }
}
