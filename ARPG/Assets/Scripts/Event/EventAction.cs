using UnityEngine;

public partial class EventAction
{
    //이벤트들 정의
    public Define.EventActionType Type { get; set; }
    public float Time { get; set; }
    public BaseObject Owner { get; set; }
    public BaseObject Target { get; set; }

    public int[] ints = null;
    public float[] floats = null;
    public bool[] bools = null;
    public string[] strings = null;
    public GameObject[] gameObjects = null;
    public Vector3[] vector3s = null;

    public EventAction() { }
    public EventAction(EventAction evt)
    {
        ints = evt.ints;
        floats = evt.floats;
        bools = evt.bools;
        strings = evt.strings;
        gameObjects = evt.gameObjects;
        vector3s = evt.vector3s;
    }

    public void Initialized()
    {
        ints = null;
        floats = null;
        bools = null;
        strings = null;
        gameObjects = null;
        vector3s = null;
    }
}