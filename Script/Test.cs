using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Effect;

    // Update is called once per frame
    void Start()
    {
        Instantiate(Effect, transform.position = new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
    }
}
