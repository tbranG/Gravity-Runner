using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFix : MonoBehaviour
{
    public GameObject[] checkObjs;
    private int current_index;

    // Start is called before the first frame update
    void Awake()
    {
        checkObjs = new GameObject[3];
        current_index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkObjs[0] != null && checkObjs[1] != null)
        {
            if (checkObjs[0] != null && checkObjs[1] != null && checkObjs[2] != null)
            {
                for(int x = 0; x <= 2; x++)
                {
                    checkObjs[x].transform.position = new Vector3(transform.position.x, checkObjs[x].transform.position.y, 0f);
                }
            } else
            {
                for(int y = 0; y <= 1; y++)
                {
                    checkObjs[y].transform.position = new Vector3(transform.position.x, checkObjs[y].transform.position.y, 0f);
                }
            }                    
        }
    }

    IEnumerator RefreshVariable(int i)
    {
        yield return new WaitForSeconds(1f);
        checkObjs[i] = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkObjs[current_index] = collision.gameObject;
        StartCoroutine(RefreshVariable(current_index));

        current_index++;
        if(current_index > checkObjs.Length - 1)
        {
            current_index = 0;
        }
    }
}
