using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlider : MonoBehaviour
{
    float timeToMove = 2;

    Vector2 initialPosition;
    Vector2 startingPosition;
    Vector2 endingPosition;

    bool moving = false;
    bool movingIn = false;
    bool movingOut = false;

    Vector2 speed = Vector2.zero;

    public void StartMove(Vector2 starting, Vector2 ending, float ttm) {
        startingPosition = starting;
        endingPosition = ending;
        timeToMove = ttm;
        moving = true;
        movingIn = true;
        movingOut = false;
    }

    public void ReturnToLastPosition(float ttm) {
        movingIn = false;
        movingOut = true;
        timeToMove = ttm;
    }

    public void ReturnToInitialPosition(float ttm) {
        movingIn = false;
        movingOut = true;
        timeToMove = ttm;
        startingPosition = initialPosition;
    }


    public void Start() {
        initialPosition = transform.localPosition;
    }

    private void Update() {
        if (moving) {
            if (movingIn) { //Move down
                Vector2 localPosition = transform.localPosition;

                Vector2 newPosition = Vector2.SmoothDamp(localPosition, endingPosition, ref speed, timeToMove);
                transform.localPosition = newPosition;
                timeToMove -= Time.deltaTime;

                if (transform.localPosition.x == endingPosition.x && transform.localPosition.y == endingPosition.y) {
                    movingIn = false;
                    speed = Vector2.zero;
                }
            }
            else if (movingOut) {
                Vector2 localPosition = transform.localPosition;
                Vector2 newPosition = Vector2.SmoothDamp(localPosition, startingPosition, ref speed, timeToMove);
                transform.localPosition = newPosition;
                timeToMove -= Time.deltaTime;

                if (transform.localPosition.x == startingPosition.x && transform.localPosition.y == startingPosition.y) {
                    movingOut = false;
                    speed = Vector2.zero;
                }
            }
        }
    }
}
