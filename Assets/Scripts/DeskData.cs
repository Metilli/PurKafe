
using System;
using System.Collections.Generic;

[Serializable]
public class DeskData
{
    public string deskName;
    public List<ProductModal> products;
    public double totalPrice;
    public DateTime date;
}

[System.Serializable]
public class Categories
{
    public List<Category> categories = new List<Category>();
}
[System.Serializable]
public class Category
{
    public CategoryEnum name;
    public List<ProductModal> products = new List<ProductModal>();
}


[System.Serializable]
public class ProductModal
{
    public string productName;
    public double price;
    public int count = 1;
    public CategoryEnum categoryName;
}

public enum CategoryEnum
{
    Coffee,
    Cocktail,
    Tea,
    Meal,
    SoftDrink
}