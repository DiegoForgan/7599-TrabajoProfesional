using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
   [SerializeField]
   private HealthBarWithAmount healthBar;
   [SerializeField]
   private ManaBar manaBar;
   [SerializeField]
   private TextMeshProUGUI gold;
   [SerializeField]
   private TextMeshProUGUI keys;

   public void InitHUD(int maxHealth, int maxMana){
       healthBar.initialize(maxHealth);
       manaBar.Initialize(maxMana);
       UpdateGold(0);
       UpdateKeys(0);
   }

   public void UpdateHealth(int health){
       healthBar.SetHealth(health);
   }

   public void UpdateMaxHealth(int newMaxHealth){
       healthBar.SetMaxHealth(newMaxHealth);
   }

   public void UpdateMana(int mana){
       manaBar.SetBarMana(mana);
   }

   public void UpdateMaxMana(int newMaxMana){
       manaBar.SetMaxBarMana(newMaxMana);
   }

   public void UpdateGold(int goldValue){
       gold.SetText("Gold: " + goldValue);
   }

   public void UpdateKeys(int keysValue){
       keys.SetText("Keys: " + keysValue);
   }
}
