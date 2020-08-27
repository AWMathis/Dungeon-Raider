using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] Text healthText;
    [SerializeField] Image healthImage;
    [SerializeField] float healthBarFillSpeed = 5f;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject blackOverlay;


    [SerializeField] Text armorText;

    [SerializeField] Text goldText;

    [SerializeField] Text expText;
    [SerializeField] Image EXPImage;
    [SerializeField] float EXPFillSpeed = 5f;

    [SerializeField] Text levelText;

    [SerializeField] Text scoreText;


    Player player;

    bool screenFadedIn = false;

    
    //public string State { get { return state; } }

    void UpdateHealth() {


        int playerHealth = player.Health;
        int playerMaxHealth = player.MaxHealth;
        string playerHealthText = playerHealth + "/" + playerMaxHealth;
        healthText.text = playerHealthText;


        float desiredFill = (float)playerHealth/(float)playerMaxHealth;
        float currentFill = healthImage.fillAmount;

        healthImage.fillAmount = Mathf.Lerp(currentFill, desiredFill, Time.deltaTime * healthBarFillSpeed);

    }

    void UpdateArmor() {
        armorText.text = player.Armor.ToString();
    }

    void UpdateGold() {
        goldText.text = player.Gold.ToString();
    }

    void UpdateEXP() {
        expText.text = player.EXP + "/" + player.LevelUpThreshold;

        float playerEXP = player.EXP;
        float threshold = player.LevelUpThreshold;

        float desiredFill = (float)playerEXP / (float)threshold;
        float currentFill = EXPImage.fillAmount;

        EXPImage.fillAmount = Mathf.Lerp(currentFill, desiredFill, Time.deltaTime * EXPFillSpeed);

    }

    void UpdateLevel() {
        levelText.text = player.Level.ToString();
    }


    public void GameOver() {
        gameUI.SetActive(false);
        blackOverlay.GetComponent<ScreenFade>().fadeIn(5f);
    }




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateArmor();
        UpdateGold();
        UpdateEXP();
        UpdateLevel();

        if (!screenFadedIn) {
           blackOverlay.GetComponent<ScreenFade>().fadeOut(2f);
            screenFadedIn = true;
        }
    }
}
