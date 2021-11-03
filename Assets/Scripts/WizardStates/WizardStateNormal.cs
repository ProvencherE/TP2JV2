using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        attackRange = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveWizard();
        CheckRange();
    }

    private void CheckRange()
    {
        GameObject[] allEnemyWizards = new GameObject[10];
        if (isBlue)
        {
            allEnemyWizards = GameObject.FindGameObjectsWithTag("GreenWizard");
        }
        if (isGreen)
        {
            allEnemyWizards = GameObject.FindGameObjectsWithTag("BlueWizard");
        }
        for(int i = 0; i < allEnemyWizards.Length; i++)
        {
            if(Vector2.Distance(transform.position, allEnemyWizards[i].transform.position)  < attackRange)
            {
                break;
            }
        }
    }

    public override void ManageStateChange()
    {
        
    }

    public override void MoveWizard()
    {
        throw new System.NotImplementedException();
    }
}
