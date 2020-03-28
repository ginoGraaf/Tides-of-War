using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TileSprite")]
[System.Serializable]
public class TilesGraphic : ScriptableObject
{
    public List<TileLoadSprite> TileSprite;
}

[System.Serializable]
public class TileLoadSprite
{
    [SerializeField]
    public string nameOfTile;
    [SerializeField]
    Sprite tileImage;

    public Sprite TileImage { get { return tileImage; } set { tileImage = value; } }
    public string NameOfTile { get { return nameOfTile; } set { nameOfTile = value; } }
}
