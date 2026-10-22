using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string nodeName;

    private bool hasPlayed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasPlayed)
        {
            return;
        }
        
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        dialogueRunner.StartDialogue(nodeName);
        hasPlayed = true;
    }



}
