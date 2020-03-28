using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMouseMapController : MonoBehaviour
{
    public enum typeBuild { SELECTMODE, GROUND, BUILDING, ADDRESOURCE, BULLDOZE, HIRE, DESIGNATEDROOM, UNDESIGNATEDROOM, CREATEITEM, CANBUILD }
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
            case typeBuild.GROUND:
                DoBuild(tile);
                break;
            case typeBuild.BUILDING:
                DoBuildObject(tile);
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
}
