using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IDamagable
{
    void TakePhysicalDamage(float damageAmount);
}

[System.Serializable]
public class Condition
{
    [HideInInspector]
    public float currentValue;
    private float minValue = 0.0f;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    public void Add(float amount)
    {
        currentValue = Mathf.Min(currentValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        currentValue = Mathf.Max(currentValue - amount, minValue);
    }

    public float GetPercentage()
    {
        return currentValue / maxValue;
    }
}

public class PlayerConditions : MonoBehaviour, IDamagable
{
    public Condition health;
    public Condition bodyTemperature;
    public Condition hunger;
    public Condition thirst;
    public Condition stamina;

    public float noBodyTemperatureHealthDecay;
    public float noHungerHealthDecay;
    public float noThirstHealthDecay;

    public UnityEvent onTakeDamage;

    private void Start()
    {
        InitConditions();
    }

    private void InitConditions()
    {
        health.currentValue = health.startValue;
        bodyTemperature.currentValue = bodyTemperature.startValue;
        hunger.currentValue = hunger.startValue;
        thirst.currentValue = thirst.startValue;
        stamina.currentValue = stamina.startValue;
    }

    void Update()
    {
        PassiveConditions();

        ChangeConditionsUIBar();
    }

    private void PassiveConditions()
    {
        bodyTemperature.Subtract(bodyTemperature.decayRate * Time.deltaTime);
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);

        stamina.Add(stamina.regenRate * Time.deltaTime);

        if (bodyTemperature.currentValue == 0.0f)
        {
            bodyTemperature.Subtract(noBodyTemperatureHealthDecay * Time.deltaTime);
        }

        if (hunger.currentValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (thirst.currentValue == 0.0f)
        {
            thirst.Subtract(noThirstHealthDecay * Time.deltaTime);
        }

        if (health.currentValue == 0.0f)
        {
            Die();
        }
    }

    private void ChangeConditionsUIBar()
    {
        health.uiBar.fillAmount = health.GetPercentage();
        bodyTemperature.uiBar.fillAmount = bodyTemperature.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        thirst.uiBar.fillAmount = thirst.GetPercentage();
        stamina.uiBar.fillAmount = stamina.GetPercentage();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Heat(float amount)
    {
        bodyTemperature.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
        AudioManager.instance.EatingSound();
    }

    public void Drink(float amount)
    {
        thirst.Add(amount);
        AudioManager.instance.DrinkingSound();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.currentValue - amount < 0)
        {
            return false;
        }

        stamina.Subtract(amount);

        return true;
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakePhysicalDamage(float damageAmount)
    {
        AudioManager.instance.DamageSound();
        health.Subtract(damageAmount);

        onTakeDamage?.Invoke();
    }
}
