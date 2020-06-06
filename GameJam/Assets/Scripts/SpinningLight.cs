using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningLight : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime / 4.0f);
    }
}
