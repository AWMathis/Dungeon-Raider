using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    public bool leftMouseDown = false;
    public bool leftMouseJustUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftMouseDown = Input.GetMouseButton(0);
        leftMouseJustUp = Input.GetMouseButtonUp(0);
    }
}
