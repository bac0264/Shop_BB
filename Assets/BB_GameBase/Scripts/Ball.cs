using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int Size=1;
    public int Number;
    // Start is called before the first frame update
    void Start()
    {
        Number = Random.Range(10, 40);
        UpdateNumber();
    }
    void UpdateNumber()
    {
        GetComponentInChildren<TextMeshPro>().text = Number.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            PoolManager.Instance.Remove("bullet", collision.gameObject);
            Number--;
            if (Number == 0)
            {
                
                    GameObject b1 = PoolManager.Instance.Instantiate("ball");
                    GameObject b2 = PoolManager.Instance.Instantiate("ball");
                    b1.transform.position = transform.position;
                    b2.transform.position = transform.position;

                    b1.GetComponent<Ball>().Size = Size-1;
                    b2.GetComponent<Ball>().Size = Size-1;
                    b1.GetComponent<Rigidbody2D>().isKinematic = false;
                    b1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 200));
                    b2.GetComponent<Rigidbody2D>().AddForce(new Vector2(250, 200));
                
                PoolManager.Instance.Remove("ball", gameObject);
            }
            UpdateNumber();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
