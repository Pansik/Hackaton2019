using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    [SerializeField]
    private DialogueController dialogueController;



    public void TriggerDialogue()
    {
        dialogueController.StartDialogue(dialogue);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
