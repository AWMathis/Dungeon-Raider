using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIManager : MonoBehaviour
{

    [SerializeField] float timeToMove = 2;

    int newLevel;
    int expToNextLevel;
    bool eventInProgress;

    public bool EventInProgress { get { return eventInProgress; } }

    public void ShowLevelUpMenu(int EXPToNextLevel, int currentLevel) {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GameGridManager>().Pause();
        eventInProgress = true;
        transform.Find("Text").gameObject.GetComponent<Text>().text = "Congratulations! You've leveled up from " + (currentLevel - 1) + " to " + currentLevel + "!";
        //TODO Implement dynamic text for level up stats
        gameObject.GetComponent<PanelSlider>().StartMove(transform.localPosition, Vector2.zero, timeToMove);
    }

    public void HideLevelUpMenu() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GameGridManager>().UnPause();
        eventInProgress = false;
        gameObject.GetComponent<PanelSlider>().ReturnToInitialPosition(timeToMove);


        GameObject.FindGameObjectWithTag("UI").GetComponent<UIEventManager>().EndUIEvent("LevelUp");
    }

}
