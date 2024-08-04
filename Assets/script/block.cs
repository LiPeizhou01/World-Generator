using Map_generator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
enum Surface
{
    grass = 0x0001,
    forest = 0x0002,
    rainforest = 0x0004,
    desert =  0x0008,
    rock = 0x0010,
    snow = 0x0020,
    ice = 0x0040,
    volcano =  0x0080,
}
public class Block : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
