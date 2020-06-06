﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFourAudio : MonoBehaviour
{
	public AudioManager audioManager;

    void Start()
    {

		GetComponent<ChuckSubInstance>().RunCode( @"
			
			0 => global int sOctave;

			fun void playMelody()
			{

				50::ms => dur decay;
				90::ms => dur NoteLength;

				[77, 77, 77, 77, 72, 72, 72, 72, 70, 70, 67, 70] @=> int p4Ch0Pr0[];

				SawOsc s0 => LPF filter => dac;

				4000 => filter.freq;

				0.1 => s0.gain;

			 	while(true)
			 	{
			 		for(0 => int i; i < 12; i++)
				    {
						Math.mtof(p4Ch0Pr0[i] + sOctave) => s0.freq;

						10::ms + decay => now;
						NoteLength - 10::ms - decay => now;
					}
				}
			}

			spork ~ playMelody();
			while(true)
			{
				1::second => now;
			}

		");

		GetComponent<ChuckSubInstance>().SetInt( "sOctave", audioManager.extOctave );
    }

    void Update()
    {
    	GetComponent<ChuckSubInstance>().SetInt( "sOctave", audioManager.extOctave );
    }
}
