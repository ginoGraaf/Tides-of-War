using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListButton : MonoBehaviour
{
    [SerializeField]
    GameObject button;
    [SerializeField]
    Transform tileTransformMenu;
    [SerializeField]
    GameObject menuBar;
    [SerializeField]

    BuildMouseMapController buildMouseMapController;
    // Start is called before the first frame update
    public void CreateButton(UnitLoadSprite tileSpriteInfo)
    {
        GameObject tileButton = (GameObject)Instantiate(button);
        tileButton.transform.SetParent(tileTransformMenu);
        Image image = tileButton.GetComponent<Image>();
        image.sprite = tileSpriteInfo.UnitSprite;

        tileButton.name = tileSpriteInfo.unitName;
        Button tileButtonClick = tileButton.GetComponent<Button>();
        tileButtonClick.onClick.AddListener(delegate { buildMouseMapController.CreateUnit(tileSpriteInfo.unitName); });
    }
    public void OpenCloseMenu()
    {
        if (menuBar.activeInHierarchy)
        {
            menuBar.SetActive(false);
        }
        else
        {
            menuBar.SetActive(true);
        }
    }
}
