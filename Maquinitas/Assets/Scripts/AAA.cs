using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAA : MonoBehaviour
{

	[SerializeField]
	int money;

	[SerializeField]
	int maxStock;

	public Items item1;

    // Start is called before the first frame update
    void Start()
    {
		item1.stock = maxStock;
    }

    // Update is called once per frame
    void Update()
    {
		
    }

    public void Buy()
    {
        if (item1.stock > 0)
        {
            item1.stock -= 1;
            money += item1.sellPrice;


            Debug.Log(item1.stock);

        }
    }

}