using UnityEngine;

public class sTypingIndicator : MonoBehaviour
{
    public sNumericalInput parent;
    public float charWidth = 0.2f;

    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(parent.isInputKeyboard)
        {
            rend.enabled = true;
            transform.position = new Vector3(parent.typedText.Length * charWidth, 0, 0) + parent.transform.position;
        }
        else
        {
            rend.enabled = false;
        }
    }
}
