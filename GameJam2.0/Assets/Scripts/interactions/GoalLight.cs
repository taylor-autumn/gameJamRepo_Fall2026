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
    public static bool codeCompleted = false;
    public static bool ventCompleted = false;
    public static bool pipeCompleted = false;
    public Animator LightIndicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (theroomtype)
        {
            case RoomType.CodeRoom:
            codeCompletedCheck();
            codeCompleted = true;
            break;

            case RoomType.VentRoom:
            ventCompletedCheck();
            ventCompleted = false;
            break;

            case RoomType.pipeRoom:
            pipeCompletedCheck();
            pipeCompleted = false;
            break;
        }
    }

    private void codeCompletedCheck()
    {
        
    }
    private void ventCompletedCheck()
    {
        if (ventCompleted == true)
        {
            LightIndicator.SetBool("green", true);
        }
    }
    private void pipeCompletedCheck()
    {
        
    }
}
