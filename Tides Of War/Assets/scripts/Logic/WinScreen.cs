using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    GameObject BannerA, BannerB, BannerC, WinScreenObj;
    [SerializeField]
    TextMeshProUGUI WinTextMesh;
    // Start is called before the first frame update
    public void WinGameScreenOpen(string teamWon,Color teamColor)
    {
        Color darker = new Color(teamColor.r + 0.2f, teamColor.g + 0.2f, teamColor.b + 0.2f);
        WinScreenObj.SetActive(true);
        WinScreenObj.GetComponent<Image>().color = teamColor;
        BannerA.GetComponent<Image>().color = darker;
        BannerB.GetComponent<Image>().color = darker;
        BannerC.GetComponent<Image>().color = darker;
        WinTextMesh.text = teamWon;
    }
}
