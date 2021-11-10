using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFearless : WizardState
{
    int timeToRegenerate = 60;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().flipY = false;
        towersToTarget = new GameObject[3];
        speed = 3f;
        attackRange = 2f;
        if (isBlue)
        {
            towersToTarget = GameObject.FindGameObjectsWithTag("GreenTower");
        }
        if (isGreen)
        {
            towersToTarget = GameObject.FindGameObjectsWithTag("BlueTower");
        }
        //Trouver le code ici: https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (GameObject tower in towersToTarget)
        {
            float dist = Vector2.Distance(tower.transform.position, currentPos);
            if (dist < minDist)
            {
                targetedTower = tower;
                minDist = dist;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<WizardManager>().healthLevel < 0)
        {
            gameObject.SetActive(false);
        }
        if (!inBattle)
        {
            MoveWizard();
            CheckRange();
            ManageStateChange();
        }
        else
        {
            regenerateHealth();
        }
    }

    private void regenerateHealth()
    {
        if (timeToRegenerate <= 0)
        {
            timeToRegenerate = 60;
            gameObject.GetComponent<WizardManager>().healthLevel += 1;
        }
        else
        {
            timeToRegenerate--;
        }
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
        if (Vector2.Distance(transform.position, targetedTower.transform.position) < attackRange)
        {
            shootProjectile(targetedTower);
            inBattle = true;
        }
    }

    public override void ManageStateChange()
    {
        if (gameObject.GetComponent<WizardManager>().healthLevel <= MAX_HEALTH_LEVEL * 0.25)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Flee);
        }
    }

    public override void MoveWizard()
    {
        if (!inBattle)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetedTower.transform.position, speed * Time.deltaTime);
            speed += 0.1f;
        }
    }

}
