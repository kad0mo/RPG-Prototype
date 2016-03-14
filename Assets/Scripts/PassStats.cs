using UnityEngine;
using System.Collections;

public class PassStats : MonoBehaviour {

	inventoryController invController;
	GameObject inventory;
	StatController statControl;
	GameObject player;

	// Use this for initialization
	void Awake () {

		inventory = GameObject.Find("Inventory");
		invController = inventory.GetComponent<inventoryController>();

		player = GameObject.Find("Player");
		statControl = player.GetComponent<StatController>();

	}

	// Update is called once per frame
	void Update () {

	}

	public void passStats()
	{
		if(invController.itemChosen)
		{
			Debug.Log("Passing shiet..");

		}

	}

}
