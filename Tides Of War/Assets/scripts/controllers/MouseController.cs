using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Vector3 LastFramePos;
    Vector3 currFramePos;
    float speed = 5;
    // Use this for initialization
    public GameObject cam;
    // Update is called once per frame
    private void Start()
    {
        cam.transform.position = new Vector3(WorldManger.Instance.MapSizeX / 2, WorldManger.Instance.MapSizeY / 2,-10);
    }
    void Update()
    {
        currFramePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(currFramePos);
        DragCamera();
        LastFramePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void DragCamera()
    {
        if (Input.GetMouseButton(1))
        {
            Vector2 diff = LastFramePos - currFramePos;
            cam.transform.Translate(diff);
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel") * 1.5f;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 30f);

    }
}

