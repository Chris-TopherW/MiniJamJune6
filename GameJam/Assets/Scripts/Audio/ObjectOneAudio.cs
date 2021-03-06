﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOneAudio : MonoBehaviour
{
	public AudioManager audioManager;

    void Start()
    {

		GetComponent<ChuckSubInstance>().RunCode( @"
			
			16 => global int sNotesModulo;
			2 => global int sBitcrush;

			class Crusher extends Chugen
			{
				0 => float bitMultiplier;

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
				[72, 75, 0, 72, 75, 0, 72, 70, 68, 0, 70, 72, 0, 77, 0, 70] @=> int p0Ch0Pr0[];

				TriOsc s0 => ADSR env0 => Crusher crusher => LPF filter => Gain g => dac;

				50::ms => dur decay;
				90::ms => dur NoteLength;
				env0.set(10::ms, decay, 0.0, 1::ms);
				4000 => filter.freq;

				0.4 => g.gain;

			 	while(true)
			 	{
			 		if(sNotesModulo > 16) 16 => sNotesModulo;

			 		
			 		for(0 => int i; i < sNotesModulo; i++)
				    {
				    	if(p0Ch0Pr0[i] == 0.0) 0.0 => g.gain;
						else 0.4 => g.gain;

						Math.mtof(p0Ch0Pr0[i]) => s0.freq;

			 			crusher.SetBitDepth(sBitcrush);
			 			
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
		GetComponent<ChuckSubInstance>().SetInt( "sBitcrush", audioManager.extBitCrush );
    }

    void Update()
    {
    	GetComponent<ChuckSubInstance>().SetInt( "sNotesModulo", audioManager.extNotesModulo );
		GetComponent<ChuckSubInstance>().SetInt( "sBitcrush", audioManager.extBitCrush );
    }
}
