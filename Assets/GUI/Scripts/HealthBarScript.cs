using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;


public class HealthBarScript : MonoBehaviour
{
    public Unit owner;
    public Image currentHealthImage;
    public RectTransform currentHealthRectTransform;
    private float startSizeDeltaX;
    private float startSizeDeltaY;
    public Gradient healthGradient;

    void Start()
    {
        startSizeDeltaX = currentHealthRectTransform.sizeDelta.x;
        startSizeDeltaY = currentHealthRectTransform.sizeDelta.y;
    }


    void Update()
    {
        currentHealthRectTransform.sizeDelta = GetPercentualHealth(owner.GetComponent<Unit>(), currentHealthRectTransform.sizeDelta);
        
    }

    Vector2 GetPercentualHealth(Unit unit, Vector2 oldSizeDelta, bool vertical = false)
    {

        float maxHealth = unit.maxHealth; 
        float currentHealth = unit.Health;
        float currentHealthPercentage = currentHealth / maxHealth;

        this.GetComponentInChildren<UnityEngine.UI.Image>().color = healthGradient.Evaluate(1 - currentHealthPercentage);

        if (vertical)
        {
            //this.GetComponentInChildren<UnityEngine.UI.Image>().color = healthGradient.Evaluate(currentHealthPercentage);
            return new Vector2(currentHealthPercentage * startSizeDeltaX, oldSizeDelta.y);
        }
        else
        {
            //this.GetComponentInChildren<UnityEngine.UI.Image>().color = healthGradient.Evaluate(currentHealthPercentage);
            return new Vector2(oldSizeDelta.x, currentHealthPercentage * startSizeDeltaY);
        }
    }
}
