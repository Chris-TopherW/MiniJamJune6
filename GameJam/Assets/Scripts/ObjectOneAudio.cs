using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOneAudio : MonoBehaviour
{
    void Start()
    {
    	GetComponent<ChuckSubInstance>().RunCode( @"
    		class Crusher extends Chugen
			{
				8 => int bitDepth;
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

			[72, 75, 0, 72, 75, 0, 72, 70, 68, 0, 70, 72, 0, 77, 0, 70] @=> int p0Ch0Pr0[];
			[0, 75, 77, 0, 75, 77, 0, 77, 75, 77, 0, 75, 72, 0, 77, 0] @=> int p1Ch0Pr0[];

			SinOsc s0 => ADSR env0 => Crusher crusher => dac;
			SinOsc s1 => env0 => crusher;

			50::ms => dur decay;
			90::ms => dur NoteLength;
			env0.set(10::ms, decay, 0.0, 1::ms);
			crusher.SetBitDepth(4);

			0.4 => s0.gain;
			0.4 => s1.gain;

			16 => int notesModulo;

			while(true)
			{
				if(notesModulo > 16) 16 => notesModulo;

			    for(0 => int i; i < notesModulo; i++)
			    {
			    	if(p0Ch0Pr0[i] == 0.0) 0.0 => s0.gain;
					else 0.4 => s0.gain;
					if(p1Ch0Pr0[i] == 0.0) 0.0 => s1.gain;
					else 0.4 => s1.gain;

					Math.mtof(p0Ch0Pr0[i]) => s0.freq;
					Math.mtof(p1Ch0Pr0[i]) => s1.freq;

					env0.keyOn();
					10::ms + decay => now;
					env0.keyOff();
					NoteLength - 10::ms - decay => now;
			    }		
				
			}
		" );
    }
}
