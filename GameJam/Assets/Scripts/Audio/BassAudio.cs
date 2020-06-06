using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassAudio : MonoBehaviour
{
	public AudioManager audioManager;

    void Start()
    {

		GetComponent<ChuckSubInstance>().RunCode( @"
			
			0 => global int sOctave;
			3 => global int sNoteDecayMultiplier;

			fun void playMelody()
			{
				[41, 0, 41, 41, 0] @=> int p3Ch0Pr0[];

				TriOsc s0 => ADSR env0 => LPF filter => dac;

				(20 * sNoteDecayMultiplier)::ms => dur decay;
				90::ms => dur NoteLength;
				env0.set(10::ms, decay, 0.0, 1::ms);
				4000 => filter.freq;

				0.1 => s0.gain;

			 	while(true)
			 	{
			 		for(0 => int i; i < 5; i++)
				    {
						0.1 => s0.gain;

						Math.mtof(p3Ch0Pr0[i] + sOctave) => s0.freq;

						(20 * sNoteDecayMultiplier)::ms => decay;
						env0.set(10::ms, decay, 0.0, 1::ms);
						env0.keyOn();

						NoteLength => now;
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
		GetComponent<ChuckSubInstance>().SetInt( "sNoteDecayMultiplier", audioManager.extBassDecayMultiplier );
    }

    void Update()
    {
    	GetComponent<ChuckSubInstance>().SetInt( "sOctave", audioManager.extOctave );
    	GetComponent<ChuckSubInstance>().SetInt( "sNoteDecayMultiplier", audioManager.extBassDecayMultiplier );
    }
}
