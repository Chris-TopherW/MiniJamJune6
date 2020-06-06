using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThreeAudio : MonoBehaviour
{
	public AudioManager audioManager;

    void Start()
    {

		GetComponent<ChuckSubInstance>().RunCode( @"
			
			0 => global int sOctave;

			fun void playMelody()
			{
				[41, 41, 41, 41, 0] @=> int p3Ch0Pr0[];

				TriOsc s0 => ADSR env0 => LPF filter => dac;

				50::ms => dur decay;
				90::ms => dur NoteLength;
				env0.set(10::ms, decay, 0.0, 1::ms);
				4000 => filter.freq;

				0.4 => s0.gain;

			 	while(true)
			 	{
			 		for(0 => int i; i < 5; i++)
				    {
				    	if(p3Ch0Pr0[i] == 0.0) 0.0 => s0.gain;
						else 0.4 => s0.gain;

						Math.mtof(p3Ch0Pr0[i] + sOctave) => s0.freq;

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

		GetComponent<ChuckSubInstance>().SetInt( "sOctave", audioManager.extOctave );
    }

    void Update()
    {
    	GetComponent<ChuckSubInstance>().SetInt( "sOctave", audioManager.extOctave );
    }
}
