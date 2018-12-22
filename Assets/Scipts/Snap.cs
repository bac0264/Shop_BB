using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Snap : MonoBehaviour
{
    public RectTransform panel; // to hold the scrollView
    public List<Button> btnn = new List<Button>(); // 
    public RectTransform center; // Center to compare the distance for each iten 
    public float[] distance; // All Item's center
    private bool dragging = false;
    public int bttnDistance;
    public int minButtonNum;
    // Use this for initialization
    public void Awake()
    {
        distance = new float[btnn.Count];
        Debug.Log("btnn: " + btnn.Count);
        Debug.Log("btnn1: " + btnn[1].GetComponent<RectTransform>().anchoredPosition.x +
            " btn0: " + btnn[0].GetComponent<RectTransform>().anchoredPosition.x + "distance: "+ (btnn[1].GetComponent<RectTransform>().anchoredPosition.x - btnn[0].GetComponent<RectTransform>().anchoredPosition.x));
        // distance between
        bttnDistance = (int)Mathf.Abs(btnn[1].GetComponent<RectTransform>().anchoredPosition.x - btnn[0].GetComponent<RectTransform>().anchoredPosition.x);
        // startSnap = true;
        LerpToImage(minButtonNum * (-bttnDistance));
    }
    private void Update()
    {
        for (int i = 0; i < btnn.Count; i++)
        {
            distance[i] = (int)Mathf.Abs(center.transform.position.x - btnn[i].transform.position.x);
        }
        float minDistance = Mathf.Min(distance);
        for (int a = 0; a < btnn.Count; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }
        if (!dragging)
        {
            LerpToImage(minButtonNum * (-bttnDistance));
        }

    }

    public void LerpToImage(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 5);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }
    public void EndDrag()
    {
        dragging = false;
    }

}
