using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] private Sprite chosenSprite;
    public enum wizardStateToSwitch { Normal, Flee, Hide, Safe, Fearless}

    private SpriteRenderer spriteRenderer;
    private WizardState wizardState;

    private Transform forestInContact = null;

    // Start is called before the first frame update
    void Awake()
    {
        wizardState = GetComponent<WizardState>();
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
                    break;
                }
            case wizardStateToSwitch.Fearless:
                {
                    wizardState = gameObject.AddComponent<WizardStateFearless>() as WizardStateFearless;
                    break;
                }
            case wizardStateToSwitch.Flee:
                {
                    wizardState = gameObject.AddComponent<WizardStateFlee>() as WizardStateFlee;
                    break;
                }
            case wizardStateToSwitch.Hide:
                {
                    wizardState = gameObject.AddComponent<WizardStateHide>() as WizardStateHide;
                    break;
                }
            case wizardStateToSwitch.Safe:
                {
                    wizardState = gameObject.AddComponent<WizardStateSafe>() as WizardStateSafe;
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

}
