using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{

    [SerializeField] Image image;

    public void fadeIn(float time) {
        image.CrossFadeAlpha(255, time, false);
    }

    public void fadeOut(float time) {
        Debug.Log("Fading Out");
        image.CrossFadeAlpha(0, time, false);
    }

    void Start() {
        image = gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
    }
 
}
