using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public int SceneNumber = 2;


	void OnTriggerEnter(Collider other) {

		if(other.gameObject.CompareTag("Player"))
		{

			Application.LoadLevel(SceneNumber);
		}
	}


}
