using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{

    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;

    [SerializeField] private Color highlightColor = Color.green;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Vector3 buttonReleaseScaleVector = new Vector3(0.2f, 0.2f, 1f);
    [SerializeField] private Vector3 buttonPressScaleVector = new Vector3(0.5f, 0.5f, 1f);

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnMouseDown()
    { 
        transform.localScale = buttonPressScaleVector;
    }


    private void OnMouseExit()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = Color.white;

    }

    private void OnMouseEnter()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = highlightColor;
    }

    private void OnMouseUp()
    {
        transform.localScale = buttonReleaseScaleVector;

        if (targetObject != null)
            targetObject.SendMessage(targetMessage);
    }
}
