
using UnityEngine;
using System.Collections;
using Leap;

public class BetterPinch : MonoBehaviour {


  public const float TRIGGER_DISTANCE_RATIO = 0.7f;

  // The stiffness of the spring force used to move the object toward the hand.
  public float forceSpringConstant = 100.0f;
  // The maximum range at which an object can be picked up.
  public float magnetDistance = 2.0f;

  protected bool pinching_;
  protected Collider grabbed_;


	public LayerMask pinchIgnoreList;

	public LineRenderer[] spanLines;
	public GameObject[] tipLights;







 	void Start() {
		pinching_ = false;
		grabbed_ = null;
	}


	// Finds an object to grab and grabs it.
	void OnPinch(Vector3 pinch_position) {
		pinching_ = true;

		// Check if we pinched a movable object and grab the closest one that's not part of the hand.
		Collider[] close_things = Physics.OverlapSphere(pinch_position, magnetDistance);
		Vector3 distance = new Vector3(magnetDistance, 0.0f, 0.0f);

		for (int j = 0; j < close_things.Length; ++j) {
			Vector3 new_distance = pinch_position - close_things[j].transform.position;
			if (close_things[j].GetComponent<Rigidbody>() != null && new_distance.magnitude < distance.magnitude &&
				!close_things[j].transform.IsChildOf(transform)) {
				grabbed_ = close_things[j];
				distance = new_distance;
			}
		}
	}


	// Clears the pinch state.
	void OnRelease() {
		grabbed_ = null;
		pinching_ = false;
	}





	void FixedUpdate() {

		HandModel hand_model = GetComponent<HandModel>();
		Hand leap_hand = hand_model.GetLeapHand();

		Vector3 thumbPos = hand_model.fingers[0].GetTipPosition();
		Vector3 indexPos = hand_model.fingers[1].GetTipPosition();

		Vector3 pinchCenter = (thumbPos + indexPos)/2;

		float pinchSpan = Vector3.Distance(thumbPos, indexPos);

		Vector3 thumbTargetPos;
		Vector3 indexTargetPos;
		
		RaycastHit thumbToBall;
		RaycastHit indexToBall;

		bool hitFromThumb = Physics.Raycast(thumbPos, indexPos, out thumbToBall, pinchSpan, ~pinchIgnoreList);
		bool hitFromIndex = Physics.Raycast(indexPos, thumbPos, out indexToBall, pinchSpan, ~pinchIgnoreList);

		Debug.Log (hitFromThumb);
		Debug.Log (hitFromIndex);

		if (thumbToBall.collider != null) {
			thumbTargetPos = thumbToBall.point;
			indexTargetPos = indexToBall.point;

			Debug.Log(thumbTargetPos);
			Debug.Log(indexTargetPos);


			//Debug.DrawRay(thumbPos, thumbToBall.point, Color.cyan);
			//Debug.DrawRay(indexPos, indexToBall.point, Color.cyan);

			spanLines[0].SetPosition(1, thumbToBall.point);
			spanLines[1].SetPosition(1, indexToBall.point);

		}

		else {
			spanLines[0].SetPosition(1, pinchCenter);
			spanLines[1].SetPosition(1, pinchCenter);
		}

			spanLines[0].SetPosition(0, thumbPos);
			spanLines[1].SetPosition(0, indexPos);
		

		
		for (int k = 0; k < 2; ++k) {
			tipLights[k].transform.position = hand_model.fingers[k].GetTipPosition();
			
		}














	/*
    bool trigger_pinch = false;
    HandModel hand_model = GetComponent<HandModel>();
    Hand leap_hand = hand_model.GetLeapHand();
	LineRenderer lineRenderer = GetComponent<LineRenderer>();

    if (leap_hand == null)
      return;

    // Scale trigger distance by thumb proximal bone length.
    Vector leap_thumb_tip = leap_hand.Fingers[0].TipPosition;
    float proximal_length = leap_hand.Fingers[0].Bone(Bone.BoneType.TYPE_PROXIMAL).Length;
    float trigger_distance = proximal_length * TRIGGER_DISTANCE_RATIO;

    // Check thumb tip distance to joints on all other fingers.
    // If it's close enough, start pinching.
    for (int i = 1; i < HandModel.NUM_FINGERS && !trigger_pinch; ++i) {
      Finger finger = leap_hand.Fingers[i];

      for (int j = 0; j < FingerModel.NUM_BONES && !trigger_pinch; ++j) {
        Vector leap_joint_position = finger.Bone((Bone.BoneType)j).NextJoint;
        if (leap_joint_position.DistanceTo(leap_thumb_tip) < trigger_distance)
          trigger_pinch = true;
      }
    }
	
	//for (int k = 0; k < 5; ++k) {
	//	Vector3 fingerTipPos = hand_model.fingers[k].GetTipPosition();
	//	lineRenderer.SetPosition(k, fingerTipPos);
	//}

	Vector3 pinch_position = hand_model.fingers[0].GetTipPosition();

    // Only change state if it's different.
    if (trigger_pinch && !pinching_)
      OnPinch(pinch_position);
    else if (!trigger_pinch && pinching_)
      OnRelease();

    // Accelerate what we are grabbing toward the pinch.
    if (grabbed_ != null) {
      Vector3 distance = pinch_position - grabbed_.transform.position;
      grabbed_.GetComponent<Rigidbody>().AddForce(forceSpringConstant * distance);
    } */



  }
}
