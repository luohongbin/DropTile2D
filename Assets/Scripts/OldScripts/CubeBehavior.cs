﻿using UnityEngine;
using System.Collections;
using SynchronizerData;


public class CubeBehavior : MonoBehaviour {

	//private Animator anim;
	private BeatObserver beatObserver;


	void Start ()
	{
	//	anim = GetComponent<Animator>();
		beatObserver = GetComponent<BeatObserver>();
	}

	void Update ()
	{
		if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat) {
			audio.Play ();
		}
		if ((beatObserver.beatMask & BeatType.UpBeat) == BeatType.UpBeat) {
		//	transform.Rotate(Vector3.forward, 45f);
		}
	}
}
