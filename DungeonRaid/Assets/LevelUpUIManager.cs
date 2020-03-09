using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIManager : MonoBehaviour
{

    [SerializeField] float timeToMove = 2;

    int newLevel;
    int expToNextLevel;
    bool levelingUp;

    public bool LevelingUp { get { return levelingUp; } }

    public void ShowLevelUpMenu(int EXPToNextLevel, int currentLevel) {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GameGridManager>().Pause();
        levelingUp = true;
        transform.Find("Text").gameObject.GetComponent<Text>().text = "Congratulations! You've leveled up from " + (currentLevel - 1) + " to " + currentLevel + "!";
        //TODO Implement dynamic text for level up stats
        gameObject.GetComponent<PanelSlider>().StartMove(transform.localPosition, Vector2.zero, timeToMove);
    }

    public void HideLevelUpMenu() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GameGridManager>().UnPause();
        levelingUp = false;
        gameObject.GetComponent<PanelSlider>().ReturnToInitialPosition(timeToMove); ;
    }

}
