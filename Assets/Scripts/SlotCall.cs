using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotCall : MonoBehaviour {

	GameObject inv;
	Inventory invScript;
	inventoryController invControlScript;
	ItemData itemData;
	public GameObject itemChild;
	public GameObject itemChildData;
	public string itemDescription;
	GameObject descriptionPanel;

	public bool itemChosen;

	public GameObject profilePanel;
	public GameObject slotPanel;

	weaponSlot weapon;
	public GameObject weaponPanel;

	armorSlot armor;
	public GameObject armorPanel;


	// Use this for initialization
	void Awake() {


		slotPanel = GameObject.Find("Slot Panel");


		inv = GameObject.Find("Inventory");
		invScript = inv.GetComponent<Inventory>();
		invControlScript = inv.GetComponent<inventoryController>();

		descriptionPanel = GameObject.Find("Description Panel");

		itemChosen = false;
	}

	// Update is called once per frame
	void Update () {


	}

	void Start()
	{

		descriptionPanel = GameObject.Find("Description Panel");

	}

	public void inventoryCall()
	{


		itemChild = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;

		itemDescription = itemChild.GetComponent<Text>().text;

		descriptionPanel.transform.GetChild(0).GetComponent<Text>().text = itemDescription.ToString();





	}


	public void itemCancel()
	{

		itemChosen = false;

	}

	public void equipCancel()
	{
		invControlScript.equipCancel();
		invScript.normalizeItemSet();
	}

	public void selectItem() {

		itemChildData = this.gameObject.transform.GetChild(0).gameObject;
		itemData = itemChildData.GetComponent<ItemData>();

		if(itemData.usable)
		{
			itemChosen = true;
			Debug.Log(itemData.health);
			invControlScript.chooseItem();
			invControlScript.containStats(itemData.health, itemData.mana);

			invControlScript.getItemSlotData(this.gameObject);

		}
		if(itemData.weapon && invControlScript.choosingWeapon)
		{
			weaponPanel = GameObject.Find("WeaponPanel");
			weapon = weaponPanel.GetComponent<weaponSlot>();




			weapon.getWeaponId(itemData.id);
			weapon.addEquippedItem();
			invScript.normalizeItemSet();

			invControlScript.getItemSlotData(this.gameObject);
			invControlScript.weaponSelect();

		}

		if(itemData.armor && invControlScript.choosingArmor)
		{
			armorPanel = GameObject.Find("ArmorPanel");
			armor = armorPanel.GetComponent<armorSlot>();

			armor.getArmorId(itemData.id);
			armor.addEquippedItem();
			invScript.normalizeItemSet();

			invControlScript.getItemSlotData(this.gameObject);
			invControlScript.armorSelect();

		}else{

			Debug.Log("Cannot use this");

		}


	}

}
