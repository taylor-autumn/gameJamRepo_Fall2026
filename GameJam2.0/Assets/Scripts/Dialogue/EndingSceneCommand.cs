using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class EndingSceneCommand : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string endingSceneName;

    private bool isLoading;

    private void Awake()
    {
        if (dialogueRunner == null)
        {
            Debug.LogError("Need bond Dialogue Runner。", this);
            return;
        }

        dialogueRunner.AddCommandHandler("teleportToGE1", LoadEndingScene);
    }

    private void LoadEndingScene()
    {
        if (isLoading)
            return;

        if (string.IsNullOrWhiteSpace(endingSceneName) ||
            !Application.CanStreamedLevelBeLoaded(endingSceneName))
        {
            Debug.LogError(
                $"can not load scene：{endingSceneName}. check the connection",
                this);
            return;
        }

        isLoading = true;
        SceneManager.LoadScene(endingSceneName);
    }

    private void OnDestroy()
    {
        if (dialogueRunner != null)
        {
            dialogueRunner.RemoveCommandHandler("teleportToGE1");
        }
    }
}