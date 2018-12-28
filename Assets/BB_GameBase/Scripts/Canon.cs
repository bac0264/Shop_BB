using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    bool touching = false;
    Vector2 lastTouchPos;
    public static float screenWidth;
    BoxCollider2D boxCollider;
    void Start()
    {
        screenWidth = Camera.main.orthographicSize * 1f / Screen.height * Screen.width;
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        do
        {
            if (touching)
            {
                GameObject bullet = PoolManager.Instance.Instantiate("bullet");
                bullet.transform.position = transform.position;
                bullet.transform.DOMoveY(Camera.main.orthographicSize + 1, 0.3f).SetEase(Ease.Linear);
                PoolManager.Instance.Remove("bullet", bullet,0.3f);
            }
            yield return new WaitForSeconds(0.1f);
        } while (true);
    }
    void BulletOutScreen(GameObject bullet)
    {
        
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) {
            touching = true;
            lastTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (touching)
        {
            Vector2 curPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 oldCanonPos = transform.position;

            transform.Translate(new Vector3(curPos.x - lastTouchPos.x, 0, 0));
            if (transform.position.x < -screenWidth + boxCollider.size.x / 2) transform.position = new Vector3(-screenWidth + boxCollider.size.x / 2, transform.position.y, 0);
            if (transform.position.x > screenWidth - boxCollider.size.x / 2) transform.position = new Vector3(screenWidth - boxCollider.size.x / 2, transform.position.y, 0);

            float angleWheelRotate = (oldCanonPos.x-transform.position.x  ) * 180f / (transform.GetChild(0).GetComponent<CircleCollider2D>().radius * 2 * Mathf.PI);
            transform.GetChild(0).Rotate(new Vector3(0, 0, angleWheelRotate));
            transform.GetChild(1).Rotate(new Vector3(0, 0, angleWheelRotate));
            lastTouchPos = curPos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touching = false;
        }
    }

}
