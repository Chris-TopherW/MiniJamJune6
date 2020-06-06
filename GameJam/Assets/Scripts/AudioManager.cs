using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Range(1, 16)]
	public int extNotesModulo = 16;

	[Range(-12, 12)]
	public int extOctave = 0;

	private float timeSinceLastShift = 0.0f;
	private float timeOfNextShift = 0.0f;

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
	}

	void melodyLenShift()
	{
		timeSinceLastShift = 0;
		timeOfNextShift = Time.time + Random.Range(4.0f, 14.0f);
		Debug.Log("timeOfNextShift = ");
		Debug.Log(timeOfNextShift);
		extNotesModulo = Random.Range(2, 16);
	}
}
