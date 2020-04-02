using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    Dictionary<UnitModel, GameObject> UnitOnMap = new Dictionary<UnitModel, GameObject>();
    Dictionary<string, Sprite> UnitSprite = new Dictionary<string, Sprite>();
    [SerializeField]
    UnitListButton unitListButton;
    [SerializeField]
    List<UnitLoadSprite> unitLoadSpritesSheets;

    public void InitSprite()
    {
        foreach(UnitLoadSprite us in unitLoadSpritesSheets)
        {
            UnitSprite.Add(us.unitName, us.UnitSprite);
            unitListButton.CreateButton(us);
        }
        WorldManger.Instance.World.RegisterCharacterToWorld(CreateNewUnit);
    }



    void CreateNewUnit(UnitModel model)
    {
        GameObject Unit = new GameObject(model.UnitName);
        Unit.AddComponent<SpriteRenderer>().sprite = UnitSprite[model.UnitName];
        Unit.transform.position = new Vector3(model.PosX, model.PosY, -2);

        model.CbUnitUpdateGraphics += UpdateUnitSprite;
    }

    void UpdateUnitSprite(UnitModel model)
    {
        if(!UnitOnMap.ContainsKey(model))
        {
            Debug.LogError("Unit doesn't exist in world");
            return;
        }
        GameObject unit = UnitOnMap[model];
        unit.transform.position = new Vector3(model.PosX, model.PosY, -2);
    }
}

[System.Serializable]
public class UnitLoadSprite
{
    public string unitName;
    public Sprite UnitSprite;
}