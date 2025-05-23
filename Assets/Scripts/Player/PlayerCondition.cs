using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    public event Action onTakeDamge;
    private void Awake()
    {
        uiCondition = GetComponent<UICondition>();    
    }
    void Start()
    {
        
    }

    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f) 
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("����");
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount) 
    {
        hunger.Add(amount); 
    }
    
    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamge?.Invoke();
    }
}
