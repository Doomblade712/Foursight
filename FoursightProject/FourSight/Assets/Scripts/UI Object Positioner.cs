using UnityEngine;

public class UIObjectPositioner : MonoBehaviour
{
    public RectTransform objectToPosition;
    public int widthDivier = 2;
    public int heightDivier = 2;
    public float widthMutiplier = 1.0f;
    public float heightMutiplier = 1.0f;

    public bool updatePosition = false;

    void Start()
    {
        SetUIObjectPosition();
    }

    void Update()
    {
        if (updatePosition)
        {
            SetUIObjectPosition();
        }
    }

    public void SetUIObjectPosition()
    {
        if (objectToPosition != null && widthDivier != 0 && heightDivier != 0)
        {
            float anchorX = widthMutiplier / widthDivier;
            float anchorY = heightMutiplier / heightDivier;

            objectToPosition.anchorMin = new Vector2(anchorX, anchorY);
            objectToPosition.anchorMax = new Vector2(anchorX, anchorY);
            objectToPosition.pivot = new Vector2(0.5f, 0.5f);

            objectToPosition.anchoredPosition = Vector2.zero;
        }
    }
}
