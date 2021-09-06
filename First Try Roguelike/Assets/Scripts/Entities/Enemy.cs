using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public HealthBar healthBar;
    private SpriteRenderer _enemySpriteRenderer;
    private ShootProyectile _shootProyectile;
    
    //Called before the Start function
    private void Awake() {
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _enemySpriteRenderer.color = Color.green;
        _shootProyectile = GetComponent<ShootProyectile>();
    }

    private void Start() {
        health = maxHealth;
        healthBar.initialize(health);
    }

    public override void TakeDamage(int damage)
    {
        StartCoroutine(flashColor());
        base.TakeDamage(damage);
        healthBar.SetHealth(health);
        _shootProyectile.Shoot();
    }

    IEnumerator flashColor(){
        _enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _enemySpriteRenderer.color = Color.green;
    }
}
