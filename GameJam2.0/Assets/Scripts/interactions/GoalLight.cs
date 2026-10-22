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
    public static bool codeCompleted;
    public static bool ventCompleted;
    public static bool pipeCompleted;
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
            break;

            case RoomType.VentRoom:
            ventCompletedCheck();
            break;

            case RoomType.pipeRoom:
            pipeCompletedCheck();
            break;
            
            default:
            justbegreen();
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
    private void justbegreen()
    {
        LightIndicator.SetBool("green", true);
    }
}
