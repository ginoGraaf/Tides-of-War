﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitModel
{
    public enum UnitColor { RED,BLUE,GREEN,YELLOW};
    public UnitColor unitColorType;
    public string UnitName { get; set; }
    public int UnitMovemtCredits { get; set; }//can copy this in the algorithme.
    public int UnitPower { get; set; }
    public int UnitDefence { get; set; }
    public float UnitHitPoints { get; set; }
    TileModel CurrentTile{ get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public Action<UnitModel> CbUnitUpdateGraphics;
}
