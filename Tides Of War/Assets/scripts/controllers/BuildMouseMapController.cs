using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMouseMapController : MonoBehaviour
{
    public enum typeBuild { SELECTMODE, GROUND, BUILDING, ADDRESOURCE, BULLDOZE, MAKEUNIT, DESIGNATEDROOM, UNDESIGNATEDROOM, CREATEITEM, CANBUILD }
    typeBuild buildModeIsObjects = typeBuild.SELECTMODE;
    string buildTypeName = "";
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            ChangeBuild();
        }
        if(Input.GetMouseButton(1))
        {
            buildModeIsObjects = typeBuild.SELECTMODE;
            WorldManger.Instance.unitController.UnitDeselect();
            buildTypeName = "";
        }

    }

    void ChangeBuild()
    {
        int x = Mathf.FloorToInt(mousePos.x+0.5f);
        int y = Mathf.FloorToInt(mousePos.y+0.5f);
        TileModel tile = WorldManger.Instance.World.getTile(x,y);
        switch(buildModeIsObjects)
        {
            case typeBuild.SELECTMODE:
                if (tile != null)
                {
                    WorldManger.Instance.unitController.UnitSlected(tile.UnitOnTile);
                }
                break;
            case typeBuild.GROUND:
                DoBuild(tile);
                break;
            case typeBuild.BUILDING:
                DoBuildObject(tile);
                break;
            case typeBuild.MAKEUNIT:
                DoBuildUnit(tile);
                break;

        }
    }

    void DoBuild(TileModel tile)
    {
        if (tile != null)
        {
            tile.TileType = buildTypeName;
        }
    }
    public void DoBuildObject(TileModel tile)
    {
        WorldManger.Instance.World.PlaceBuildingObj(tile, buildTypeName);
    }

    public void DoBuildUnit(TileModel tile)
    {
        WorldManger.Instance.World.CreateCharacter(tile, WorldManger.Instance.unitController.GetUnitFromCreateList(buildTypeName));
    }

    public void ChangeTile(string name)
    {
        buildModeIsObjects = typeBuild.GROUND;
        buildTypeName = name;
    }

    public void SetBuilding(string name)
    {
        buildModeIsObjects = typeBuild.BUILDING;
        buildTypeName = name;
    }

    public void CreateUnit(string name)
    {
        buildModeIsObjects = typeBuild.MAKEUNIT;
        buildTypeName = name;
    }
}
