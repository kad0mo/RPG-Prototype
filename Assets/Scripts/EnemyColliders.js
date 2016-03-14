#pragma strict

var EnemyBehaviour:EnemyBehaviour;

function Start () {

}

function Update () {

}

function OnTriggerEnter(other:Collider) {

    if(other.gameObject.CompareTag("fist"))
    {
        Debug.Log("Enemy hits the player.");
        EnemyBehaviour.takeDamage(1);


    }
    if(other.gameObject.CompareTag("fireball"))
    {
        Debug.Log("Enemy hits the player.");
        EnemyBehaviour.takeDamage(2);


    }

}
