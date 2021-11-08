using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    [SerializeField] bool showDetails;
    protected WizardManager wizardManager;
    protected float speed;
    protected bool hiddenInForest = false;
    protected bool hiddenInTower = false;
    protected const float MAX_HEALTH_LEVEL = 30;
    protected float attackRange = 5f;
    protected bool isGreen = false;
    protected bool isBlue = false;
    protected bool inBattle = false;
    protected bool isSafe = false;
    protected bool isHiding = false;
    protected int killCount = 0;
    protected GameObject[] towersToTarget;
    protected GameObject[] allEnemyWizards;
    protected GameObject[] projectiles;
    protected GameObject projectileBase;
    protected GameObject targetedTower;
    // Start is called before the first frame update
    void Awake()
    {
        if (showDetails)
        {
            print("Changement d'�tat");
        }
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
