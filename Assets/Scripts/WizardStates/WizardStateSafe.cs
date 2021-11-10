using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSafe : WizardState
{
    int timeToRegenerate = 20;
    // Start is called before the first frame update
    void Start()
    {
        if (showDetails)
        {
            print("Il est en sécurité dans le chateau");
        }
        isSafe = true;
    }

    // Update is called once per frame
    void Update()
    {
        regenerateHealth();
        ManageStateChange();
    }

    private void regenerateHealth()
    {
        if(timeToRegenerate <= 0)
        {
            timeToRegenerate = 20;
            gameObject.GetComponent<WizardManager>().healthLevel += 1;
        }
        else
        {
            timeToRegenerate--;
        }
    }

    public override void ManageStateChange()
    {
        if(gameObject.GetComponent<WizardManager>().healthLevel >= MAX_HEALTH_LEVEL)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Normal);
        }
    }

    public override void MoveWizard()
    {
        //Ne fait rien
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.activeSelf)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Normal);
        }
    }
}
