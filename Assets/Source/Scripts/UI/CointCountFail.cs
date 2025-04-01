using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CointCountFail : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayCount(int count)
    {
        _text.text = "Your score:  <sprite name=\"coin\">" + count;
    }
}
