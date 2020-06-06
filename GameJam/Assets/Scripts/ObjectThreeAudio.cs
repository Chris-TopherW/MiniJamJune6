﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThreeAudio : MonoBehaviour
{
	public AudioManager audioManager;

    void Start()
    {

		GetComponent<ChuckSubInstance>().RunCode( @"
			
			16 => global int sNotesModulo;

			class Crusher extends Chugen
			{
				2 => int bitDepth;
				Math.pow(2, bitDepth) => float bitMultiplier;

				fun void SetBitDepth(int value)
				{
					Math.pow(2, value) => bitMultiplier;
				}

				fun float tick(float input)
				{
					input * bitMultiplier => input;
					input $ int => input;
					input $ float => input;
					input / bitMultiplier => input;
					return input;
				}
			}

			fun void playMelody()
			{
				[0, 80, 77, 80, 0, 80, 77, 80, 0, 77, 80, 77, 0, 77, 80, 77] @=> int p1Ch0Pr0[];

				TriOsc s1 => ADSR env0 => Crusher crusher => LPF filter => dac;

				50::ms => dur decay;
				90::ms => dur NoteLength;
				env0.set(10::ms, decay, 0.0, 1::ms);
				crusher.SetBitDepth(4);
				4000 => filter.freq;

				0.4 => s1.gain;

			 	while(true)
			 	{
			 		if(sNotesModulo > 16) 16 => sNotesModulo;
			 		
			 		for(0 => int i; i < sNotesModulo; i++)
				    {
						if(p1Ch0Pr0[i] == 0.0) 0.0 => s1.gain;
						else 0.4 => s1.gain;

						Math.mtof(p1Ch0Pr0[i]) => s1.freq;

						env0.keyOn();
						10::ms + decay => now;
						env0.keyOff();
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

		GetComponent<ChuckSubInstance>().SetInt( "sNotesModulo", audioManager.extNotesModulo );
    }

    void Update()
    {
    	GetComponent<ChuckSubInstance>().SetInt( "sNotesModulo", audioManager.extNotesModulo );
    }
}
