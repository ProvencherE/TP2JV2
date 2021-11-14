using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateInactive : WizardState
{

    // Start is called before the first frame update
    void Start()
    {
        if (showDetails)
        {
            print("Il entre en état Inactif");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ManageStateChange()
    {
        
    }

    public override void MoveWizard()
    {
        
    }

    protected override void regenerateHealth()
    {
        
    }
}
