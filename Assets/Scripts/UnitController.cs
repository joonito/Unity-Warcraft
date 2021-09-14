using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    private List<PlayerControl> selectedUnit;
    public Vector3 startPos;
    public AudioSource audioSource;
    void Awake()
    {
        selectedUnit = new List<PlayerControl>();
    }
    void Update()
    {
        selectUnits();
        moveUnits();
    }
    void selectUnits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] colliders = Physics2D.OverlapAreaAll(startPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            foreach(PlayerControl unit in selectedUnit) {
                unit.SetSelectedVisible(false);
            }
            selectedUnit.Clear();
            foreach (Collider2D collider in colliders)
            {
                PlayerControl unit = collider.GetComponent<PlayerControl>();
                if (unit != null) {
                    unit.SetSelectedVisible(true);
                    selectedUnit.Add(unit);
                }
            }
            Debug.Log(selectedUnit.Count);
        }
    }
    void moveUnits() {
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedUnit.Count != 0)
                audioSource.Play();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            List<Vector3> targetPositionList = GetPositionListAround(mousePosition, new float[] {1f, 2f, 3f}, new int[] {5, 10, 20});
            int targetPositionListIndex = 0;
            foreach (PlayerControl unit in selectedUnit)
            {
                unit.target = targetPositionList[targetPositionListIndex];
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
                unit.target.z = unit.transform.position.z;
                unit.animator.SetFloat("xPos", mousePosition.x - unit.transform.position.x);
                unit.animator.SetFloat("yPos", mousePosition.y - unit.transform.position.y);
            }
        }
    }
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount) {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++) {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray){
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++) {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle) {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}
