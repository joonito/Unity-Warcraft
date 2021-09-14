using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 camPos;
    private float camSpeed;
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    void Start()
    {
        camPos.z = -10;
        camSpeed = 0.1f;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) {
            camPos.y += camSpeed;
            gameObject.transform.position = camPos;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            camPos.y -= camSpeed;
            gameObject.transform.position = camPos;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            camPos.x -= camSpeed;
            gameObject.transform.position = camPos;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            camPos.x += camSpeed;
            gameObject.transform.position = camPos;
        }
    }
}
