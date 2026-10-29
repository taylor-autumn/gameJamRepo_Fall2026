using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string nodeName = "C1S1";

    private PlayerControl player;
    private bool hasPlayed;
    private bool ownsDialogue;
    [SerializeField] private GameObject gameplayUI;
    private bool uiWasActive;



    private void Awake()
    {
        FindGameplayUI();
    }

    private void FindGameplayUI()
    {
        if (gameplayUI == null)
        {
            gameplayUI = GameObject.Find("charUi");
        }

        if (gameplayUI == null)
        {
            Debug.LogWarning("Can not find charUi, the ui will not be hiden during cutscene", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControl detectedPlayer =
            other.GetComponentInParent<PlayerControl>();

        if (detectedPlayer == null)
            return;

        if (hasPlayed || dialogueRunner == null)
            return;

        if (dialogueRunner.IsDialogueRunning)
            return;

        // 先保存玩家引用
        player = detectedPlayer;
        hasPlayed = true;
        ownsDialogue = true;

        // 开始对白前，先绑定结束事件
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);

        if (gameplayUI != null)
        {
            uiWasActive = gameplayUI.activeSelf;
            gameplayUI.SetActive(false);
        }

        player.StopEveryMove();
        dialogueRunner.StartDialogue(nodeName);
    }

    private void OnDialogueComplete()
    {
        if (!ownsDialogue)
            return;

        ownsDialogue = false;

        if (gameplayUI != null)
            gameplayUI.SetActive(uiWasActive);

        if (player != null)
            player.DisableStopEveryMove();

        if (dialogueRunner != null)
            dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueComplete);
    }

    private void OnDestroy()
    {
        if (dialogueRunner != null)
            dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueComplete);

        if (ownsDialogue && player != null)
            player.DisableStopEveryMove();
    }
}