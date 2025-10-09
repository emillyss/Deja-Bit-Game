using UnityEngine;

[System.Serializable]
public struct SavedState
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public int memoryWeight;
    public string objName;

    public SavedState(Vector3 pos, Quaternion rot, Vector3 vel, int weight, string name)
    {
        position = pos;
        rotation = rot;
        velocity = vel;
        memoryWeight = weight;
        objName = name;
    }
}

