using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFearless : WizardState
{
    // Start is called before the first frame update
    void Start()
    {
        if (showDetails)
        {
            print("Il entre en état intrépide");
        }
        gameObject.GetComponent<SpriteRenderer>().flipY = false;
        towersToTarget = new GameObject[3];
        speed = 3f;
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
        if (!inBattle)
        {
            MoveWizard();
            CheckRangeFearless();
            ManageStateChange();
        }
        else
        {
            regenerateHealth();
        }
    }

    protected override void regenerateHealth()
    {
        timerRegen -= Time.deltaTime;
        if (timerRegen <= 0)
        {
            wizardManager.heal(regenerationHPS);
            timerRegen = 1;
        }
    }
    
    protected void CheckRangeFearless()
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
