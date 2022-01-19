using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField]
    float bounceSpeed = 2;
    [SerializeField]
    float height = .5f;
    [SerializeField]
    GameObject parent;

    Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector2(this.transform.position.x, (parent.transform.position.y + 5));
    }

    // Update is called once per frame
    private void Update()
    {
        float newY = Mathf.Sin(Time.time * bounceSpeed);

        this.transform.position = new Vector2(position.x, (newY * height + 1.5f));
    }
}
