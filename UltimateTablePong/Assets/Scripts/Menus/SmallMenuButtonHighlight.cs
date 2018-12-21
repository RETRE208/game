using UnityEngine;
using UnityEngine.EventSystems;

public class SmallMenuButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Vector2 buttonNormalSize;
    private Vector2 buttonBigSize;

    private static readonly float DELTA = 30;
    private static readonly float EASING_FACTOR = 2;

    private bool grow;
    private bool shrink;

    private void Start()
    {
        buttonNormalSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        buttonBigSize = new Vector2(buttonNormalSize.x + DELTA, buttonNormalSize.y + DELTA);
        grow = false;
        shrink = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            goBackToNormal();
        }

        if (grow)
        {
            var newSizeX = (gameObject.GetComponent<RectTransform>().sizeDelta.x + buttonBigSize.x) / EASING_FACTOR;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newSizeX, buttonNormalSize.y);

            if ((buttonBigSize.x - gameObject.GetComponent<RectTransform>().sizeDelta.x) < 0.5f)
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = buttonBigSize;
                grow = false;
            }
        }

        if (shrink)
        {
            var newSizeX = (gameObject.GetComponent<RectTransform>().sizeDelta.x + (buttonNormalSize.x)) / EASING_FACTOR;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newSizeX, buttonNormalSize.y);

            if ((gameObject.GetComponent<RectTransform>().sizeDelta.x - (buttonNormalSize.x)) < 0.5f)
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = buttonNormalSize;
                shrink = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        shrink = false;
        grow = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        grow = false;
        shrink = true;
    }

    public void goBackToNormal()
    {
        grow = false;
        shrink = false;
        gameObject.GetComponent<RectTransform>().sizeDelta = buttonNormalSize;
    }
}
