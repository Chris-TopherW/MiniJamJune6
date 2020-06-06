using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Range(0, 16)]
	public int extNotesModulo = 16;

	[Range(-12, 12)]
	public int extOctave = 0;
}
