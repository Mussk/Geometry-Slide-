using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float duration = 0.75f;
    //public float fadeSpeed = 2f;

    private TextMeshProUGUI text;
    //private Canvas canvas;
    private float timer;
    private Vector3 startPosition;
    private Vector3 direction;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        //canvas = GetComponent<Canvas>();
        startPosition = transform.position;
        direction = Vector3.Lerp(Camera.main.transform.forward, Vector3.up, 0.75f).normalized;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Move upward
        transform.position = startPosition + direction * (timer * floatSpeed);

        // Fade out
        if (text != null)
        {
            Color c = text.color;
            c.a = Mathf.Lerp(1f, 0f, timer / duration);
            text.color = c;
        }

        // Disable after duration
        if (timer >= duration)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}