using UnityEngine;

public class GoalLight : MonoBehaviour
{
    public enum RoomType
    {
        CodeRoom,
        VentRoom,
        pipeRoom,
        usbPlug
    }
    public RoomType theroomtype;
    public static bool codeCompleted;
    public static bool ventCompleted;
    public static bool pipeCompleted;
    public static bool usbCompleted;
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

            case RoomType.usbPlug:
            usbCompletedCheck();
            break;
            
            default:
            justbegreen();
            break;
        }
    }

    private void codeCompletedCheck()
    {
        if (codeCompleted == true)
        {
            playerStatus.instance.completedFlashlightLevel = true;
            LightIndicator.SetBool("green", true);
        }
    }
    private void ventCompletedCheck()
    {
        if (ventCompleted == true)
        {
            playerStatus.instance.completedScrewLevel = true;
            LightIndicator.SetBool("green", true);
        }
    }
    private void usbCompletedCheck()
    {
        if (usbCompleted == true)
        {
            playerStatus.instance.completedUSBLevel = true;
            LightIndicator.SetBool("green", true);
        }
    }
    private void pipeCompletedCheck()
    {
        if (pipeCompleted == true)
        {
            playerStatus.instance.completedWrenchLevel = true;
            LightIndicator.SetBool("green", true);
        }
    }
    private void justbegreen()
    {
        LightIndicator.SetBool("green", true);
    }
}
