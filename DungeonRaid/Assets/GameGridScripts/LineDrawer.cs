using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
	public void DrawLines(List<GameObject> selected) {
		var lineRender = gameObject.GetComponent<LineRenderer>();

		for (int i = 0; i < 50; i++) {
			Vector3 outOfBounds = new Vector3(0, 0, 100);
			lineRender.SetPosition(i, outOfBounds);
		}

		if (selected.Count > 1) {
			for (int i = 0; i < selected.Count; i++) {
				Vector3 selectedPosition = selected[i].transform.position;
				Vector3 linePosition = new Vector3(selectedPosition.x, selectedPosition.y, selectedPosition.z - 1);
				lineRender.SetPosition(i, linePosition);
			}
		}
	}
}
