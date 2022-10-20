using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt_Ctrl : MonoBehaviour
{
    public float rollSpeed;
    public Vector2 direction;
    public List<GameObject> Objs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= Objs.Count - 1; i++)
        {
            Objs[i].GetComponent<Rigidbody2D>().AddForce((rollSpeed * direction * Time.deltaTime));
        }
    }

    //벨트와 오브젝트가 닿았을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Objs.Add(collision.gameObject);
    }

    //벨트와 오브젝트가 떨어졌을 때
    private void OnCollisionExit2D(Collision2D collision)
    {
        Objs.Remove(collision.gameObject);
    }
}
