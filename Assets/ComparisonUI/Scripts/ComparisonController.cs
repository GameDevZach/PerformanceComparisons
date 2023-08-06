using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComparisonController : MonoBehaviour
{
    public int instanceQuantity;
    private int currentQuantity = 0;
    private GameObject[] activeInstances;
    public float zOffset;
    private float currentZ;
    public int rows = 5;
    public float rowOffset = 1;
    [SerializeField]
    private ObjectListScriptableObject objectListHolder;
    private GameObject[] prefabList;
    private bool leftActive = false;
    private bool rightActive = false;
    private GameObject prefabA;
    private GameObject prefabB;
    private GameObject placingObject;

    [SerializeField]
    private TMP_Dropdown SelectADropdown;
    [SerializeField]
    private TMP_Dropdown SelectBDropdown;
    [SerializeField]
    private GameObject EditPanel;
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private TMP_Text unityVersionText;
    [SerializeField]
    private TMP_Text fpsTextA;
    [SerializeField]
    private TMP_Text fpsTextB;

    // FPS tracking
    private int frameCount = 0;
    private float deltaTime = 0.0f;
    private float fps = 0.0f;
    public float updateFPSRate = 4.0f; // Updates per sec.


    void Start()
    {
        currentQuantity = 0;
        currentZ = 0;

        unityVersionText.text = "Unity " + Application.unityVersion;

        prefabList = objectListHolder.prefabsToCompare;
        List<string> optionsList = new List<string>();
        for( int i=0; i<prefabList.Length; i++)
        {
            optionsList.Add(prefabList[i].name);
        }
        SelectADropdown.ClearOptions();
        SelectADropdown.AddOptions(optionsList);
        SelectBDropdown.ClearOptions();
        SelectBDropdown.AddOptions(optionsList);
        if (prefabList.Length > 0)
        {
            prefabA = prefabList[0];
            prefabB = prefabList[0];
        }
        else
        {
            Debug.LogError("No prefabs in list found on scriptable object. Please create and attach an ObjectListScriptableObject and list prefabs for comparison in it.");
        }

    }

    void Update()
    {
        UpdateFPS();
        // Continue adding rows if below target quantity 
        if(currentQuantity < activeInstances.Length)
        {
            PlaceRow();
        }
    }

    private void UpdateFPS()
    {
        frameCount++;
        deltaTime += Time.deltaTime;
        if(deltaTime > 1.0f / updateFPSRate)
        {
            fps = frameCount / deltaTime;
            frameCount = 0;
            deltaTime -= 1.0f / updateFPSRate;
            // update text
            fpsTextA.text = prefabA.name + "<br>FPS: " + Mathf.RoundToInt(fps);
            fpsTextB.text = prefabB.name + "<br>FPS: " + Mathf.RoundToInt(fps);
        }
    }


    public void PressASide()
    {
        StartPlacing(prefabA);
        leftActive = true;
        rightActive = false;
        fpsTextA.gameObject.SetActive(true);
        fpsTextB.gameObject.SetActive(false);
    }

    public void PressBSide()
    {
        StartPlacing(prefabB);
        leftActive = false;
        rightActive = true;
        fpsTextA.gameObject.SetActive(false);
        fpsTextB.gameObject.SetActive(true);
    }

    public void SelectObjectA(int val)
    {
        Debug.Log(val);
        prefabA = prefabList[val];
    }

    public void SelectObjectB(int val)
    {
        Debug.Log(val);
        prefabB = prefabList[val];
    }

    public void DoneEditing()
    {
        EditPanel.SetActive(false);
    }

    public void StartEditing()
    {
        EditPanel.SetActive(true);
    }

    public void UpdateQuantity(string quantStr)
    {
        instanceQuantity = Mathf.RoundToInt(Mathf.Max(1, int.Parse(quantStr)));
    }

    public void UpdateRows(string rowStr)
    {
        rows = Mathf.RoundToInt(Mathf.Max(1, int.Parse(rowStr)));
    }

    public void UpdateZOffset(string zStr)
    {
        zOffset = float.Parse(zStr);
    }

    private void StartPlacing(GameObject placeMe)
    {
        ClearObjects();
        placingObject = placeMe;
        activeInstances = new GameObject[instanceQuantity];
    }

    private void ClearObjects()
    {
        for(int i=0; i<currentQuantity; i++)
        {
            Destroy(activeInstances[i]);
        }

        currentQuantity = 0;
        currentZ = 0;
    }

    private void PlaceRow()
    {
        float placementWidth = rowOffset * (rows - 1);
        float xOffset = -placementWidth / 2;

        int curRowCount = Mathf.RoundToInt(Mathf.Min(activeInstances.Length - currentQuantity, rows));

        for (int i = 0; i < curRowCount; i++)
        {
            activeInstances[currentQuantity] = Instantiate(placingObject);
            activeInstances[currentQuantity].transform.Translate(new Vector3(xOffset, 0, currentZ));
            currentQuantity += 1;
            xOffset += rowOffset;
        }

        quantityText.text = currentQuantity + "/" + activeInstances.Length;
        currentZ += zOffset;
    }
}
