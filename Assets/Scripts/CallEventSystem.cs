using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CallEventSystem : MonoBehaviour {

	GameObject firstProfile;
	GameObject slotPanel;
	GameObject firstItem;

	GameObject eqPanel;
	GameObject selectedEq;

	// Use this for initialization
	void Awake () {

		firstProfile = GameObject.Find("MainChara");
		slotPanel = GameObject.Find("Slot Panel");
		eqPanel = GameObject.Find("Equipment Panel");



	}

	// Update is called once per frame
	void Update () {

	}

	public void eventSystemSelectProfile()
	{

		  EventSystem.current.SetSelectedGameObject(firstProfile);

	}
	public void eventSystemSelectItems()
	{
			firstItem = slotPanel.transform.GetChild(0).gameObject;
			EventSystem.current.SetSelectedGameObject(firstItem);
	}
	public void eventSystemEquipment(int equipNumber)
	{
		selectedEq = eqPanel.transform.GetChild(equipNumber).gameObject;
		EventSystem.current.SetSelectedGameObject(selectedEq);

		return;
	}
}
