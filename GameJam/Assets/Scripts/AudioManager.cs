using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Range(1, 16)]
	public int extNotesModulo = 16;

	[Range(-12, 12)]
	public int extOctave = 0;

	[Range(2, 5)]
	public int extBassDecayMultiplier = 3;

	private float timeSinceLastShift = 0.0f;
	private float timeOfNextShift = 0.0f;

	private float timeSinceLastBassShift = 0.0f;
	private float timeOfNextBassShift = 0.0f;

	void Start()
	{
		melodyLenShift();
	}

	void Update()
	{
		if(Time.time > timeOfNextShift)
		{
			melodyLenShift();
		}

		if(Time.time > timeOfNextBassShift)
		{
			bassLenShift();
		}
	}

	void melodyLenShift()
	{
		timeSinceLastShift = 0;
		timeOfNextShift = Time.time + Random.Range(4.0f, 14.0f);
		extNotesModulo = Random.Range(2, 16);
	}

	void bassLenShift()
	{
		timeSinceLastBassShift = 0;
		timeOfNextBassShift = Time.time + Random.Range(8.0f, 14.0f);
		extBassDecayMultiplier = Random.Range(2, 5);
	}
}
