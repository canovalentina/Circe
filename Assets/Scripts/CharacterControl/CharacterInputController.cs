﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterInputController : MonoBehaviour {

	public string Name = "WitchCirce";

	private float filteredForwardInput = 0f;
	private float filteredTurnInput = 0f;

	public bool InputMapToCircular = true;

	public float forwardInputFilter = 5f;
	public float turnInputFilter = 5f;

	private float forwardSpeedLimit = 1f;

	public float Forward
	{
		get;
		private set;
	}

	public float Turn
	{
		get;
		private set;
	}

	public bool Action
	{
		get;
		private set;
	}

	public bool Jump
	{
		get;
		private set;
	}

		

	void Update () {
		
		//GetAxisRaw() so we can do filtering here instead of the InputManager
		float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
		float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis

   // Debug.Log($"H: {h}, V: {v}");

		if (InputMapToCircular)
		{
			// make coordinates circular
			//based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
			h = h * Mathf.Sqrt(1f - 0.5f * v * v);
			v = v * Mathf.Sqrt(1f - 0.5f * h * h);

		}


		//BEGIN ANALOG ON KEYBOARD DEMO CODE
		if (Input.GetKey(KeyCode.Q))
			h = -0.5f;
		else if (Input.GetKey(KeyCode.E))
			h = 0.5f;

		//END ANALOG ON KEYBOARD DEMO CODE  


		//do some filtering of our input as well as clamp to a speed limit
		filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v, 
			Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

		filteredTurnInput = Mathf.Lerp(filteredTurnInput, h, 
			Time.deltaTime * turnInputFilter);

		Forward = filteredForwardInput;
		Turn = filteredTurnInput;


		//Capture "fire" button for action event
		Action = Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject();

		Jump = Input.GetButtonDown("Jump");

	}
}
