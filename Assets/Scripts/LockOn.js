//================================================
//Lock On Script
//================================================
private var current : int = 0;
var locked : boolean = false;
var enemyLocations : GameObject[];
var closest : GameObject;
var activeIcon : GameObject; //Current targeted enemy indicator


function Update()
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
        GetComponent.<Animation>().CrossFade("Armature|Idle", 0.2);

    }
    /* Fixattu versio tästä myöhemmin
    if (playerController.isAttacking)

    */
        transform.LookAt(Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z));
        GetComponent.<Animation>().CrossFade("Armature|Combat_Idle", 0.2);
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
