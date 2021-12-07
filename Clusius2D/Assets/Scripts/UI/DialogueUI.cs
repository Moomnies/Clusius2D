using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using UnityEngine.UI;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] PlayerConversant _PlayerConversant;
        [SerializeField] TextMeshProUGUI _AIText;
        [SerializeField] Button _NextButton;
        [SerializeField] TextMeshProUGUI _NextButtonText;
        [SerializeField] Transform _ChoiceRoot;
        [SerializeField] GameObject _ChoicePrefab;
        [SerializeField] GameObject _DialogueUI;
        [SerializeField] Button _QuitButton;
        [SerializeField] GameObject _Choices;
        [SerializeField] TextMeshProUGUI _SpeakerText;
             
        void Start()
        {            
            _NextButton.onClick.AddListener(() => Next());
            _QuitButton.onClick.AddListener(() => _PlayerConversant.Quit());
            _PlayerConversant.onConversantUpdated += UpdateUI;

            UpdateUI();
        }

        void Next()
        {
            if (_PlayerConversant.HasNext())
            {
                _PlayerConversant.Quit();
                return;
            }           

            _PlayerConversant.Next();
            
            
            if (!_PlayerConversant.IsChoosing())
            {
                _NextButton.gameObject.SetActive(true);               
            }           
                     
        }

        void UpdateUI()
        {
            _DialogueUI.SetActive(_PlayerConversant.IsActive());

            if (!_PlayerConversant.IsActive())
            {
                return;
            }
            
            _AIText.text = _PlayerConversant.GetText();
            _SpeakerText.text = _PlayerConversant.GetSpeaker();
            

            if (_PlayerConversant.HasNext())
            {
                _NextButtonText.SetText("End>>");                    
            }          

            foreach (Transform item in _ChoiceRoot)
            {
                Destroy(item.gameObject);
            }

            BuildChoiceList();

        }

        private void BuildChoiceList()
        {
            if (_PlayerConversant.IsChoosing())
            {             
                if (_NextButton.gameObject.activeSelf)
                {
                    _NextButton.gameObject.SetActive(false);
                    
                }
                
                if (!_Choices.gameObject.activeSelf)
                {
                    _Choices.SetActive(true);
                }

                foreach (DialogueNode choice in _PlayerConversant.GetChoices())
                {
                    GameObject choiceInstance = Instantiate(_ChoicePrefab, _ChoiceRoot);
                    var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                    textComp.text = choice.Text;

                    Button button = choiceInstance.GetComponentInChildren<Button>();
                    button.onClick.AddListener(() =>
                    {
                        _PlayerConversant.SelectChoice(choice);
                        _Choices.SetActive(false);
                        Next();                        
                    });
                }
            }            
        }
    }
}
