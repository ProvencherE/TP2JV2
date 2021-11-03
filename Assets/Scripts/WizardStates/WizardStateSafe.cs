using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSafe : WizardState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ManageStateChange()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveWizard()
    {
        throw new System.NotImplementedException();
    }

}
