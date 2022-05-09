using System;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private PlayerData playerData; 
    private int maxMana;
    private int mana;
    private int keys;
    private int gold;
    private HUD _hud;
    private PlayerMovement _playerMovement;
    private WeaponManagement _weaponManagement;
    private bool canOpenDoor;
    public static Player instance;

    private void Awake()
    {
        // Gets the player references to UI and other script components
        GetPlayerReferences();
        // Singleton implementation
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        // Maintain this object through all the life of the game
        DontDestroyOnLoad(gameObject);
    }

    private void GetPlayerReferences()
    {
        _hud = GameObject.Find("HUD").GetComponent<HUD>();
        _playerMovement = GetComponent<PlayerMovement>();
        _weaponManagement = GetComponent<WeaponManagement>();
    }

    internal void DisableKeyAction()
    {
        canOpenDoor = false;
    }

    public void InitializeStats(){
        canOpenDoor = false;
        slowedDown = false;
        health = playerData.health;
        maxHealth = health;
        mana = playerData.mana;
        maxMana = mana;
        keys = playerData.keys;
        gold = playerData.gold;
        //Initialize the HUD
        _hud.InitHUD(playerData.health,playerData.mana,playerData.gold,playerData.keys);
    }

    public void InitializeMovementStats(){
        //Passing movement data to the movement component
        _playerMovement.SetMovementSpeed(playerData.movementSpeed);
    }

    public void InitializeSpellsAndAttacksStats(){
        //Passing spells and attack data to the weapon management component
        _weaponManagement.SetSpellAndAttackStats(playerData);
    }
    private void Start() {
        InitializeStats();
        InitializeMovementStats();
        InitializeSpellsAndAttacksStats();
    }

    internal void EnableKeyAction()
    {
        if(keys >= 1) canOpenDoor = true;
    }

    private void Update() {
        if(canOpenDoor && Input.GetKeyDown(KeyCode.E)){
            SpendKey();
            DisableKeyAction();
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.N)) LoadNextLevel();
        if(Input.GetKeyDown(KeyCode.K)) DestroyElement();
    }

    private void LoadNextLevel()
    {
        Debug.Log("Door Opened!\nNew level loaded");
        //Maybe play a level completion sound before loading next scene
        GameManager.Instance.LoadNextLevel();
    }

    public override void TakeDamage(int damage){
        base.TakeDamage(damage);
        _hud.UpdateHealth(health);
    }

    public override void DestroyElement(){
        Debug.Log("You were killed :(");
        GameManager.Instance.ShowGameOver();
        FindObjectOfType<AudioManager>().PlaySound("GameOverTheme");
        Destroy(gameObject);
    } 
    
    public void AddHealth(int amount){
        health += amount;
        if(health >= maxHealth) health = maxHealth;
        //Update the health bar HUD value aswell
        _hud.UpdateHealth(health);
    }

    public void AddMana(int amountToGrant)
    {
        mana += amountToGrant;
        if (mana >= maxMana) mana = maxMana;
        //Update the health bar HUD value aswell
        _hud.UpdateMana(mana);
    }

    public int GetMana() { return mana;}

    public void SpendMana(int amountSpent){
        mana -= amountSpent;
        if(mana < 0) mana = 0;
        _hud.UpdateMana(mana);
    }

    //Key Pickup Logic
    public void ObtainKey()
    {   
        keys = keys+1;
        _hud.UpdateKeys(keys);
    }

    public void SpendKey(){
        keys = keys-1;
        _hud.UpdateKeys(keys);
    }

    //Gold currency logic when gold item was picked up by the player
    public void AddGold(int goldGranted)
    {
        gold += goldGranted;
        _hud.UpdateGold(gold);
    }
}
