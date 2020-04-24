using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class TurnController : MonoBehaviour
{
    [SerializeField]
    WinScreen winScreen;
    [SerializeField]
    bool[] activeTeam;
    [SerializeField]
    int waterRiseCounter = 2;

    int turn = 0;
    int teamIndex=0;
    TeamModel[] teams = new TeamModel[4];
    bool waterRise = false;  
    int waterCounter = 0;
    
    public void Init()
    {
        teams[0] = new TeamModel { teamColor = TeamModel.TeamColor.RED, active = activeTeam[0] };
        teams[1] = new TeamModel { teamColor = TeamModel.TeamColor.BLUE, active = activeTeam[1] };
        teams[2] = new TeamModel { teamColor = TeamModel.TeamColor.GREEN, active = activeTeam[2] };
        teams[3] = new TeamModel { teamColor = TeamModel.TeamColor.YELLOW, active = activeTeam[3] };
        
        NextTeam();
    }


    public void NextTeam()
    {
        switch(teams[teamIndex].teamColor)
        {
            case TeamModel.TeamColor.RED:
                TurnUnitsWalkableAgain(UnitModel.UnitColor.RED);
                break;
            case TeamModel.TeamColor.BLUE:
                TurnUnitsWalkableAgain(UnitModel.UnitColor.BLUE);
                break;
            case TeamModel.TeamColor.GREEN:
                TurnUnitsWalkableAgain(UnitModel.UnitColor.GREEN);
                break;
            case TeamModel.TeamColor.YELLOW:
                TurnUnitsWalkableAgain(UnitModel.UnitColor.YELLOW);
                break;
        }
    }
    public void TurnUnitsWalkableAgain(UnitModel.UnitColor initcolor)
    {
        List<UnitModel> units = WorldManger.Instance.unitController.GetAllUnits();
        for (int i = 0; i < units.Count; i++)
        {
            if(units[i].unitColorType==initcolor)
            {
                units[i].UnitHasMoved = false;
                units[i].CbUnitUpdateGraphics(units[i]);
            }
            else
            {
                units[i].UnitHasMoved = true;
                units[i].CbUnitUpdateGraphics(units[i]);
            }
        }
    }

    public UnitModel.UnitColor GetTeam()
    {
        switch(teams[teamIndex].teamColor)
        {
            case TeamModel.TeamColor.RED:
                return UnitModel.UnitColor.RED;
         
            case TeamModel.TeamColor.BLUE:
                return UnitModel.UnitColor.BLUE;
             
            case TeamModel.TeamColor.GREEN:
                return UnitModel.UnitColor.GREEN;

            case TeamModel.TeamColor.YELLOW:
                return UnitModel.UnitColor.YELLOW;
         
            default:
                return UnitModel.UnitColor.RED;
      
        }

    }

    public void TeamUpdate(UnitModel.UnitColor initcolor)
    {
        List<UnitModel> units = WorldManger.Instance.unitController.GetAllUnits();
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].unitColorType != initcolor)
            {
                units[i].UnitHasMoved = true;
                units[i].CbUnitUpdateGraphics(units[i]);
            }
        }
    }

    void UpdateWaterTiles()
    {
        waterRise = !waterRise;
        if(waterRise)
        {
            for (int x = 0; x < WorldManger.Instance.MapSizeX; x++)
            {
                for (int y = 0; y < WorldManger.Instance.MapSizeY; y++)
                {
                    TileModel tile = WorldManger.Instance.World.getTile(x, y);
                    if (tile.CbTileWaterRise == null )
                        continue;
                    tile.CbTileWaterRise(tile);
                }
            }
        }
        else
        {
            waterCounter = 0;
            for (int x = 0; x < WorldManger.Instance.MapSizeX; x++)
            {
                for (int y = 0; y < WorldManger.Instance.MapSizeY; y++)
                {
                    TileModel tile = WorldManger.Instance.World.getTile(x, y);
                    if (tile.CbTileWaterLower == null )
                        continue;
                    tile.CbTileWaterLower(tile);
                }
            }
        }
    }

    public void EndTurn()
    {
        teamIndex++;
        TeamActive();
      
        NextTeam();
    }

    public void TeamActive()
    {
        if (teamIndex >= teams.Length)
        {
            teamIndex = 0;
            turn++;
            waterCounter++;
            if (waterCounter >= waterRiseCounter)
            {
                UpdateWaterTiles();
            }
            WorldManger.Instance.unitController.CheckUnitsOnWater();
        }
        if (!teams[teamIndex].active)
        {
            teamIndex++;
            TeamActive();
        }
    
    }

   public void GameWon(string teamWon,Color teamColor)
    {
        winScreen.WinGameScreenOpen(teamWon, teamColor);
    }
}
