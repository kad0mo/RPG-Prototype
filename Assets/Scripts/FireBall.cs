using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {


	private float speed = 20.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		transform.position += Time.deltaTime * speed * transform.forward;

	}
}
