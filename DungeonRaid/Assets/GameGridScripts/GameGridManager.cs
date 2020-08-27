using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridManager : MonoBehaviour
{
	
	[SerializeField] int gridRows = 3;
	[SerializeField] int gridColumns = 3;
	[SerializeField] float xPadding = 3; //Horizontal distance between points so that there isn't any overlap
	[SerializeField] float yPadding = 3; //Vertical distance between points so that there isn't any overlap
	[SerializeField] float initialPadding = 1.5f; //Initial padding to use since we want the spawner to be at the center of the grid
	[SerializeField] Vector2 gamePieceScale;

	private List<Vector2> gridLocations = new List<Vector2>();
	private List<GameObject> allGamePieces = new List<GameObject>();

	[SerializeField] GameObject[] gamePiecePrefabs;

	private List<GameObject> selected = new List<GameObject>();
	private List<int> selectedIndexes = new List<int>();
	private string selectedActionType;

	[SerializeField] float spawnOffset = 15f;
	[SerializeField] float timeToFall = 0.5f;

	[SerializeField] GameObject LineDrawingGameObject;
	LineDrawer lineDrawer;

	bool paused = false;

	private int sumAttack = 0; //Sum of all sword damage plus the player's inate damage

	public int SumAttack { get { return sumAttack; } }
	public string CurrentActionType { get { return selectedActionType; } }


	//Warning, this only creates a correct layout when rows and columns are even. Other conditions will still create a grid, but it won't be centered.
	void InitializeGridLocations() {
		gridLocations.Clear();


		//Start at the game piece in the top left
		float initialX = transform.position.x + (initialPadding + (xPadding * (gridColumns/2))*-1);
		float initialY = transform.position.y + (initialPadding + (yPadding * (gridRows/2-1))); 


		//////////////CLEAN THIS SHIT UP
		//Fills in points one column at a time
		for (int x = 0; x < gridColumns; x++) {
			float tempX = initialX + (xPadding * x);
			for (int y = 0; y < gridRows; y++) {
				float tempY = initialY - (yPadding * y);
				float newTempX = transform.InverseTransformPoint(new Vector2(tempX, tempY)).x;
				float newTempY = transform.InverseTransformPoint(new Vector2(tempX, tempY)).y;
				gridLocations.Add(new Vector2(newTempX, newTempY));
			}
		}

	}
	
	void InitializeGameGrid() {
		

		for (int i = 0; i < gridLocations.Count; i++) {
			GameObject newItem = SpawnRandomGamepiece(gridLocations[i]);
			allGamePieces.Add(newItem);
			newItem.name = "" + i;
			
		}
	}

	//Spawns a gamepiece at the given location. Note that spawnLocation should be in local coordinates
	GameObject SpawnRandomGamepiece(Vector2 spawnLocation) {
		int index = Random.Range(0, gamePiecePrefabs.Length);
		GameObject temp = Instantiate(gamePiecePrefabs[index], spawnLocation, transform.localRotation, transform);
		temp.transform.localScale = gamePieceScale;
		temp.transform.localPosition = new Vector3 (spawnLocation.x, spawnLocation.y, -1);
		return temp;
	}

	public void GamePieceSelected(string type, GameObject selectedObject) {
		if (!paused) {
			selectedActionType = type;

			selected.Add(selectedObject);

			UpdateAll();
		}
	}

	public void GamePiecePassedOver(GameObject selectedObject) {
		if (!paused) {
			if (NextPieceInRange(selectedObject.transform.localPosition)) {
				if (selected.Contains(selectedObject)) {
					int index = selected.IndexOf(selectedObject);
					while (selected.Count - 1 >= index) {
						selected.RemoveAt(index);
					}
				}
				selected.Add(selectedObject);


				sumAttack = gameObject.GetComponent<Player>().Damage;
				if (selectedActionType == "Combat") {
					for (int i = 0; i < selected.Count; i++) {
						sumAttack += selected[i].GetComponent<CombatPiece>().AttackPower;
					}
				}

				UpdateAll();
			}

		}	
	}

	void MouseReleased() {
		UpdateAll();

		if (selected.Count >= 3) {
			for (int i = 0; i < selected.Count; i++) {
				selected[i].GetComponent<GamePiece>().OnUse();
			}

			ShiftPiecesDown();

			EnemyTurn();
		}
	
		selected.Clear();
		selectedActionType = "";
		sumAttack = 0;

		
		UpdateAll();
	}

	//This happens after every player action, such as selecting some pieces
	void EnemyTurn() {
		for(int i = 0; i < allGamePieces.Count; i++) {
			allGamePieces[i].GetComponent<GamePiece>().EachTurnEffect();
		}
	}

	void ShiftPiecesDown() {
		for (int i = 0; i < gridColumns; i++) {
			for (int j = gridRows-1; j >= 0; j--) {

				int index = (i * gridRows) + (j);
				if (allGamePieces[index] == null || allGamePieces[index].name == "Deleted") {

					//If you're at the top of the column, spawn a new piece for the top
					 if (j == 0) {
						GameObject newItem = SpawnRandomGamepiece(new Vector2(gridLocations[index].x, gridLocations[index].y + spawnOffset));
						newItem.name = "" + index;
						allGamePieces[index] = newItem;
						allGamePieces[index].GetComponent<GamePiece>().MoveTo(gridLocations[index], timeToFall);
					}
					else {

						int tempIndex = 0;

						//Scan through to see if there's any game pieces in the same column but above the empty space
						for (tempIndex = 1; (index-tempIndex)%gridRows != 0; tempIndex++) {
							if (allGamePieces[index-tempIndex] != null && allGamePieces[index-tempIndex].name != "Deleted") {
								break;
							}
						}

						//Debug.Log("Pulling from " + (index-tempIndex) + " to " + index);

						//If the scan gets to the top of the column and it's all empty, spawn a new piece
						if ((index - tempIndex) % gridRows == 0 && (allGamePieces[index - tempIndex] == null || allGamePieces[index - tempIndex].name == "Deleted")) {
							GameObject newItem = SpawnRandomGamepiece(new Vector2(gridLocations[index].x, gridLocations[index].y + spawnOffset));
							newItem.name = "" + index;
							allGamePieces[index] = newItem;
							allGamePieces[index].GetComponent<GamePiece>().MoveTo(gridLocations[index], timeToFall);
						}
						//Else move that piece down and check the next one up the chain
						else {
							allGamePieces[index] = allGamePieces[index - tempIndex];
							allGamePieces[index - tempIndex] = null;
							allGamePieces[index].GetComponent<GamePiece>().MoveTo(gridLocations[index], timeToFall);
						}

						//Move allGridItems[i*gridRows+j-1] to this location
						//Check allGridItem[
					} 
				}

			}
		}

	}

	bool NextPieceInRange(Vector2 nextPieceLocation) {
		Vector2 lastPieceLocation = selected[selected.Count - 1].transform.localPosition;

		float maxDistance = 0.3f;
		float distance = Vector2.Distance(nextPieceLocation, lastPieceLocation);

		return distance <= maxDistance;

	}


	public void UpdateAll() {

		GameObject[] pieces = GameObject.FindGameObjectsWithTag("GamePiece");

		foreach (GameObject piece in pieces) {
			bool currentlySelected = selected.Contains(piece);

			piece.GetComponent<GamePiece>().UpdateState(selectedActionType, currentlySelected, sumAttack);
		}


		lineDrawer.DrawLines(selected);
	}

	public void Pause() {
		paused = true;
	}

	public void UnPause() {
		paused = false;
	}

	// Start is called before the first frame update
	void Start() {
		lineDrawer = LineDrawingGameObject.GetComponent<LineDrawer>();
        InitializeGridLocations();
		InitializeGameGrid();
    }



    // Update is called once per frame
    void Update() {
        if (gameObject.GetComponent<MouseManager>().leftMouseJustUp == true) {
			MouseReleased();
		}
	}
}
