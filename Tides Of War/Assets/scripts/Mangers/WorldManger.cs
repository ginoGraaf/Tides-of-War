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
    BuildingView buildingView;

    public BuildingController buildingController;
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
        TileModel model = World.getTile(10, 10);
        Debug.Log("X: " + model.X + " Y: " + model.Y);
        tileview.TileSetup();
        buildingView.SetUpBuildings();
    }
}
