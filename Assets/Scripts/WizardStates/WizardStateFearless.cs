using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFearless : WizardState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveWizard();
        ManageStateChange();
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
        speed += 0.1f;
    }

}
