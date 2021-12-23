using UnityEngine;

public class scr_tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] Buttons;
    [SerializeField] GameObject[] Indicators;
    [SerializeField] int CurrentButton;

    void Start()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (CurrentButton < i)
            {
                Buttons[i].SetActive(false);
                Indicators[i].SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (CurrentButton < (Buttons.Length - 1))
        {
            if (Input.touchCount > 0)
            {
                Debug.Log("klik");
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("klik start");
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (CurrentButton < i)
                        {
                        }
                        else
                        {
                            Debug.Log("klik " + Buttons[i + 1] + " aan");
                            Buttons[i + 1].SetActive(true);
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
                    CurrentButton++;
                }
            }
        }
    }
}
