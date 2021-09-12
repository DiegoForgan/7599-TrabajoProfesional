using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyData enemyData;
    public HealthBar healthBar;

    private List<SpellData> availableSpells;
    private MeleeWeapon meleeWeapon;
    private SpriteRenderer _enemySpriteRenderer;
    private ShootProyectile _shootProyectile;
    
    //Called before the Start function
    private void Awake() {
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _enemySpriteRenderer.color = Color.green;
        _shootProyectile = GetComponent<ShootProyectile>();
    }

    private void Start() {
        health = enemyData.health;
        maxHealth = health;
        availableSpells = enemyData.availableSpell; 
        healthBar.initialize(enemyData.health);
    }

    public override void TakeDamage(int damage)
    {
        StartCoroutine(flashColor());
        base.TakeDamage(damage);
        healthBar.SetHealth(health);
        //_shootProyectile.Shoot();
        ShootRandomSpell();
    }
    
    void ShootRandomSpell(){
        SpellData selectedSpell = availableSpells[Random.Range(0,availableSpells.Count)];
        Transform shootPoint = transform.Find("ShootPoint");
        GameObject proyectile = Instantiate(selectedSpell.proyectilePrefab,shootPoint.position,shootPoint.rotation);
        Rigidbody2D proyectileRigidBody = proyectile.GetComponent<Rigidbody2D>();
        FindObjectOfType<AudioManager>().PlaySound(selectedSpell.name);
        proyectileRigidBody.AddForce(shootPoint.up * selectedSpell.proyectileForce, ForceMode2D.Impulse);
    }

    IEnumerator flashColor(){
        _enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _enemySpriteRenderer.color = Color.green;
    }
}
