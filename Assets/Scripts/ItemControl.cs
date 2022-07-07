using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    public static ItemControl instance;

    public GameObject guard;
    public GameObject item;
    public GameObject UIPanel;

    public float time = 5f;
    public float xLeftBoundary = 5f;
    public float xRightBoundary = 5f;
    public float zLeftBoundary = 5f;
    public float zRightBoundary = 5f;
    public float yBoundary = 5f;
    public int itemNum = 5;

    private void Start()
    {
        instance = this;
        createItem();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void createItem()
    {
        for(int i = 0; i < itemNum; i++)
        {
            float x = Random.Range(xLeftBoundary, xRightBoundary);
            float y = yBoundary;
            float z = Random.Range(zLeftBoundary, zRightBoundary);
            Instantiate(item, new Vector3(x, y, z), Quaternion.identity);
        }
    }

    public void getQuality(GameObject target)
    {
        int rand = Random.Range(0, 3);
        if(rand == 0)
        {
            GameObject obj = Instantiate(guard, target.transform.position, Quaternion.identity);
            obj.transform.parent = target.transform;
            obj.transform.localPosition = Vector3.zero;
            print(obj.transform.position);
            obj.transform.localScale = new Vector3(4, 4, 4);
            StartCoroutine(invincible(target, obj));
        }
        else if(rand == 1)
        {
            UIPanel.GetComponent<Text>().text = "Power¡ü";
            StartCoroutine(waitTime(5f));
            StartCoroutine(fireTimeReduce(target));
        }
        else if(rand == 2)
        {
            UIPanel.GetComponent<Text>().text = "HP¡ü";
            StartCoroutine(waitTime(2f));
            target.GetComponent<TankHealth>().add(10);
        }

    }

    IEnumerator waitTime(float time)
    {
        UIPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        UIPanel.SetActive(false);
    }

    public void addItem()
    {
        float x = Random.Range(xLeftBoundary, xRightBoundary);
        float y = yBoundary;
        float z = Random.Range(zLeftBoundary, zRightBoundary);
        Instantiate(item, new Vector3(x, y, z), Quaternion.identity);
    }

    IEnumerator invincible(GameObject target,GameObject obj)
    {
        target.GetComponent<TankHealth>().isinvincible = true;
        yield return new WaitForSeconds(time);
        target.GetComponent<TankHealth>().isinvincible = false;
        Destroy(obj);
    }
    IEnumerator fireTimeReduce(GameObject target)
    {
        float lastFireTime = target.GetComponent<TankShooting>().FireTime;
        target.GetComponent<TankShooting>().FireTime = 0.1f;
        yield return new WaitForSeconds(time);
        target.GetComponent<TankShooting>().FireTime = lastFireTime;
    }
}
