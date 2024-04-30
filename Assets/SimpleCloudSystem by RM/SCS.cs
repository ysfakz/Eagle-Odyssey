using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCS : MonoBehaviour {

	public Transform Player ;
	public  float CloudsSpeed;
	private Vector3 previousPlayerPosition;

	private void Start() {
		if (Player != null)
            previousPlayerPosition = Player.position;
	}

	private void Update () {
		if (!Player)
			return;

		// gameObject.transform.position = Player.transform.position;
		// transform.Rotate(0,Time.deltaTime*CloudsSpeed ,0); 

        float verticalMovement = Player.position.y - previousPlayerPosition.y;
        transform.Translate(0f, -verticalMovement, 0f);
        transform.Rotate(0, Time.deltaTime * CloudsSpeed, 0);
        previousPlayerPosition = Player.position;
	}

}
