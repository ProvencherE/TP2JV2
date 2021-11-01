using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardManager wizardManager;
    // Start is called before the first frame update
    void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
    }

    // Update is called once per frame
    void Update() {}
}
