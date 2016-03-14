using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {


	public Inventory inv;

	// Use this for initialization
	void Start () {






	}

	// Update is called once per frame
	void Update () {


	}

	public void OnTriggerEnter(Collider other) {

			if (other.CompareTag("Player"))
			{
					Debug.Log("Get items");
					Destroy(this.gameObject);
			
					inv.AddItem(1);

			}

	}

}
