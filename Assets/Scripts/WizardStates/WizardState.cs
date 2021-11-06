using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardManager wizardManager;
    protected float speed;
    protected bool hiddenInForest = false;
    protected bool hiddenInTower = false;
    protected float healthLevel = 30;
    protected float attackRange = 5f;
    protected bool isGreen = false;
    protected bool isBlue = false;
    protected bool inBattle = false;
    protected GameObject[] towersToTarget;
    protected GameObject[] allEnemyWizards;
    protected GameObject[] projectiles;
    protected GameObject projectileBase;
    protected GameObject targetedTower;
    // Start is called before the first frame update
    void Awake()
    {
        if(tag == "GreenWizard")
        {
            isGreen = true;
        }
        if(tag == "BlueWizard")
        {
            isBlue = true;
        }
        wizardManager = GetComponent<WizardManager>();
    }

    // Update is called once per frame
    void Update() {}

    public abstract void MoveWizard();

    public abstract void ManageStateChange();
}
