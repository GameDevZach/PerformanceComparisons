using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComparisonController : MonoBehaviour
{
    public int instanceQuantity;
    private int currentQuantity = 0;
    private GameObject[] activeInstances;
    public float zOffset;
    public int rows = 1;
    public float rowOffset;
    [SerializeField]
    private ObjectListScriptableObject objectListHolder;
    private GameObject[] prefabList;
    private bool leftActive = false;
    private bool rightActive = false;

    [SerializeField]
    private TMP_Dropdown SelectADropdown;
    [SerializeField]
    private TMP_Dropdown SelectBDropdown;
    [SerializeField]
    private GameObject EditPanel;

    void Start()
    {
        currentQuantity = 0;

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
    }

    void Update()
    {
        
    }


    public void PressASide()
    {

    }

    public void PressBSide()
    {

    }

    public void SelectObjectA(int val)
    {
        Debug.Log(val);
    }

    public void SelectObjectB(int val)
    {
        Debug.Log(val);
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
}
