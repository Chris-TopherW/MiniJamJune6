using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOneAudio : MonoBehaviour
{
	[Range(0, 16)]
	public int ExtNotesModulo = 16;

	[Range(-12, 12)]
	public int ExtOctave = 0;

    void Start()
    {

		GetComponent<ChuckSubInstance>().RunCode( @"
			
			16 => global int sNotesModulo;
			0 => global int sOctave;

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
				[72, 75, 0, 72, 75, 0, 72, 70, 68, 0, 70, 72, 0, 77, 0, 70] @=> int p0Ch0Pr0[];
				[0, 75, 77, 0, 75, 77, 0, 77, 75, 77, 0, 75, 72, 0, 77, 0] @=> int p1Ch0Pr0[];

				TriOsc s0 => ADSR env0 => Crusher crusher => LPF filter => dac;
				TriOsc s1 => env0;

				50::ms => dur decay;
				90::ms => dur NoteLength;
				env0.set(10::ms, decay, 0.0, 1::ms);
				crusher.SetBitDepth(4);
				4000 => filter.freq;

				0.4 => s0.gain;
				0.4 => s1.gain;

			 	while(true)
			 	{
			 		if(sNotesModulo > 16) 16 => sNotesModulo;
			 		
			 		for(0 => int i; i < sNotesModulo; i++)
				    {
				    	if(p0Ch0Pr0[i] == 0.0) 0.0 => s0.gain;
						else 0.4 => s0.gain;
						if(p1Ch0Pr0[i] == 0.0) 0.0 => s1.gain;
						else 0.4 => s1.gain;

						Math.mtof(p0Ch0Pr0[i] + sOctave) => s0.freq;
						Math.mtof(p1Ch0Pr0[i] + sOctave) => s1.freq;

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

		GetComponent<ChuckSubInstance>().SetInt( "sNotesModulo", ExtNotesModulo );
		GetComponent<ChuckSubInstance>().SetInt( "sOctave", ExtOctave );
    }

    void Update()
    {
    	GetComponent<ChuckSubInstance>().SetInt( "sNotesModulo", ExtNotesModulo );
    	GetComponent<ChuckSubInstance>().SetInt( "sOctave", ExtOctave );
    }
}
