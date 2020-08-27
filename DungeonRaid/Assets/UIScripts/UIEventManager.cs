using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    bool eventInProgress { get; set;}

    [SerializeField] GameObject levelUpPanel;

    Queue<UIEvent> UIEventQueue = new Queue<UIEvent>();
    UIEvent currentEvent;

    void LevelUpEvent(int EXPToNextLevel, int currentLevel) {
        levelUpPanel.GetComponent<LevelUpUIManager>().ShowLevelUpMenu(EXPToNextLevel, currentLevel);
    }

	void Start() {
        eventInProgress = false;	
	}

	// Update is called once per frame
	void Update()
    {
        if (!eventInProgress) {
            CheckForEvents();
        }
    }

    public void QueueEvent(UIEvent myEvent) {
        UIEventQueue.Enqueue(myEvent);
    }

    void CheckForEvents() {
        if (UIEventQueue.Count != 0) {
            eventInProgress = true;
            currentEvent = UIEventQueue.Dequeue();

            switch (currentEvent.type) {
                case "LevelUp":
                    LevelUpEvent(currentEvent.EXPToNextLevel, currentEvent.currentLevel);
                    break;
            }
        }
	}

    public void EndUIEvent(string eventType) {
        eventInProgress = false;
	}


}
