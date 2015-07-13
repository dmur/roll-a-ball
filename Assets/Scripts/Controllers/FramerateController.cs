using UnityEngine;
using System.Collections;

public class FramerateController : MonoBehaviour {

	private int updateCount;
	// Update is called once per frame
	void Start () {
		updateCount = 0;
	}

	void Update () {
		if (updateCount % 10 == 0) {
			TextMesh tm = gameObject.GetComponent ("TextMesh") as TextMesh;
			tm.text = "Framerate\n" + (1 / Time.deltaTime);
		}
		updateCount++;
	}
}
