using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSafe : WizardState
{
    private int regenMultiply = 3;
    // Start is called before the first frame update
    void Start()
    {
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
        timerRegen -= Time.deltaTime;
        if (timerRegen <= 0)
        {
            wizardManager.heal(regenerationHPS * regenMultiply);
            timerRegen = 1;
        }
    }

    public override void ManageStateChange()
    {
        if(wizardManager.healthLevel >= MAX_HEALTH_LEVEL)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Normal);
        }
    }

    public override void MoveWizard()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.activeSelf)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Normal);
        }
    }

}
