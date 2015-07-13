using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {

	public GameObject splashes;

	void OnTriggerEnter() {
		Instantiate (splashes, transform.position, Quaternion.identity);
	}
}
