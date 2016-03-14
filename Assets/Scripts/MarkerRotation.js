#pragma strict

var RotationSpeed: float = 5.0;

function Start () {

}

function Update () {

    transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime), Space.World);

}
