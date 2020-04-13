using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManger : MonoBehaviour
{
    [SerializeField]
    int mapSizeX = 0, mapSizeY = 0;
    [SerializeField]
    TileView tileview;
    [SerializeField]
    UnitView unitView;
    [SerializeField]
    BuildingView buildingView;

    public BuildingController buildingController;
    public UnitController unitController;
    public TurnController turnController;
    public GameMapController World { get; set; }
   
    public static WorldManger Instance { get; set; }
    public int MapSizeX { get => mapSizeX; set => mapSizeX = value; }
    public int MapSizeY { get => mapSizeY; set => mapSizeY = value; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        World = new GameMapController(mapSizeX, mapSizeY);
        Init();

    }

    void Init()
    {
        tileview.TileSetup();
        unitController.InitUnits();
        unitView.InitSprite();
        buildingView.SetUpBuildings();
        turnController.Init();
    }
}
