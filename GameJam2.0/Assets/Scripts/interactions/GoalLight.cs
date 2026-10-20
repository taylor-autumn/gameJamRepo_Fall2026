using UnityEngine;

public class GoalLight : MonoBehaviour
{
    public enum RoomType
    {
        CodeRoom,
        VentRoom,
        pipeRoom
    }
    public RoomType theroomtype;
    public bool isRoom = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // switch (theroomtype)
        // {
        //     //case RoomType.CodeRoom:
        
        // }
    }
}
