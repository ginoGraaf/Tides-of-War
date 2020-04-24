using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMouseMapController : MonoBehaviour
{
    public enum typeBuild { SELECTMODE, GROUND, BUILDING, ADDRESOURCE, BULLDOZE, MAKEUNIT, DESIGNATEDROOM, UNDESIGNATEDROOM, CREATEITEM, CANBUILD }
    typeBuild buildModeIsObjects = typeBuild.SELECTMODE;
    string buildTypeName = "";
    Vector2 mousePos;
    UnitModel selectedUnit;
    bool MouseTypeSelectUnit = false;
    public UnitModel SelectedUnit { get => selectedUnit; set => selectedUnit = value; }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {

            ChangeBuild();
        }
        if(Input.GetMouseButton(0) && !MouseTypeSelectUnit)
        {
            ChangeBuild();
        }
        if(Input.GetMouseButton(1))
        {
            buildModeIsObjects = typeBuild.SELECTMODE;
            WorldManger.Instance.unitController.UnitDeselect();
            if (!WorldManger.Instance.unitController.openUnitOrderMenu.activeInHierarchy)
            {
                SelectedUnit = null;
            }
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
                if (SelectedUnit != null)
                {
                    WorldManger.Instance.unitController.MoveUnit(SelectedUnit, tile);
                }
                if (tile != null)
                {
                    MouseTypeSelectUnit = true;
                    if (!WorldManger.Instance.unitController.openUnitOrderMenu.activeInHierarchy)
                    {
                        WorldManger.Instance.unitController.UnitSlected(tile.UnitOnTile);
                        if (tile.UnitOnTile != null)
                        {
                            SelectedUnit = tile.UnitOnTile;
                        }
                    }
                }
                if (WorldManger.Instance.unitController.openUnitOrderMenu.activeInHierarchy)
                {
                    WorldManger.Instance.unitController.OrderStrike(tile,selectedUnit);
                    WorldManger.Instance.unitController.DeslectTargets();
                }

                break;
            case typeBuild.GROUND:
                DoBuild(tile);
                break;
            case typeBuild.BUILDING:
                DoBuildObject(tile);
                break;
            case typeBuild.MAKEUNIT:
                if (tile.UnitOnTile == null)
                {
                    DoBuildUnit(tile);
                }
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
        MouseTypeSelectUnit = false;
        buildTypeName = name;
    }

    public void SetBuilding(string name)
    {
        buildModeIsObjects = typeBuild.BUILDING;
        MouseTypeSelectUnit = false;
        buildTypeName = name;
    }

    public void CreateUnit(string name)
    {
        buildModeIsObjects = typeBuild.MAKEUNIT;
        MouseTypeSelectUnit = false;
        buildTypeName = name;
    }
}
