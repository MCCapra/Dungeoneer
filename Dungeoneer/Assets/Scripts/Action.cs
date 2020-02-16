﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Michael Capra
* Action Class: Represents an action taken in combat
* 2/15/20
*/

[CreateAssetMenu(fileName ="New Action 1", menuName = "New Action")]
public class Action : ScriptableObject
{
    public enum TargetingType { None, Single, All };

    [SerializeField] private Effect effect;
    [SerializeField] private TargetingType targetingType;

    public Effect AppliedEffect { get { return effect; } }
    public TargetingType TargetType { get { return targetingType; } }
}
