using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectListScriptableObject", order = 1)]
public class ObjectListScriptableObject : ScriptableObject
{
    public GameObject[] prefabsToCompare;
}
