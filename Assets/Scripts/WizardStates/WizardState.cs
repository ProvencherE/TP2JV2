using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    [SerializeField] public bool showDetails;
    protected WizardManager wizardManager;
    protected float speed;
    protected bool hiddenInForest = false;
    protected bool hiddenInTower = false;
    public const int MAX_HEALTH_LEVEL = 30;
    protected float attackRange = 5f;
    protected bool isGreen = false;
    protected bool isBlue = false;
    protected bool inBattle = false;
    protected bool isSafe = false;
    protected bool isHiding = false;
    protected int killCount = 0;
    protected int regenerationHPS = 5;
    protected float timerRegen = 1;
    protected GameObject[] towersToTarget;
    protected GameObject[] allEnemyWizards;
    protected GameObject[] projectiles;
    protected GameObject projectileBase;
    protected GameObject targetedTower;
    protected GameObject fleeingPosition;
    protected GameObject[] potentialForestToHideTo;
    protected GameObject[] towersToFleeTo;
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

    protected abstract void regenerateHealth();

    public void asignProjectile(GameObject projectile)
    {
        projectileBase = projectile;
    }

    protected void shootProjectile(GameObject target)
    {
        if (!projectileBase.activeSelf) projectileBase.GetComponent<ProjectileMovement>().fire(transform.position, target);
    }

    protected void CheckRange()
    {
        if (isBlue)
        {
            allEnemyWizards = new GameObject[GameObject.FindGameObjectsWithTag("GreenWizard").Length];
            allEnemyWizards = GameObject.FindGameObjectsWithTag("GreenWizard");
        }
        if (isGreen)
        {
            allEnemyWizards = new GameObject[GameObject.FindGameObjectsWithTag("BlueWizard").Length];
            allEnemyWizards = GameObject.FindGameObjectsWithTag("BlueWizard");
        }
        if (Vector2.Distance(transform.position, targetedTower.transform.position) < attackRange)
        {
            if (targetedTower.activeSelf)
            {
                shootProjectile(targetedTower);
                inBattle = true;
            }
            else
            {
                findNearestTower();
                inBattle = false;
            }
        }
        else
        {
            int counter = 0;
            for (int i = 0; i < allEnemyWizards.Length; i++)
            {
                if (allEnemyWizards[i].activeSelf && Vector2.Distance(transform.position, allEnemyWizards[i].transform.position) < attackRange)
                {
                    shootProjectile(allEnemyWizards[i]);
                    inBattle = true;
                    break;
                }
                else counter++;
            }
            if (counter == allEnemyWizards.Length) inBattle = false;
        }
    }

    protected void findNearestTower()
    {
        if (isBlue)
        {
            towersToTarget = new GameObject[GameObject.FindGameObjectsWithTag("GreenTower").Length];
            towersToTarget = GameObject.FindGameObjectsWithTag("GreenTower");
        }
        else if (isGreen)
        {
            towersToTarget = new GameObject[GameObject.FindGameObjectsWithTag("BlueTower").Length];
            towersToTarget = GameObject.FindGameObjectsWithTag("BlueTower");
        }
        //Trouver le code ici: https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (GameObject tower in towersToTarget)
        {
            float dist = Vector2.Distance(tower.transform.position, currentPos);
            if (dist < minDist)
            {
                targetedTower = tower;
                minDist = dist;
            }
        }
    }

}
