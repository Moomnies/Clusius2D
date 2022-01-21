using RPG.Dialogue;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DialogueQuestion : MonoBehaviour {

    [SerializeField]
    Dialogue[] questions;

    static Dialogue[] questionDialogues;

    public static UnityAction<PlantStatus> startQuestion;

    [SerializeField]
    PlantStatus status;

    public static bool awnswerIsCorrect = false;

    private void Start() {

        questionDialogues = new Dialogue[questions.Length];

        for (int i = 0; i < questionDialogues.Length; i++) {

            questionDialogues[i] = questions[i];
        }

        startQuestion += GetQuestion;
    }

    public void GetQuestion(PlantStatus plantStatus) {

        status = plantStatus;
        awnswerIsCorrect = false;
        StartCoroutine(WaitForAnswer());
    }  

    IEnumerator WaitForAnswer() {

        int i = Random.Range(0, questionDialogues.Length);

        PlayerConversant.StartDialogue(questionDialogues[i]);        

        while ((PlayerConversant._CurrentDialogue != null || awnswerIsCorrect == true) && status != null) {

            if (awnswerIsCorrect) {
          
                status.needFixed();
                status = null;
            }

            yield return false;
        }          
    }
}

