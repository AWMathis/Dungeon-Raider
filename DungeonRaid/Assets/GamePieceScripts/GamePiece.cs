using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePiece : MonoBehaviour
{

    [SerializeField] protected string name = "";
    [SerializeField] protected string actionType = ""; //Combat, armor, potion, etc
    [SerializeField] protected Color neutralColor = Color.white;
    [SerializeField] protected Color activeColor = Color.blue;
    [SerializeField] protected Color disabledColor = Color.grey;

    protected string currentActionType;

    //For movement
    Vector3 velocity = Vector3.zero;
    Vector3 destination;
    float timeToMove = 0f;

    //For Pulsing
    float timeToPulse = 0f;
    float timePulsed = 0f;
    Vector2 pulseScaleVector;
    Vector2 originalScale;
    Vector2 scale = Vector2.zero;
    [SerializeField] protected float pulseScale = 0.07f;
    [SerializeField] protected float pulseTime = 0.4f;

    protected Player player;

    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameObject.GetComponent<SpriteRenderer>().color = neutralColor;

        SetAttributes();
    }
    public virtual void SetAttributes() {

    }

    public virtual void OnUse() {
        Destroy(gameObject);
    }

    void OnMouseDown() {
        Debug.Log("Mouse down");
        transform.parent.gameObject.GetComponent<GameGridManager>().GamePieceSelected(actionType, gameObject);
    }

    public void OnMouseEnter() {
        if (Input.GetMouseButton(0) && actionType == currentActionType) {
            transform.parent.gameObject.GetComponent<GameGridManager>().GamePiecePassedOver(gameObject);
        }
    }

    public virtual void UpdateState(string currentActionType, bool currentlySelected, int sumAttack) {

        this.currentActionType = currentActionType;

        if (currentlySelected) {
            gameObject.GetComponent<SpriteRenderer>().color = activeColor;
        }
        else if (currentActionType == "") {
            gameObject.GetComponent<SpriteRenderer>().color = neutralColor;
        }
        else if (actionType != currentActionType) {
            gameObject.GetComponent<SpriteRenderer>().color = disabledColor;
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().color = neutralColor;
        }

    }

    public void MoveTo(Vector2 destinationGiven, float speed) {
        destination = new Vector3(destinationGiven.x, destinationGiven.y, transform.localPosition.z);
        timeToMove = speed;
    }

    public virtual void EachTurnEffect() {

    }

    public void Pulse(float scale, float time) {
        Debug.Log("Setting pulse");
        pulseScaleVector = new Vector2(scale, scale);
        originalScale = transform.localScale;
        timeToPulse = time;
        timePulsed = time;

    }

    public void Update() {

        if (timeToMove != 0) {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, destination, ref velocity, timeToMove);
            timeToMove -= Time.deltaTime;
        }

        if (timeToPulse > 0) {
            if (timeToPulse > timePulsed/2) {
                transform.localScale = (Vector2.SmoothDamp(transform.localScale, pulseScaleVector, ref scale, timeToPulse-timePulsed/2));
            }
            else {
                transform.localScale = (Vector2.SmoothDamp(transform.localScale, originalScale, ref scale, timeToPulse));
            }
            timeToPulse -= Time.deltaTime;

        }

    }
}
