using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DisasterObjectData")]
public class DisasterObjectDataBase : ScriptableObject
{
    public SerializedDictionary<string, GameObject> DisasterObjectsDataBase;
}
