using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] private Sprite chosenSprite;
    [SerializeField] private GameObject projectilePrefab;
    public enum wizardStateToSwitch { Normal, Flee, Hide, Safe, Fearless }

    private SpriteRenderer spriteRenderer;
    private WizardState wizardState;

    private Transform forestInContact = null;
    private GameObject projectile;
    private float damageReceiveInForest = 0.75f;

    // Start is called before the first frame update

    void Awake()
    {
        projectile = Instantiate(projectilePrefab);
        projectile.SetActive(false);
        wizardState = GetComponent<WizardState>();
        wizardState.asignProjectile(projectile);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = chosenSprite;
    }

    public void changeWizardState(wizardStateToSwitch nextState)
    {
        Destroy(wizardState);

        switch (nextState)
        {
            case wizardStateToSwitch.Normal:
                {
                    wizardState = gameObject.AddComponent<WizardStateNormal>() as WizardStateNormal;
                    wizardState.asignProjectile(projectile);
                    break;
                }
            case wizardStateToSwitch.Fearless:
                {
                    wizardState = gameObject.AddComponent<WizardStateFearless>() as WizardStateFearless;
                    wizardState.asignProjectile(projectile);
                    break;
                }
            case wizardStateToSwitch.Flee:
                {
                    wizardState = gameObject.AddComponent<WizardStateFlee>() as WizardStateFlee;
                    wizardState.asignProjectile(projectile);
                    break;
                }
            case wizardStateToSwitch.Hide:
                {
                    wizardState = gameObject.AddComponent<WizardStateHide>() as WizardStateHide;
                    wizardState.asignProjectile(projectile);
                    break;
                }
            case wizardStateToSwitch.Safe:
                {
                    wizardState = gameObject.AddComponent<WizardStateSafe>() as WizardStateSafe;
                    wizardState.asignProjectile(projectile);
                    break;
                }
        }

    }

    public Transform getForestInContact()
    {
        return forestInContact;
    }

    public void quitForest()
    {
        forestInContact = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Forest")
            forestInContact = other.transform;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Forest")
            forestInContact = null;
    }

    public void spawn(Vector3 position)
    {
        this.transform.position = new Vector3(position.x * 5 / 6, position.y, position.z);
        this.gameObject.SetActive(true);
    }

    public void setDamage(int damage)
    {
        if(forestInContact != null) damage = (int)(damage * damageReceiveInForest) ;
        wizardState.setDamage(damage);
    }
}
