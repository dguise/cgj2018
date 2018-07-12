using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Unit owner;
    Image currentHealthImage;
    RectTransform foreground;
    private float startSizeDeltaX;
    private float startSizeDeltaY;
    public Gradient healthGradient;

    Vector2 offset = new Vector2(-24, 60);
    void Start()
    {
        foreground = (RectTransform)transform.GetChild(0);
        startSizeDeltaX = foreground.sizeDelta.x;
        startSizeDeltaY = foreground.sizeDelta.y;

        owner = transform.parent.GetComponent<Unit>();
        currentHealthImage = GetComponentInChildren<Image>();
    }


    void Update()
    {
        var percentHealth = GetPercentualHealth(owner);
        foreground.sizeDelta = GetGradientVector(percentHealth, foreground.sizeDelta);
        foreground.position = Camera.main.WorldToScreenPoint(owner.transform.position) + (Vector3)offset;
        if (percentHealth >= 1)
        {
            currentHealthImage.enabled = false;
        } 
        else if (currentHealthImage.enabled == false)
        {
            currentHealthImage.enabled = true;
        }
    }

    float GetPercentualHealth(Unit unit)
    {

        float maxHealth = unit.maxHealth; 
        float currentHealth = unit.Health;
        return currentHealth / maxHealth;
    }

    Vector2 GetGradientVector(float percentageHealth, Vector2 oldSizeDelta)
    {
        this.GetComponentInChildren<Image>().color = healthGradient.Evaluate(1 - percentageHealth);
        return new Vector2(oldSizeDelta.x, percentageHealth * startSizeDeltaY);
    }
}
