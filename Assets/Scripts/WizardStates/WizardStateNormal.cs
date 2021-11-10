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
        speed = 3f;
        attackRange = 4f;
        findNearestTower();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<WizardManager>().healthLevel < 0)
        {
            gameObject.SetActive(false);
        }
        MoveWizard();
        CheckRange();
        ManageStateChange();
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
