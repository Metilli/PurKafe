
using UnityEngine;

[System.Serializable]
public class Product: MonoBehaviour
{
    public string productName;
    public double price;
    public int count = 1;
    public CategoryEnum categoryName;
}