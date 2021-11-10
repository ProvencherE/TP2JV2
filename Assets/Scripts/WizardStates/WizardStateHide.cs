using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateHide : WizardState
{
    private int regenMultiply = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageAction();
        ManageStateChange();
    }

    public override void ManageStateChange()
    {
        if(wizardManager.healthLevel == MAX_HEALTH_LEVEL)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Normal);
        }
        else if(wizardManager.healthLevel >= MAX_HEALTH_LEVEL * 0.5 && inBattle)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Normal);
        }
        else if (wizardManager.healthLevel <= MAX_HEALTH_LEVEL * 0.25 && inBattle)
        {
            wizardManager.changeWizardState(WizardManager.wizardStateToSwitch.Flee);
        }
    }

    public override void MoveWizard()
    {
        //Never
    }

    private void ManageAction()
    {
        CheckRange();
        if(!inBattle) Regenerate();
    }

    private void Regenerate()
    {
        timerRegen -= Time.deltaTime;
        if(timerRegen <= 0)
        {
            wizardManager.heal(regenerationHPS * regenMultiply);
        }
    }
}
