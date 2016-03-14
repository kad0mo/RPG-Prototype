using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class inventoryController : MonoBehaviour {

	private bool screenActive;
	public GameObject mainScreen;
	public GameObject Player;
	public GameObject profilePanel;
	public GameObject slotPanel;
	public GameObject skillPanel;

	public CanvasGroup _canvasGroup;
	public GameObject naviPanel;
	public GameObject Profile;
	public GameObject secondProfile;

	public Button profileButton;
	public Button secondProfileButton;

	public Button firstProfile;

	CharController charControls;

	CallEventSystem callEventSys;
	StatController statControls;

	public bool itemChosen;
	public bool choosingWeapon;
	public bool choosingArmor;

	public int healthContainer;
	public int manaContainer;


	public GameObject descriptionPanel;

	public GameObject itemChildData;
	public GameObject selectedSlot;
	public ItemData itemData;

 	public GameObject characontroller;
	Characters character;


	public GameObject charaName;
	public GameObject charaPicture;

	public GameObject charaLevel;
	public GameObject charStr;
	public GameObject charMag;
	public GameObject charSpd;

	public GameObject curExp;
	public GameObject nextExp;

	public GameObject mainChara;
	public GameObject secondChara;

	public GameObject weaponPanel;
	public GameObject armorPanel;
	weaponSlot weapon;
	armorSlot armor;

	public GameObject inventory;
	Inventory inv;

	public Sprite characterSprite;
	public Image image;
	public GameObject namePanel;


	// Use this for initialization

	void Awake()
	{
		Player = GameObject.Find("Player");

		mainChara = GameObject.Find("MainChara");
		secondChara = GameObject.Find("SecondChara");


		statControls = secondChara.GetComponent<StatController>();
		charControls = Player.GetComponent<CharController>();

		profilePanel = GameObject.Find("Profile Panel");
		slotPanel = GameObject.Find("Slot Panel");
		mainScreen = GameObject.Find("Inventory Panel");
		naviPanel = GameObject.Find("Navigation Panel");
		skillPanel = GameObject.Find("Skill Panel");
		Profile = naviPanel.transform.GetChild(0).gameObject;
		profileButton = Profile.GetComponent<Button>();
		callEventSys = Profile.GetComponent<CallEventSystem>();


		firstProfile = profilePanel.transform.GetChild(0).gameObject.GetComponent<Button>();

		secondProfile = GameObject.Find("SecondChara");
		secondProfileButton = profilePanel.transform.GetChild(1).gameObject.GetComponent<Button>();

		descriptionPanel = GameObject.Find("Description Panel");

		slotPanel.SetActive(false);
		profilePanel.SetActive(false);

		itemChosen = false;
		choosingWeapon = false;
		choosingArmor = false;

		characontroller = GameObject.Find("Characters");
		character = characontroller.GetComponent<Characters>();

		charaName = GameObject.Find("CharaName");
		charaPicture = GameObject.Find("CharaPic");

		charaLevel = GameObject.Find("LevelText");

		charStr = GameObject.Find("Strength");
		charMag = GameObject.Find("Mag");
		charSpd = GameObject.Find("Speed");

		curExp = GameObject.Find("CurrentExp");
		nextExp = GameObject.Find("NextLevelExp");

		weaponPanel = GameObject.Find("WeaponPanel");
		weapon = weaponPanel.GetComponent<weaponSlot>();

		armorPanel = GameObject.Find("ArmorPanel");
		armor = armorPanel.GetComponent<armorSlot>();

		inventory = GameObject.Find("Inventory");
		inv = inventory.GetComponent<Inventory>();



	}

	void Start () {





		screenActive = false;


	}

	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("ToggleInventory"))
		{
				screenActive = !screenActive;
				_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
				profileButton.interactable = true;
				itemChosen = false;
		}

		if(screenActive)
		{
			screenActive = true;
			mainScreen.SetActive(true);
			Time.timeScale = 0.3f;
			charControls.activateInventory();
		}
		else
		{
			mainScreen.SetActive(false);
			charControls.deActivateInventory();
			Time.timeScale = 1.0f;
		}


	}

	public void chooseItem()
	{
		slotPanel.SetActive(false);
		profilePanel.SetActive(true);
		callEventSys.eventSystemSelectProfile();
		_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
		itemChosen = true;
		profileButton.interactable = false;
		Navigation verticalNav = new Navigation();
		verticalNav.mode = Navigation.Mode.Vertical;
		firstProfile.navigation = verticalNav;

	}

	public void weaponSelect()
	{

		if(choosingWeapon)
		{


			itemData.amount -= 1;


			if(itemData.amount == 0)
			{
				Destroy (selectedSlot);
				inv.refreshItemList();
			}

			weapon.showEquip(weapon.characterId);

			profilePanel.SetActive(true);
			slotPanel.SetActive(false);
			choosingWeapon = false;

			Debug.Log("Current amount:" + itemData.amount);
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;

			callEventSys.eventSystemEquipment(0);
			weapon.afterSelecting = true;



		}

	}

	public void armorSelect()
	{

		if(choosingArmor)
		{


			itemData.amount -= 1;


			if(itemData.amount == 0)
			{
				Destroy (selectedSlot);
				inv.refreshItemList();
			}

			armor.showEquip(armor.characterId);

			profilePanel.SetActive(true);
			slotPanel.SetActive(false);
			choosingArmor = false;

			Debug.Log("Current amount:" + itemData.amount);
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;

			callEventSys.eventSystemEquipment(1);
			armor.afterSelecting = true;

		}

	}

	public void equipCancel()
	{
		if(choosingWeapon || choosingArmor)
		{
			if(choosingWeapon)
			{
				callEventSys.eventSystemEquipment(0);
			}
			if(choosingArmor)
			{
				callEventSys.eventSystemEquipment(1);
			}
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
			profilePanel.SetActive(true);
			slotPanel.SetActive(false);
			choosingWeapon = false;
			choosingArmor = false;
		}

	}

	public void chooseWeapon()
	{
		choosingWeapon = true;
		profilePanel.SetActive(false);
		slotPanel.SetActive(true);
		callEventSys.eventSystemSelectItems();
		_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
	}
	public void chooseArmor()
	{
		choosingArmor = true;
		profilePanel.SetActive(false);
		slotPanel.SetActive(true);
		callEventSys.eventSystemSelectItems();
		_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
	}



	public void containStats(int health, int mana)
	{

		healthContainer = health;
		manaContainer = mana;

		Debug.Log("Contains hp:" + healthContainer);
		Debug.Log("Contains mana:" + manaContainer);

	}



	public void profileCancel()
	{
		if(itemChosen)
		{
			profilePanel.SetActive(false);
			slotPanel.SetActive(true);
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
			profileButton.interactable = true;
			callEventSys.eventSystemSelectItems();
			itemChosen = false;

			Navigation explicitNav = new Navigation();
			explicitNav.mode = Navigation.Mode.Explicit;
			explicitNav.selectOnDown = secondProfileButton;
			explicitNav.selectOnUp = profileButton;
			firstProfile.navigation = explicitNav;

			healthContainer = 0;
			manaContainer = 0;

			Debug.Log("Contains hp:" + healthContainer);
			Debug.Log("Contains mana:" + manaContainer);
		}
	}

	public void profileSelect(int id)
	{
		if(itemChosen)
		{

			character.characters[id].CurHp += healthContainer;
			character.characters[id].CurMana += manaContainer;
			statControls.updateSliders();

			profilePanel.SetActive(false);
			slotPanel.SetActive(true);
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
			profileButton.interactable = true;

			Navigation explicitNav = new Navigation();
			explicitNav.mode = Navigation.Mode.Explicit;
			explicitNav.selectOnDown = secondProfileButton;
			explicitNav.selectOnUp = profileButton;
			firstProfile.navigation = explicitNav;

			itemChosen = false;

			healthContainer = 0;
			manaContainer = 0;



			itemData.amount -= 1;

			Debug.Log("Current amount:" + itemData.amount);
			if(itemData.amount == 0)
			{
				Destroy (selectedSlot);

			}
			inv.refreshItemList();
			callEventSys.eventSystemSelectItems();

		} else
		{
			inv.refreshItemList();
		callEventSys.eventSystemEquipment(0);
	  }


	}

	public void getItemSlotData(GameObject slot)
	{
		selectedSlot = slot;
		itemChildData = slot.gameObject.transform.GetChild(0).gameObject;
	  itemData = itemChildData.GetComponent<ItemData>();
		Debug.Log(itemData.amount);

	}

	public void getProfile()
	{


			slotPanel.SetActive(false);
			skillPanel.SetActive(false);
			profilePanel.SetActive(true);
			descriptionPanel.transform.GetChild(0).GetComponent<Text>().text = "Party member profiles.";

	}

	public void getItems()
	{

			descriptionPanel.transform.GetChild(0).GetComponent<Text>().text = "Items.";

			profilePanel.SetActive(false);
			slotPanel.SetActive(true);
			skillPanel.SetActive(false);


	}
	public void getSkills()
	{
			profilePanel.SetActive(false);
			slotPanel.SetActive(false);
			skillPanel.SetActive(true);
			descriptionPanel.transform.GetChild(0).GetComponent<Text>().text = "Party member skills.";
	}

	public void showCharacter(int id)
	{
		profileButton.interactable = true;

		characterSprite = Resources.Load<Sprite>("Sprites/Characters/" + id);

		namePanel.transform.GetChild(1).GetComponent<Image>().sprite = characterSprite;






		Debug.Log(character.characters[id].Mag);
		charaName.GetComponent<Text>().text = character.characters[id].Name.ToString();
		charaLevel.GetComponent<Text>().text = ("LEVEL " + character.characters[id].Level);

		charStr.transform.GetChild(0).GetComponent<Text>().text = ("STR " + character.characters[id].Str);
		charMag.transform.GetChild(0).GetComponent<Text>().text = ("MAG " + character.characters[id].Mag);
		charSpd.transform.GetChild(0).GetComponent<Text>().text = ("SPD " + character.characters[id].Spd);

		curExp.transform.GetChild(0).GetComponent<Text>().text = ("EXP " + character.characters[id].Exp);

	}
}
