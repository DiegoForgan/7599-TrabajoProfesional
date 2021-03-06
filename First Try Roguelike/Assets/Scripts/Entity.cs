using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, Collidable
{
    public int maxHealth = 100;
    public int health;
    public HealthBar healthBar;


    private void Start() {
        initializeHealth();
    }

    public void initializeHealth(){
        health = maxHealth;
        healthBar.initialize(health);
    }
    
    //Implementing "Collidable" interface method
    public virtual void TakeDamage(int damage){
         health -= damage;
         healthBar.SetHealth(health);

         if(health <= 0) DestroyElement();
     }

    public virtual void DestroyElement()
    {
        Destroy(gameObject);
    }

    public int GetHealth(){
        return health;
    }
    public void AddHealth(int amount){
        health += amount;
        if(health >= maxHealth) health = maxHealth;
        //Update the health bar HUD value aswell
        healthBar.SetHealth(health);
    }
}
