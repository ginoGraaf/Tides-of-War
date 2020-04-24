using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarView : MonoBehaviour
{
    [SerializeField]
    GameObject healthprefab;
    
    public Dictionary<UnitModel, GameObject> HealthBarInWorld = new Dictionary<UnitModel, GameObject>();


    public void AddHealthBarToUnit(UnitModel unit)
    {
        GameObject HealthBar = Instantiate(healthprefab) as GameObject;
        GameObject UnitObj = WorldManger.Instance.unitView.UnitOnMap[unit];
        HealthBar.transform.position=new Vector2(unit.PosX+0.1f, unit.PosY+0.6f);
        HealthBar.transform.SetParent(UnitObj.transform);
        HealthBarInWorld.Add(unit, HealthBar);
        unit.CbHealthBar += UpdateHealtBar;
        unit.CbHealthBar(unit);
    }

    public void UpdateHealtBar(UnitModel unit)
    {
        if(!HealthBarInWorld.ContainsKey(unit))
        {
            return;
        }
        GameObject barobj = HealthBarInWorld[unit];
        Image image = barobj.GetComponentInChildren<Image>();
        image.fillAmount = (float)unit.UnitHitPoints / unit.maxHealth * 1;
       if(unit.UnitHitPoints<=0)
        {
            HealthBarInWorld.Remove(unit);
            Destroy(barobj);
        }
        barobj.transform.position = new Vector2(unit.PosX+0.1f, unit.PosY+0.6f);
    }
}
