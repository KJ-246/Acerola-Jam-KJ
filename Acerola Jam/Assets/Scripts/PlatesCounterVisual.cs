using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{

    public PlateCounter platesCounter;
    public Transform counterTopPoint;
    public Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameobjectList;

    private void Awake()
    {
        plateVisualGameobjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlatesRemoved;
    }

    private void PlatesCounter_OnPlatesRemoved(object sender, System.EventArgs e) {
        GameObject plateGameObject = plateVisualGameobjectList[plateVisualGameobjectList.Count - 1];
        plateVisualGameobjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .2f;
        plateVisualTransform.localPosition = new Vector2(0, plateOffsetY * plateVisualGameobjectList.Count);

        plateVisualGameobjectList.Add(plateVisualTransform.gameObject);
    }
}
