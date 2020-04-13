using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitOrderMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    BuildMouseMapController buildmouseController;
    [SerializeField]
    Button AttackBtn;
    public void ChooseAttack()
    {
        AttackBtn.interactable = false;
        WorldManger.Instance.unitController.GetAttackPoints(buildmouseController.SelectedUnit);
    }
    public void ChooseWait()
    {
        WorldManger.Instance.unitController.DeslectTargets();
        AttackBtn.interactable = true;
        WorldManger.Instance.unitController.openUnitOrderMenu.SetActive(false);
        buildmouseController.SelectedUnit = null;
    }
}
