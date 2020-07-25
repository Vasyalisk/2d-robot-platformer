using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject[] dependencies;


    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDependencies()
    {
        foreach (GameObject obj in dependencies)
        {
            IResettable resettable = obj.GetComponent<IResettable>();
            if (resettable == null)
            {
                throw new System.NullReferenceException("Dependency is not resettable.");
            }
            resettable.ResetObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.lastCheckPoint = this;
        gameObject.SetActive(false);
    }
}
