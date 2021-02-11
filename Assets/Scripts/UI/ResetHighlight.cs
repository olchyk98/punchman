using System.Collections.Generic;
using UnityEngine;

public class ResetHighlight : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> myButtons = new List<GameObject>();
    
    private List<RectTransform> selectionTransforms = new List<RectTransform>();
    
    private static readonly int StopHover = Animator.StringToHash("StopHover");
    private static readonly int Hover = Animator.StringToHash("Hover");

    private void Awake()
    {
        foreach (var button in myButtons)
        {
            for (var i = 0; i < button.transform.childCount; i++)
            {
                var child = button.transform.GetChild(i);
                if (child.gameObject.name == "Select")
                {
                    selectionTransforms.Add(child.GetComponent<RectTransform>());
                }
            }
        }
    }

    public void Reset()
    {
        foreach (var rect in selectionTransforms)
        {
            rect.localScale = new Vector3(0,1,1);
        }
    }
}
