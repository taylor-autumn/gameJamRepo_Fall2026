using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CutscenePictures : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private SpriteRenderer picture;
    [SerializeField] private List<Sprite> pictures = new List<Sprite>();

    [Header("BOMB")]
    [SerializeField] private Sprite soundTriggerPicture;
    [SerializeField] private AudioClip pictureSound;
    [SerializeField] private AudioSource audioSource;

    private int currentIndex = -1;
    private bool commandsRegistered;

    private void Awake()
    {
        if (dialogueRunner == null || picture == null)
        {
            Debug.LogError("请绑定 Dialogue Runner 和 Picture。", this);
            return;
        }

        picture.gameObject.SetActive(false);

        dialogueRunner.AddCommandHandler<int>("showPicture", ShowPicture);
        dialogueRunner.AddCommandHandler("nextOne", NextPicture);
        dialogueRunner.AddCommandHandler("hidePicture", HidePicture);

        dialogueRunner.onDialogueComplete.AddListener(HidePicture);
        commandsRegistered = true;
    }

    private void ShowPicture(int index)
    {
        if (index < 0 || index >= pictures.Count)
        {
            Debug.LogWarning($"图片编号 {index} 超出列表范围。", this);
            return;
        }

        if (pictures[index] == null)
        {
            Debug.LogWarning($"图片列表第 {index} 项没有绑定 Sprite。", this);
            return;
        }

        currentIndex = index;
        picture.sprite = pictures[index];
        picture.gameObject.SetActive(true);
        if (picture.sprite == soundTriggerPicture &&
    pictureSound != null &&
    audioSource != null)
        {
            audioSource.PlayOneShot(pictureSound);
        }
    }

    private void NextPicture()
    {
        ShowPicture(currentIndex + 1);
    }

    private void HidePicture()
    {
        if (picture != null)
        {
            picture.gameObject.SetActive(false);
        }

        currentIndex = -1;
    }

    private void OnDestroy()
    {
        if (!commandsRegistered || dialogueRunner == null)
            return;

        dialogueRunner.RemoveCommandHandler("showPicture");
        dialogueRunner.RemoveCommandHandler("nextOne");
        dialogueRunner.RemoveCommandHandler("hidePicture");

        dialogueRunner.onDialogueComplete.RemoveListener(HidePicture);
    }
}