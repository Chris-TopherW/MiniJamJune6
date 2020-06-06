using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOneAudio : MonoBehaviour
{
    void Start()
    {
    	GetComponent<ChuckSubInstance>().RunCode(@"
    		SinOsc s => dac;
    		440 => s.freq;

    		while(true)
    		{
    			1::second => now;
    		}

    	");
    }
}
