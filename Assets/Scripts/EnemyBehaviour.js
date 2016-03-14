#pragma strict

var Target: GameObject;
var locked: boolean = false;
var moveSpeed: float = 10.0;
var chargeSpeed: float = 0.05f;
var attackLine: Vector3;
var enterCombat: boolean = false;
var attackTimer:float = 0.0f;
var attackRate:float = 4.0f;
var enemyHealth:int = 4;
var enemyAudio : AudioSource;
var damageClip : AudioClip;

var rb: Rigidbody;
var knockForce: float = 2.0;

var distance:float;



function Start () {

  rb = GetComponent.<Rigidbody>();
  GetComponent.<Animation>()["Armature|Combat_Guard"].layer = 1;
  GetComponent.<Animation>()["Armature|Blerf"].layer = 1;
  GetComponent.<Animation>()["Armature|Combat_Idle"].layer = 1;

}

function Update () {

    distance = Vector3.Distance(transform.position, Target.transform.position);



    if(locked) {



        Combat();



    } else {

        GetComponent.<Animation>().CrossFade("Armature|Idle", 0.2);

    }

}

  function OnTriggerStay(other:Collider)
  {
    if(other.gameObject.CompareTag("Player"))
    {

        locked = true;
    }
  }


function OnTriggerEnter(other:Collider) {

    if(other.gameObject.CompareTag("Player"))
    {

        locked = true;

    }


}

    function OnTriggerExit(other:Collider) {

        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has exited field of vision. Return to non combat state.");
            moveSpeed = 2;
            yield WaitForSeconds(2);
            moveSpeed = 6;
            locked = false;

        }


    }

    function takeDamage(damage:int) {


      enemyHealth = enemyHealth - damage;
      rb.AddForce(-transform.forward * knockForce);
      GetComponent.<Animation>().CrossFade("Armature|Combat_Guard", 0.2);
      attackTimer = 0;

      enemyAudio.Play ();

      if(enemyHealth < 1) {

        Debug.Log("Enemy Dies");
        Destroy(gameObject);

      }


    }

        function Combat () {

            if(!enterCombat) {

                transform.LookAt(Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));

                var delta = Target.transform.position - transform.position;
                delta.Normalize();
                Debug.Log("Delta:" + delta);

                if(distance > 2) {
                    transform.position = transform.position + (delta * moveSpeed * Time.deltaTime);

                }
            }

            if(distance < 2.5 && attackTimer > attackRate) {



            }

            if(distance < 13) {

              moveSpeed = 0;
              GetComponent.<Animation>().CrossFade("Armature|Combat_Idle", 0.05);

            } else {

              GetComponent.<Animation>().CrossFade("Armature|Walking_block", 0.2);
              moveSpeed = 8;

            }

            /*

            if(distance < 8.5) {

            attackTimer += Time.deltaTime;


            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            GetComponent.<Animation>().CrossFade("Armature|Combat_Guard", 0.2);

            if(attackTimer > attackRate) {

                enterCombat = true;

                if(enterCombat) {
                GetComponent.<Animation>().CrossFade("Armature|Blerf", 0.05);
                moveSpeed = 9;
                attackLine = Vector3(0.0f, transform.position.y, Target.transform.position.z);
                transform.Translate(attackLine * chargeSpeed * Time.deltaTime);

                yield WaitForSeconds(2);
                enterCombat = false;
                attackTimer = 0;

              }


            }



          }

          */

        }
