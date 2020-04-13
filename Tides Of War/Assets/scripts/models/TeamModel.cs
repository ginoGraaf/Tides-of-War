using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamModel 
{
    public enum TeamColor { RED,BLUE,GREEN,YELLOW};
    public TeamColor teamColor { get; set; }
    public bool active { get; set; }
}
