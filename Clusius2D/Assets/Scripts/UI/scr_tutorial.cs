using UnityEngine;

public class Scr_tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] Buttons;
    [SerializeField] GameObject[] Indicators;
    public int CurrentButton;
    [SerializeField] GameObject Pointer;
    [SerializeField] GameObject indi;
    public bool tutorial;


    void Start()
    {
        LoadTutorial();
        if (tutorial == false)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (CurrentButton < i)
                {
                    Buttons[i].SetActive(false);
                    indi.transform.position = Buttons[0].transform.position;
                    //Indicators[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("tutorial klaar");
            indi.SetActive(false);
        }
    }
    public void SaveTutorial()
    {
        SaveSystem.SaveTutorial(this);
    }

    public void LoadTutorial()
    {
        GameDataTutorial data = SaveSystem.LoadTutorial();
        if (data != null)
        {
            CurrentButton = data.CurrentButton;
            tutorial = data.tutorial;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentButton < (Buttons.Length))
        {
            if (Input.touchCount > 0)
            {
                //Debug.Log("klik");
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Pointer.transform.position = touchPosition;
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("klik start " + touchPosition);
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (CurrentButton < i)
                        {

                        }
                        else
                        {
                            RaycastHit2D[] hits = Physics2D.RaycastAll(touchPosition, -Vector3.forward);
                            for (int j = 0; j < hits.Length; j++)
                            {
                                Debug.Log("klik " + hits[j].collider.gameObject.name + " ik ben geklikt");
                                RaycastHit2D hit = hits[j];
                                if (hit.collider.gameObject == Buttons[i])
                                {
                                    Debug.Log("klik " + hit.collider.gameObject.name + " ik ben de geklikte");
                                    CurrentButton++;
                                    Debug.Log("klik " + Buttons[i + 1] + " aan");
                                    if (i == Buttons.Length)
                                    {
                                        indi.SetActive(false);
                                    }
                                    indi.transform.position = Buttons[i + 1].transform.position;
                                    Buttons[i + 1].SetActive(true);
                                }
                            }
                        }
                    }
                    for (int i = 0; i < Indicators.Length; i++)
                    {
                        if (CurrentButton == i - 1)
                        {
                            Indicators[i].SetActive(true);
                        }
                        else
                        {
                            Indicators[i].SetActive(false);
                        }
                    }
                }
            }
        }
        else
        {
            if (tutorial == false)
            {
                Debug.Log("tutorial klaar");
                indi.SetActive(false);
                tutorial = true;
                SaveTutorial();
            }
        }
    }
}
