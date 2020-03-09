using System.Collections;
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

    [SerializeField] Text levelText;

    [SerializeField] GameObject levelUpPanel;

    Player player;

    bool screenFadedIn = false;

    struct UIEvent {
        public string type;
        public int currentLevel;
        public int EXPToNextLevel;
    }
    bool UIEventInProgress = false;
    Queue<UIEvent> UIEvents;

    
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
    }

    void UpdateLevel() {
        levelText.text = player.Level.ToString();
    }


    public void GameOver() {
        gameUI.SetActive(false);
        blackOverlay.GetComponent<ScreenFade>().fadeIn(5f);
    }

    public void ShowLevelUpUI(int EXPToNextLevel, int currentLevel) {
        UIEvent tempLevelUpEvent = new UIEvent { type = "LevelUp", EXPToNextLevel = EXPToNextLevel, currentLevel = currentLevel };
        UIEvents.Enqueue(tempLevelUpEvent);
    }

    void LevelUpEvent(int EXPToNextLevel, int currentLevel) {
        levelUpPanel.GetComponent<LevelUpUIManager>().ShowLevelUpMenu(EXPToNextLevel, currentLevel);
    }

    void CheckForActiveUIEvents() {

        bool levelUpActive = levelUpPanel.GetComponent<LevelUpUIManager>().LevelingUp;

        if (levelUpActive) {
            UIEventInProgress = true;
        }
        else {
            UIEventInProgress = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UIEvents = new Queue<UIEvent>();
        UIEventInProgress = false;
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

        CheckForActiveUIEvents();

        if (UIEventInProgress == false) {
            if (UIEvents.Count != 0) {
                UIEvent newEvent = UIEvents.Dequeue();
                UIEventInProgress = true;

                switch (newEvent.type) {

                    case "LevelUp":
                        LevelUpEvent(newEvent.EXPToNextLevel, newEvent.currentLevel);
                        break;

                    default:
                        break;
                }


            }
        }

        if (!screenFadedIn) {
           blackOverlay.GetComponent<ScreenFade>().fadeOut(2f);
            screenFadedIn = true;
        }
    }
}
