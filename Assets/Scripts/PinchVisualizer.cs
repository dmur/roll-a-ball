using UnityEngine;
using System.Collections;
using Leap;

public class PinchVisualizer : MonoBehaviour {
	
	public LineRenderer pinchSpan;
	public LineRenderer toBallThumb;
	public LineRenderer toBallIndex;
	public LineRenderer inBall;
	public GameObject[] tipLights;


	void Update() {
		HandModel hand_model = GetComponent<HandModel>();
		Hand leap_hand = hand_model.GetLeapHand();
		Vector3 thumbTipPos = hand_model.fingers[0].GetTipPosition();
		Vector3 indexTipPos = hand_model.fingers[1].GetTipPosition();

		if (leap_hand == null) return;

		for (int k = 0; k < 2; ++k) {
			tipLights[k].transform.position = hand_model.fingers[k].GetTipPosition();

		}

		pinchSpan.SetPosition(0, thumbTipPos);
		pinchSpan.SetPosition(1, indexTipPos);
	}
}

