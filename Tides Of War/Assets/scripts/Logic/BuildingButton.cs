using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
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
    public void CreateButton(TileLoadSprite tileSpriteInfo)
    {
        GameObject tileButton = (GameObject)Instantiate(button);
        tileButton.transform.SetParent(tileTransformMenu);
        Image image = tileButton.GetComponent<Image>();
        image.sprite = tileSpriteInfo.TileImage;

        tileButton.name = tileSpriteInfo.nameOfTile;
        Button tileButtonClick = tileButton.GetComponent<Button>();
        tileButtonClick.onClick.AddListener(delegate { buildMouseMapController.SetBuilding(tileSpriteInfo.nameOfTile); });
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
