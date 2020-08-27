using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] int health = 10;
    [SerializeField] int maxHealth = 20;
    [SerializeField] int armor = 0;
    [SerializeField] int maxArmor = 10;
    [SerializeField] int exp = 0;
    [SerializeField] int levelUpThreshold = 10;
    [SerializeField] int gold = 0;
    [SerializeField] int level = 1;
    [SerializeField] int baseDamage = 1;

    [SerializeField] int levelUpDamageIncrease = 1;
    [SerializeField] int levelUpHealthIncrease = 1;

    public int Health { get { return health; } }
    public int MaxHealth { get { return maxHealth; } }
    public int Armor { get { return armor; } }
    public int Gold { get { return gold; } }
    public int EXP { get { return exp; } }
    public int LevelUpThreshold { get { return levelUpThreshold; } }
    public int Level { get { return level; } }
    public int MaxArmor { get { return maxArmor; } }
    public int Damage { get { return baseDamage; } }



    public void AddHealth(int amountHealed) {
        health += amountHealed;

        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    public void DealDamage(int damageDealt) {
        int actualDamage = damageDealt - armor;
        if (actualDamage > 0) {
            health -= actualDamage;
        }

        if (health <= 0) {
            Die();
        }

        armor -= damageDealt;
        if (armor < 0) {
            armor = 0;
        }
    }

    public void AddEXP(int amount) {
        exp += amount;

        while (exp >= levelUpThreshold) {
            LevelUp();
        }
    }

    public void AddGold(int amount) {
        gold += amount;
    }

    public void AddArmor(int amount) {
        armor += amount;

        if (armor > maxArmor) {
            armor = maxArmor;
        }
    }

    void LevelUp() {

        Debug.Log("Level Up!");

        exp -= levelUpThreshold;

        baseDamage += levelUpDamageIncrease;
        maxHealth += levelUpHealthIncrease;
        health += levelUpHealthIncrease;
        level++;
        levelUpThreshold += level;

        UIEvent levelUpEvent = new UIEvent("LevelUp",level,LevelUpThreshold);
        GameObject.FindGameObjectWithTag("UI").GetComponent<UIEventManager>().QueueEvent(levelUpEvent);
    }

    void Die() {
        
        GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().GameOver();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SceneManagerClass>().ReloadScene();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
