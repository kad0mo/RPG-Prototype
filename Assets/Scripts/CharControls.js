#pragma strict

var moveSpeed: float = 1.0;

var normalSpeed:float = 10.0f;
var runSpeed:float = 15.0f;

var isDashing: boolean = false;
var jumpForce: float = 155.0;
var rotateSpeed: float = 20.0f;
var yGravity: float = -5.0;
var turnSmoothing: float = 15.0f;

var canDoubleJump: boolean = false;
var canJump: boolean = true;

var newDirection : Vector3;
var forward : Vector3;


var comboTimer:float = 0.7f;
var currentComboState = 0;
var comboSecondStage:boolean = false;
var comboThirdStage:boolean = false;
var ActivateTimerToReset: boolean = false;

var comboLockDownTimer:float = 0.4f;
var comboLockDownReset:boolean = false;
var comboLockCount:boolean = false;

var Distance : float;
var attackDistance: float = 15;
var hit: RaycastHit;
var rb: Rigidbody;
var attackForce: float = 2.0;

private var current : int = 0;
var locked : boolean = false;
var enemyLocations : GameObject[];
var closest : GameObject;
var activeIcon : GameObject; //Current targeted enemy indicator

var shiftSpeed: float = 2.0;
var attackLine: Vector3;
var shifting: boolean = false;

var AttackColliders:AttackColliders;
var chargeCounter:float = 0.0;
var chargeTime:float = 1.0;
var chargeTrigger:boolean = false;
var canChargeAttack:boolean = false;
var chargeAttackSpeed:float = 5.0f;
var isCharging:boolean = false;

var projectile:GameObject;

var combat:boolean = false;

  function Start () {

    rb = GetComponent.<Rigidbody>();

    GetComponent.<Animation>()["Armature|Attackk.002"].layer = 1;
    GetComponent.<Animation>()["Armature|Blerf"].layer = 1;
    GetComponent.<Animation>()["Armature|Swordattack_01"].layer = 1;
    GetComponent.<Animation>()["Armature|Swordattack_02"].layer = 1;
    GetComponent.<Animation>()["Armature|Swordattack_02"].layer = 1;
    GetComponent.<Animation>()["Armature|Jumping"].layer = 1;

    moveSpeed = normalSpeed;
    comboLockDownTimer = 0;
}


function Update () {

    newDirection = Camera.main.transform.TransformDirection(Vector3.forward);
    Physics.gravity = Vector3(0, yGravity, 0);


    PlayerControls();
    newCombo();
    lockDown();
    shiftMovement();
    ResetComboState(ActivateTimerToReset);
    ComboLockDown(comboLockDownReset);


}

function Awake () {


    transform.forward = Vector3(newDirection[0], 0, newDirection[2]);


}

function Combat () {

    combat = true;
    Debug.Log("Combat activated");


}

function lockDown ()

{

      if (closest != null && locked)

      {

          activeIcon.SetActive(true);
          activeIcon.transform.position.y = (closest.transform.position.y + 5);
          activeIcon.transform.position.x = closest.transform.position.x;
          activeIcon.transform.position.z = closest.transform.position.z;


      }
    else
    {
    activeIcon.SetActive(false);
    }


    if(Input.GetButtonDown("Lock"))
    {
      //Looks for the closest enemy
      Debug.Log("Lock painettu alas.");
      FindClosestEnemy();
      locked = !locked;

    }
    if(locked)
    {
      //If there aren't any enemies (or the player killed the last one targeted) make sure that the lock is false
      if (closest == null || !closest)
      {
          activeIcon.SetActive(false);
          locked = false;
          closest = null;


      }
      /* Fixattu versio tästä myöhemmin
      if (playerController.isAttacking)

      */
          transform.LookAt(Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z));

    }


}

function PlayerControls () {

    var moveHorizontal = Input.GetAxisRaw ("Horizontal");
    var moveVertical = Input.GetAxisRaw ("Vertical");


    var movement = Vector3(moveHorizontal, 0.0f,  moveVertical);

    movement = Camera.main.transform.TransformDirection(movement);
    movement.y = 0.0f;





        if(moveHorizontal != 0 || moveVertical != 0 && comboLockDownTimer <= 0)
        {



            var targetRotation = Quaternion.LookRotation(movement, Vector3.up);

            // Create a rotation that is an increment closer to the target rotation from the player's rotation.
            var newRotation = Quaternion.Lerp(GetComponent.<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

            // Change the players rotation to this new rotation.
            GetComponent.<Rigidbody>().MoveRotation(newRotation);

              if(isDashing) {

                GetComponent.<Animation>().CrossFade("Armature|Run", 0.2);

              } else {

              GetComponent.<Animation>().CrossFade("Armature|Walking_block", 0.2);

              }

              if(locked && Input.GetButtonDown("SlideButton")) {

                moveSpeed = 20;
                transform.Translate (movement * moveSpeed * Time.deltaTime, Space.World);
                yield WaitForSeconds(0.15);
                moveSpeed = normalSpeed;

              } else {

                transform.Translate (movement * moveSpeed * Time.deltaTime, Space.World);

              }

          } else {

            if(locked) {

              GetComponent.<Animation>().CrossFade("Armature|Swordidle", 0.2);

            } else {

              GetComponent.<Animation>().CrossFade("Armature|Idle", 0.2);

            }

          }

         if(Input.GetButtonDown("Dashbutton") && canJump)
         {

            isDashing = true;
            moveSpeed = runSpeed;

             //Kiihdytys?

         }

        if(Input.GetButtonUp("Dashbutton"))
        {

            isDashing = false;
            moveSpeed = normalSpeed;

        }

        if(Input.GetButton("Jump") && canJump)

        {

            this.GetComponent.<Rigidbody>().velocity.y += jumpForce;
            canJump = false;
            GetComponent.<Animation>().CrossFade("Armature|Jumping", 0.05);
            yield WaitForSeconds(0.3);
            canDoubleJump = true;
        }

          if(Input.GetButton("Jump") && canDoubleJump) {

            canDoubleJump = false;
            this.GetComponent.<Rigidbody>().velocity.y += jumpForce;

          }



        if(Input.GetMouseButtonDown(1) && locked && !shifting) {



          Instantiate(projectile, Vector3(transform.position.x, transform.position.y + 2, transform.position.z) , transform.rotation);

          /*
          Debug.Log("Close distance");
          shifting = true;
          */
        }




}

function shiftMovement() {

    if(shifting && locked)
    {

      attackLine = Vector3(0.0f, 0.0f, closest.transform.position.z);
      transform.Translate(attackLine * shiftSpeed * Time.deltaTime);

      var distance:float = Vector3.Distance(transform.position, closest.transform.position);

      Debug.Log(distance);
      if(distance < 2.5)
      {
        shifting = false;
        return;

      }

      yield WaitForSeconds(0.5);
      shifting = false;


    }

}

function newCombo () {

    if(Input.GetMouseButton(0))
      {
        isCharging = true;

      }

    if(chargeCounter > chargeTime)

        {
          Debug.Log("Charged.");
          canChargeAttack = true;
          chargeCounter = 0;
    }

    if(isCharging)
    {

        chargeCounter += Time.deltaTime;
    } else
    {
        chargeCounter = 0;
    }

    if(Input.GetMouseButtonUp(0) && canChargeAttack)
    {
      isCharging = false;
      chargeTrigger = true;
      canChargeAttack = false;
      yield WaitForSeconds(0.2);
      AttackColliders.disableColliders();
      chargeTrigger = false;
    } else {

      isCharging = false;


    }

      if(chargeTrigger && locked)
      {
        GetComponent.<Animation>().CrossFade("Armature|Swordattack_01", 0.05);
        AttackColliders.enableColliders();
        attackLine = Vector3(0.0f, 0.0f, closest.transform.position.z);
        transform.Translate(attackLine * shiftSpeed * Time.deltaTime);
        chargeCounter = 0.0;

      }
      if(chargeTrigger && !locked)
      {
        GetComponent.<Animation>().CrossFade("Armature|Swordattack_01", 0.05);
        AttackColliders.enableColliders();
        attackLine = Vector3(0.0f, 0.0f, 1);
        transform.Translate(attackLine * chargeAttackSpeed * Time.deltaTime);
        chargeCounter = 0.0;

      }





    if(Input.GetMouseButtonDown(0)) {


        if(currentComboState == 0 && comboTimer == 1.0f) {


          AttackColliders.enableColliders();

          Debug.Log("1 hit");
          currentComboState++;
          ActivateTimerToReset = true;
          GetComponent.<Animation>().CrossFade("Armature|Swordattack_01", 0.05);
          rb.AddForce(transform.forward * attackForce);
          moveSpeed = 0;

          comboLockDownReset = true;
          comboSecondStage = true;


          yield WaitForSeconds(0.45);

          if(Input.GetMouseButtonDown(0) && comboTimer < 0.8) {

            return;

          } else {

          AttackColliders.disableColliders();
          comboSecondStage = false;
          return;

          }

        }

        if(currentComboState == 1 && comboSecondStage && comboTimer < 0.8f) {
          Debug.Log("2 hit, The combo Should Start");
          moveSpeed = 0;
          currentComboState++;
          comboSecondStage = false;

          AttackColliders.enableColliders();
          comboLockDownReset = true;
          GetComponent.<Animation>().CrossFade("Armature|Swordattack_02", 0.05);

          rb.AddForce(transform.forward * attackForce);
          comboThirdStage = true;


          yield WaitForSeconds(0.45);
          AttackColliders.disableColliders();
          comboThirdStage = false;


          return;

        }

        if(currentComboState == 2 && comboThirdStage && comboTimer < 0.6f) {
          Debug.Log("3 hit, Last Part of the basic combo.");
          moveSpeed = 0;
          comboThirdStage = false;
          comboLockDownReset = true;
          AttackColliders.enableColliders();
          GetComponent.<Animation>().CrossFade("Armature|Swordattack_01", 0.05);
          rb.AddForce(transform.forward * attackForce);

          yield WaitForSeconds(0.55);

          AttackColliders.disableColliders();




          return;

        }


    }

}

function ComboLockDown(Reset) {

  if(Reset) {

    comboLockDownTimer = 0.4f;
    comboLockDownReset = false;
    comboLockCount = true;
  }
  if(comboLockCount){


    comboLockDownTimer -= Time.deltaTime;

  }
  if(comboLockDownTimer <= 0) {

    comboLockCount = false;
    comboLockDownReset = false;
    if(!isDashing) {

      moveSpeed = normalSpeed;

    }
  }

}


function ResetComboState(Timer) {

    if (Timer) {

      comboTimer -= Time.deltaTime;


    }

    if (comboTimer <= 0) {

      currentComboState = 0;
      ActivateTimerToReset = false;
      comboTimer = 1.0f;


    }



}


    function FindClosestEnemy () : GameObject
        {
            // Find all game objects with tag Enemy
            enemyLocations = GameObject.FindGameObjectsWithTag("enemy");
            //var closest : GameObject;
            var distance = Mathf.Infinity;
            var position = transform.position;
            // Iterate through them and find the closest one
            for (var go : GameObject in enemyLocations)
            {
                    Debug.Log(enemyLocations);
                    var diff = (go.transform.position - position);
                    var curDistance = diff.sqrMagnitude;


                 if (curDistance < distance)
            {
                     closest = go;
                     distance = curDistance;
            }
    }
    return closest;

    }

function OnCollisionEnter(other : Collision)
{


    if(other.transform.tag == "ground"){ //If the player lands on ground
        canJump = true; //allow him to jump again
        GetComponent.<Animation>().CrossFade("Armature|Idle");
        canDoubleJump = false;
    }
}
