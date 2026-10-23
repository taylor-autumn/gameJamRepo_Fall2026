using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string nodeName;
    [SerializeField] private GameObject picture;
    private bool hasPlayed = false;
    private bool pictureShowing;
    private PlayerControl playerControl;

    private string showCommandName;
    private string hideCommandName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        picture.SetActive(false);

        showCommandName = nodeName + "_ShowPicture";
        hideCommandName = nodeName + "_HidePicture";

        dialogueRunner.AddCommandHandler(showCommandName, ShowPicture);
        dialogueRunner.AddCommandHandler(hideCommandName, HidePicture);
        hasPlayed = false;
        pictureShowing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("try trigger dialogue");
        if (hasPlayed)
        {
            return;
        }
        
        if (!collision.CompareTag("Player"))
        {
            print("not player");
            return;
        }

        playerControl = collision.GetComponentInParent<PlayerControl>();

        if (playerControl == null)
        {
            return;
        }

        print("trigger dialogue");
        dialogueRunner.StartDialogue(nodeName);
        hasPlayed = true;
    }

    private void ShowPicture()
    {

        picture.SetActive(true);
        pictureShowing = true;
        playerControl.StopEveryMove();

        //if (playerControl != null && !pictureS

    }

    private void HidePicture()
    {


        picture.SetActive(false);
        pictureShowing = false;
        playerControl.DisableStopEveryMove();

    }

    private void OnDestroy()
    {
        HidePicture();

        if (dialogueRunner != null)
        {
            dialogueRunner.RemoveCommandHandler(showCommandName);
            dialogueRunner.RemoveCommandHandler(hideCommandName);
        }
    }


}
