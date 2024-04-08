using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct People
{
    public string name;
    public int year;
    public string color;

    public People(string name, int year, string color)
    {
        this.name = name;
        this.year = year;
        this.color = color;
    }

    [Serializable]
    public class MemberList
    { public List<People> list; }
}

