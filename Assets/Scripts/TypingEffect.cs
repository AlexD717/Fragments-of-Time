using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]  // Ensure the GameObject has a TextMeshProUGUI component
public class TypingEffect : MonoBehaviour
{
    [HideInInspector] public bool startTyping = true;
    private bool startedTyping = false;
    private TextMeshProUGUI textMeshPro;  // Reference to the TextMeshProUGUI component
    private string fullText;  // The full text to be typed out
    private float textScale;
    [SerializeField] private float typingSpeed = 0.02f;  // Time between each character
    [SerializeField] private float typingPeriodPause = 0.1f;  // Pause time after typing a period

    private void OnEnable()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();  // Get the TextMeshProUGUI component
        fullText = textMeshPro.text;  // Store the full text from the component
        textScale = textMeshPro.fontSize;  // Store the font size for later use
        textMeshPro.enableAutoSizing = false;  // Disable auto sizing to maintain the set font size
        textMeshPro.fontSize = textScale; // Sets the font size to the original value
        
        if (!startedTyping)
        {
            textMeshPro.text = "";  // Clear any initial text
        }
    }

    private void OnDisable()
    {
        textMeshPro.text = fullText; // Types out text immediatly if disabled to avoid some bugs
    }

    private void Update()
    {
        if (startTyping && !startedTyping)
        {
            startedTyping = true;
            StartCoroutine(TypeText()); // Start the typing effect
        }
    }

    private IEnumerator TypeText()
    {
        // Stores the current segment of text, excluding any tags
        string currentTextSegment = "";
        bool insideTag = false;

        // Go through each character in the fullText
        for (int i = 0; i < fullText.Length; i++)
        {
            char letter = fullText[i];

            // If the letter is the start of a tag (<), start adding characters to the tag
            if (letter == '<')
            {
                insideTag = true;
                currentTextSegment += letter;
                continue;  // Skip the rest of this iteration to avoid adding the < to the text
            }

            // If the letter is the end of a tag (>), stop adding to the tag
            if (letter == '>')
            {
                insideTag = false;
                currentTextSegment += letter;
                textMeshPro.text += currentTextSegment;  // Apply the tag instantly
                currentTextSegment = "";  // Reset for the next text part
                yield return null;  // Wait a frame to apply the tag before typing the rest
                continue;
            }

            // If we're inside a tag, just accumulate the tag text
            if (insideTag)
            {
                currentTextSegment += letter;
            }
            else
            {
                // When not inside a tag, we type out the character
                textMeshPro.text += letter;
                if (letter == "."[0] || letter == ","[0])
                {
                    yield return new WaitForSecondsRealtime(typingSpeed + typingPeriodPause);  // Use unscaled time to ignore time scale
                }
                yield return new WaitForSecondsRealtime(typingSpeed);  // Use unscaled time to ignore time scale
            }
        }
    }
}