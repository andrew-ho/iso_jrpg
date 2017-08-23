using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item {
    public string name;
    public int quantity;

    public string description;

    public Item(string name, string description, int quantity)
    {
        this.name = name;
        this.description = description;
        this.quantity = quantity;
    }

   public Item()
    {

    }
	
}
