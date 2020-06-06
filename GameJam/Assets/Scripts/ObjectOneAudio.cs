using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOneAudio : MonoBehaviour
{
    void Start()
    {
    	GetComponent<ChuckSubInstance>().RunCode(@"
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

			SinOsc s => Crusher crusher => dac;
			440 => s.freq;
			crusher.SetBitDepth(4);

			while(true)
			{
				1::second => now;
			}
		");
    }
}
