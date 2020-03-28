using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField]
    TileButton tilebutton;
    [SerializeField]
    List<TilesGraphic> loadTileSprite;
    Dictionary<string, Sprite> SpriteTileList=new Dictionary<string, Sprite>();
    Dictionary<TileModel, GameObject> WorldTile = new Dictionary<TileModel, GameObject>();

    public void TileSetup()
    {
        SetupSpriteDictionary();
        SetupWorld();
        WorldManger.Instance.World.RegisterTileChange(OnTileChange);
        WorldManger.Instance.World.SetTiles();
    }

    void SetupWorld()
    {
        for (int x = 0; x < WorldManger.Instance.MapSizeX; x++)
        {
            for (int y = 0; y < WorldManger.Instance.MapSizeY; y++)
            {
                TileModel tilemodel = WorldManger.Instance.World.getTile(x, y);
                GameObject TileObj = new GameObject(tilemodel.TileType+" x: "+x+" y: "+y);
                TileObj.transform.position = new Vector2(tilemodel.X, tilemodel.Y);
                TileObj.AddComponent<SpriteRenderer>().sprite = SpriteTileList[tilemodel.TileType];
                WorldTile.Add(tilemodel, TileObj);
            }
        }
    }

    void OnTileChange(TileModel tileModel)
    {
        if(!WorldTile.ContainsKey(tileModel))
        {
            Debug.LogError("No Such Tile Model Founded");
            return;
        }
        GameObject tileObj = WorldTile[tileModel];
        tileObj.name = tileModel.TileType+" x: "+tileModel.X+" y: "+tileModel.Y;
        SpriteRenderer sprite = tileObj.GetComponent<SpriteRenderer>();
        sprite.sprite = SpriteTileList[tileModel.TileType];
    }

    void SetupSpriteDictionary()
    {
        foreach (TilesGraphic t in loadTileSprite)
        {
            for (int i = 0; i < t.TileSprite.Count; i++)
            {
                SpriteTileList.Add(t.TileSprite[i].NameOfTile, t.TileSprite[i].TileImage);
                tilebutton.CreateButton(t.TileSprite[i]);
            }
        }
    }

  
}
