using UnityEngine;
using UnityEngine.UI;

public class ColoredClick : MonoBehaviour
{
    private Image image;

    [SerializeField] private Color Disabled;
    [SerializeField] private Color Inactive;
    [SerializeField] private Color Hover;
    [SerializeField] private Color Down;

    private Color[] myColors;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        myColors = new []{Disabled,Inactive,Hover,Down};
    }

    public void UpdateClick(int index)
    {
        image.color = myColors[index];
    }
}
