using RPG.Dialogue;
using UnityEngine;

public class DialogueQuestion : MonoBehaviour {

    [SerializeField]
    Dialogue[] questions;

    static Dialogue[] questionDialogues;

    private void Start() {

        questionDialogues = new Dialogue[questions.Length];

        for (int i = 0; i < questionDialogues.Length; i++) {

            questionDialogues[i] = questions[i];
        }
    }

    public static void GetQuestion(PlantStatus plantStatus) {

        int i = Random.Range(0, questionDialogues.Length);

        PlayerConversant.StartDialogue(questionDialogues[i]);
    }
}

