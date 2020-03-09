using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
	
	[SerializeField] GameObject cardBack;
	[SerializeField] GameObject cardFront;
	
	//[SerializeField] GameManager manager;
	
	
	public void OnMouseDown() {
			if (cardBack.activeSelf) {
				cardBack.SetActive(false);
				cardFront.SetActive(true);
			}
			else if (!cardBack.activeSelf) {
				cardBack.SetActive(true);
				cardFront.SetActive(false);
			}
			
	}
	
	public void OnMouseDrag() {
		//OnMouseDown();
	}
	
	public void OnMouseEnter() {
		if (Input.GetMouseButton(0)) {
				OnMouseDown();
		}
	}
	
	
    // Start is called before the first frame update
    void Start()
    {
        cardBack.SetActive(true);
		cardFront.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
