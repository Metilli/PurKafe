using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeskDetails : MonoBehaviour
{
    private string filePath;
    private string deskDetailsFilePath;
    public GameObject CategoryObject;
    public GameObject ProductObject;
    public GameObject CategoryContainer;
    public GameObject LeftProductContainer;
    public GameObject RightProductContainer;
    public Text totalPriceText;
    public Text selectedPriceText;

    private readonly int spaceBetweenCategory = 115;
    private readonly int firstSpaceCategory = 70;
    private readonly int categoryHeight = 100;
    private readonly int categoryWidthPaddinng = 20;

    private readonly int spaceBetweenLeftProducts = 85;
    private readonly int firstSpaceLeftProduct = 70;
    private readonly int leftProductHeight = 75;
    private readonly int leftProductPadding = 20;

    private readonly int firstSpaceRightProduct = 125;
    private readonly int spaceBetweenRightProducts = 50;
    private readonly int rightProductHeight = 40;

    private Categories categories;

    private DeskData deskData = new DeskData();
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/CategoryData.json";
        deskDetailsFilePath = Application.persistentDataPath + "/DeskData.json";
        SetupPanels();
        GetCategories();
        GetDeskProducts();
    }

    void SetupPanels()
    {
        GameObject canvas = GameObject.Find("Canvas");
        RectTransform rectCanvas = canvas.GetComponent<RectTransform>();
        var totalWidth = rectCanvas.rect.width;
        var totalHeight = rectCanvas.rect.height;
        var menuPanelWidth = GameObject.Find("MenuPanel").GetComponent<RectTransform>().rect.width;
        var topPanelHeight = GameObject.Find("TopPanel").GetComponent<RectTransform>().rect.height;

        RectTransform rectMidPanel = GameObject.Find("MidPanel").GetComponent<RectTransform>();
        var midPanelWidth = totalWidth - menuPanelWidth;
        var midPanelHeight = totalHeight - topPanelHeight;
        rectMidPanel.sizeDelta = new Vector2(midPanelWidth,midPanelHeight);
        var midPanelX = midPanelWidth / 2 + menuPanelWidth;
        var midPanelY = -midPanelHeight / 2 - topPanelHeight;
        rectMidPanel.anchoredPosition = new Vector2(midPanelX,midPanelY);

        RectTransform rectLeftPanel = GameObject.Find("LeftPanel").GetComponent<RectTransform>();
        var leftPanelWidth = midPanelWidth / 3;
        rectLeftPanel.sizeDelta = new Vector2(leftPanelWidth, 0);
        rectLeftPanel.anchoredPosition = new Vector2(leftPanelWidth/2,0);

        RectTransform rectRightPanel = GameObject.Find("RightPanel").GetComponent<RectTransform>();
        var rightPanelWidth = (midPanelWidth / 3)*2;
        rectRightPanel.sizeDelta = new Vector2(rightPanelWidth, 0);
        rectRightPanel.anchoredPosition = new Vector2(-rightPanelWidth / 2, 0);

    }

    private void GetDeskProducts()
    {
        if (System.IO.File.Exists(filePath))
        {
            string text = System.IO.File.ReadAllText(filePath);
            deskData = JsonUtility.FromJson<DeskData>(text);

            for (int i = 0; i < deskData.products.Count; i++)
            {
                AddProductToDesk(deskData.products[i], i);
            }
        }
    }

    void GetCategories()
    {
        if (System.IO.File.Exists(filePath))
        {
            string text = System.IO.File.ReadAllText(filePath);
            categories = JsonUtility.FromJson<Categories>(text);


            RectTransform rectCategoryContainer = CategoryContainer.GetComponent<RectTransform>();
            var categoryContainerHeight = categories.categories.Count * spaceBetweenCategory + firstSpaceCategory * 2;
            var categoryContainerWidth = GameObject.Find("LeftPanelMask").GetComponent<RectTransform>().rect.width;
            rectCategoryContainer.sizeDelta = new Vector2(categoryContainerWidth,categoryContainerHeight);
            rectCategoryContainer.anchoredPosition = new Vector2(0,-categoryContainerHeight/2);
            for (int i = 0; i < categories.categories.Count; i++)
            {
                GameObject categoryObj = Instantiate(CategoryObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                categoryObj.transform.SetParent(CategoryContainer.transform, false);
                categoryObj.GetComponentInChildren<Text>().text = categories.categories[i].name.ToString();
                RectTransform rect = categoryObj.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(categoryContainerWidth - categoryWidthPaddinng * 2, categoryHeight);
                rect.anchoredPosition = new Vector2(0,-firstSpaceCategory-i*(spaceBetweenCategory));
                categoryObj.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)this.OpenCategoryProducts);
            }
        }
        else
        {
            categories = new Categories();

            Category coffeeCategory = new Category { name = CategoryEnum.Coffee };

            ProductModal filterCoffee = new ProductModal { productName = "Filtre kahve", price = 10, categoryName=CategoryEnum.Coffee };
            coffeeCategory.products.Add(filterCoffee);
            ProductModal espresso = new ProductModal { productName = "Espresso", price = 10, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(espresso);
            ProductModal turkishCoffee = new ProductModal { productName = "Türk kahvesi", price = 10, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(turkishCoffee);
            ProductModal latte = new ProductModal { productName = "Latte", price = 12, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(latte);
            ProductModal Americano = new ProductModal { productName = "Americano", price = 12, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(Americano);
            ProductModal CaramelleMachiato = new ProductModal { productName = "Caramelle machiato", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(CaramelleMachiato);
            ProductModal PumpkinLatte = new ProductModal { productName = "Pumpkin latte", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(PumpkinLatte);
            ProductModal Tchailatte = new ProductModal { productName = "Tchai tea latte", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(Tchailatte);
            ProductModal Mocha = new ProductModal { productName = "Mocha", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(Mocha);
            ProductModal ToffeeNut = new ProductModal { productName = "ToffeeNut latte", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(ToffeeNut);
            ProductModal Cappucino = new ProductModal { productName = "Cappucino", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(Cappucino);
            ProductModal Flat = new ProductModal { productName = "Flat white", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(Flat);
            ProductModal Frappucino = new ProductModal { productName = "Frappucino", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(Frappucino);
            ProductModal Irish = new ProductModal { productName = "Irish coffee", price = 15, categoryName = CategoryEnum.Coffee };
            coffeeCategory.products.Add(Irish);

            categories.categories.Add(coffeeCategory);

            Category teaCategory = new Category { name = CategoryEnum.Tea };

            ProductModal tea = new ProductModal { productName = "Siyah çay", price = 4, categoryName = CategoryEnum.Tea };
            teaCategory.products.Add(tea);
            ProductModal bigTea = new ProductModal { productName = "Fincan siyah çay", price = 8, categoryName = CategoryEnum.Tea };
            teaCategory.products.Add(bigTea);
            ProductModal fruitTea = new ProductModal { productName = "Meyve çayı", price = 5, categoryName = CategoryEnum.Tea };
            teaCategory.products.Add(fruitTea);
            ProductModal herbalTea = new ProductModal { productName = "Bitki çayı", price = 10, categoryName = CategoryEnum.Tea };
            teaCategory.products.Add(herbalTea);

            categories.categories.Add(teaCategory);

            Category softDrinkCategory = new Category { name = CategoryEnum.SoftDrink };

            ProductModal cola = new ProductModal { productName = "Kola", price = 8, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(cola);
            ProductModal Sprite = new ProductModal { productName = "Sprite", price = 8, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(Sprite);
            ProductModal Fanta = new ProductModal { productName = "Fanta", price = 8, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(Fanta);
            ProductModal Salep = new ProductModal { productName = "Salep", price = 15, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(Salep);
            ProductModal hotChocolate = new ProductModal { productName = "Sıcak çikolata", price = 15, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(hotChocolate);
            ProductModal lemonade = new ProductModal { productName = "Limonata", price = 10, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(lemonade);
            ProductModal churchill = new ProductModal { productName = "Churchill", price = 10, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(churchill);
            ProductModal Soda = new ProductModal { productName = "Soda", price = 5, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(Soda);
            ProductModal Milkshake = new ProductModal { productName = "Milkshake", price = 10, categoryName = CategoryEnum.SoftDrink };
            softDrinkCategory.products.Add(Milkshake);

            categories.categories.Add(softDrinkCategory);

            Category mealCategory = new Category { name = CategoryEnum.Meal };

            ProductModal tost = new ProductModal { productName = "Kaşarlı tost", price = 15, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(tost);
            ProductModal tost2 = new ProductModal { productName = "Karışık tost", price = 20, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(tost2);
            ProductModal hamburger = new ProductModal { productName = "Hamburger", price = 30, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(hamburger);
            ProductModal doubleHamburger = new ProductModal { productName = "Double hamburger", price = 35, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(doubleHamburger);
            ProductModal schnitzel = new ProductModal { productName = "Schnitzel", price = 30, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(schnitzel);
            ProductModal chickenBurger = new ProductModal { productName = "Tavuk burger", price = 25, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(chickenBurger);
            ProductModal Menemen = new ProductModal { productName = "Menemen", price = 20, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(Menemen);
            ProductModal cake = new ProductModal { productName = "Pasta", price = 15, categoryName = CategoryEnum.Meal };
            mealCategory.products.Add(cake);

            categories.categories.Add(mealCategory);

            Category cocktailCategory = new Category { name = CategoryEnum.Cocktail };

            ProductModal Blend = new ProductModal { productName = "Blend smurf", price = 10, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Blend);
            ProductModal Goblin = new ProductModal { productName = "Blend goblin", price = 10, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Goblin);
            ProductModal Pink = new ProductModal { productName = "Pink panter", price = 10, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Pink);
            ProductModal Mangoville = new ProductModal { productName = "Mangoville", price = 10, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Mangoville);
            ProductModal Kuzey = new ProductModal { productName = "Kuzey ışıkları", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Kuzey);
            ProductModal Unicorn = new ProductModal { productName = "Unicorn", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Unicorn);
            ProductModal Hulk = new ProductModal { productName = "Hulk", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Hulk);
            ProductModal Blanka = new ProductModal { productName = "Blanka", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Blanka);
            ProductModal Vega = new ProductModal { productName = "Vega", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Vega);
            ProductModal Coconut = new ProductModal { productName = "Coconut cloud", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Coconut);
            ProductModal Frozen = new ProductModal { productName = "Frozen lemonade", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Frozen);
            ProductModal Blue = new ProductModal { productName = "Blue Hawai", price = 15, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Blue);
            ProductModal Mojito = new ProductModal { productName = "Mojito lime", price = 20, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Mojito);
            ProductModal Night = new ProductModal { productName = "Night Rider", price = 20, categoryName = CategoryEnum.Cocktail };
            cocktailCategory.products.Add(Night);

            categories.categories.Add(cocktailCategory);

            string saveText = JsonUtility.ToJson(categories);
            System.IO.File.WriteAllText(filePath, saveText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCategoryProducts()
    {
        string selectedCategoryText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;

        LeftProductContainer.SetActive(true);
        CategoryContainer.SetActive(false);

        GameObject.Find("LeftPanelMask").GetComponent<ScrollRect>().content = LeftProductContainer.GetComponent<RectTransform>();

        RectTransform rectLeftProductContainer = LeftProductContainer.GetComponent<RectTransform>();

        var leftProductContainerHeight = 0;
        var leftProductContainerWidth = GameObject.Find("LeftPanelMask").GetComponent<RectTransform>().rect.width;

        foreach (Category category in categories.categories)
        {
            if(category.name.ToString() == selectedCategoryText)
            {
                leftProductContainerHeight = category.products.Count * spaceBetweenLeftProducts + firstSpaceLeftProduct * 2;
                rectLeftProductContainer.sizeDelta = new Vector2(leftProductContainerWidth, leftProductContainerHeight);
                rectLeftProductContainer.anchoredPosition = new Vector2(0, -leftProductContainerHeight / 2);
                for (int i = 0; i < category.products.Count; i++)
                {
                    GameObject leftProductObj = Instantiate(CategoryObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    leftProductObj.transform.SetParent(LeftProductContainer.transform, false);
                    leftProductObj.GetComponentInChildren<Text>().text = category.products[i].productName;
                    RectTransform rect = leftProductObj.GetComponent<RectTransform>();
                    rect.sizeDelta = new Vector2(leftProductContainerWidth - leftProductPadding * 2, leftProductHeight);
                    rect.anchoredPosition = new Vector2(0, -firstSpaceLeftProduct - i * (spaceBetweenLeftProducts));
                    leftProductObj.GetComponent<Button>().onClick.AddListener(delegate { AddProductFromButtonToDesk(); });
                }
            }
        }
    }

    public void AddProductFromButtonToDesk()
    {
        string selectedProductText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;

        int index = -1;
        foreach (Category category in categories.categories)
        {
            foreach (ProductModal product in category.products)
            {
                if (product.productName == selectedProductText)
                {
                    bool isProductExist = false;
                    GameObject[] rightProductObjs = GameObject.FindGameObjectsWithTag("RightContainerProduct");
                    index = rightProductObjs.Length;
                    foreach (GameObject rightProduct in rightProductObjs)
                    {
                        Text[] inListProductTexts = rightProduct.GetComponentsInChildren<Text>();
                        if (isProductExist) break;
                        foreach (Text text in inListProductTexts)
                        {
                            if (text.name == "ProductName" && text.text == selectedProductText)
                            {
                                var dropDown = rightProduct.GetComponentInChildren<Dropdown>();
                                List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();
                                foreach (Dropdown.OptionData item in dropDown.options)
                                {
                                    optionList.Add(new Dropdown.OptionData { text = item.text });
                                }
                                int increasedCount = (Convert.ToInt32(optionList[optionList.Count - 1].text) + 1);
                                Dropdown.OptionData nextCount = new Dropdown.OptionData { text = increasedCount.ToString() };
                                optionList.Add(nextCount);
                                dropDown.ClearOptions();
                                dropDown.AddOptions(optionList);
                                dropDown.value = dropDown.options.Count - 1;
                                isProductExist = true;
                            }
                            if (text.name == "TotalPriceText" && isProductExist)
                            {
                                text.text = (product.price * rightProduct.GetComponentInChildren<Dropdown>().options.Count).ToString();
                                rightProduct.GetComponent<Product>().count += 1;
                            }
                        }
                    }
                    if (isProductExist == false)
                    {
                        AddProductToDesk(product, index);
                    }
                }
            }
        }
    }

    private void AddProductToDesk(ProductModal productModal,int index)
    {
        GameObject rightProductObj = Instantiate(ProductObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        var product = rightProductObj.GetComponent<Product>();
        product.productName = productModal.productName;
        product.price = productModal.price;
        product.count = productModal.count;
        product.categoryName = productModal.categoryName;
        rightProductObj.transform.SetParent(RightProductContainer.transform, false);
        RectTransform rect = rightProductObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(RightProductContainer.GetComponent<RectTransform>().rect.width, rightProductHeight);
        rect.anchoredPosition = new Vector2(0, -firstSpaceRightProduct - index * (spaceBetweenRightProducts));

        Text[] texts = rightProductObj.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            if (text.name == "ProductName")
            {
                text.text = product.productName;
            }
            else if (text.name == "UnitPriceText" || text.name == "TotalPriceText")
            {
                text.text = product.price.ToString();
            }
        }
        rightProductObj.GetComponentInChildren<Dropdown>().AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData { text = "1" } });
        rightProductObj.GetComponentInChildren<Dropdown>().onValueChanged.AddListener(delegate { RightProductDropDownValueChange(rightProductObj); });
        rightProductObj.GetComponentInChildren<Toggle>().onValueChanged.AddListener(delegate { RightProductToggleChanged(rightProductObj); });
    }

    private void RightProductDropDownValueChange(GameObject rightProductObj)
    {
        print("value changed");
    }

    private void RightProductToggleChanged(GameObject rightProductObj)
    {

    }

    private void updatePriceInfo()
    {
        GameObject[] rightProductObjs = GameObject.FindGameObjectsWithTag("RightContainerProduct");
        double totalPrice = 0;
        foreach (GameObject productObj in rightProductObjs)
        {

        }
    }

    public void BackToCategory()
    {
        LeftProductContainer.SetActive(false);
        CategoryContainer.SetActive(true);
        GameObject.Find("LeftPanelMask").GetComponent<ScrollRect>().content = CategoryContainer.GetComponent<RectTransform>();
    }
}
