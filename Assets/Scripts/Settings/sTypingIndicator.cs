using UnityEngine;

public class sTypingIndicator : MonoBehaviour
{
    public sNumericalInput parent;
    const float charWidth = 0.2f;

    SpriteRenderer rend;

    float transparency;
    public float pingPongDuration = 0.5f;

    bool lastParentIsTyping;
    
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = parent.isTyping;
    }

    void Update()
    {
        if (lastParentIsTyping != parent.isTyping)
        {
            rend.enabled = lastParentIsTyping = parent.isTyping;
        }
        if (!parent.isTyping) return;
        transform.position = new Vector3(parent.typedText.Length * charWidth, 0, 0) + parent.transform.position;

        // Fading animation
        transparency = Mathf.PingPong(Time.time / pingPongDuration, 1f);
        
        var newColor = rend.color;
        newColor.a = transparency;
        rend.color = newColor;
    }
}
