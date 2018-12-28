using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGenerate());
    }
    IEnumerator StartGenerate()
    {
        do
        {
            yield return new WaitForSeconds(5f);
            GameObject obj = PoolManager.Instance.Instantiate("ball");



            int size = Random.Range(1, 5);
            obj.transform.localScale = new Vector3(1f / size, 1f / size, 1f / size);
            obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

            float dau = (Random.Range(0, 2) - 0.5f) * 2;
            float bound = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            obj.transform.position = new Vector3((Canon.screenWidth + bound / 2) * dau, Random.Range(0f, 6f), Random.Range(0f,1f));

            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.GetComponent<Rigidbody2D>().isKinematic = true;

            obj.transform.DOMoveX((Canon.screenWidth - bound / 2) * dau, 3f).OnComplete(() => BallSlideComplete(obj,dau)).SetEase(Ease.Linear);

        } while (true);
    }
    void BallSlideComplete(GameObject obj,float dau)
    {
        obj.GetComponent<Rigidbody2D>().isKinematic = false;
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-dau * 150, 100));
        obj.transform.DORotate(new Vector3(0,0,obj.transform.eulerAngles.z + 360f), 8f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
