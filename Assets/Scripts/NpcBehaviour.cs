﻿using UnityEngine;
using System.Collections;

public class NpcBehaviour : MonoBehaviour {
public Animation anim;
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animation>();

	}

	// Update is called once per frame
	void Update () {

		anim.CrossFade("Armature|Idle");

	}
}
