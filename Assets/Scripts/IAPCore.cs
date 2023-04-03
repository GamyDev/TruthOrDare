using System;
using UnityEngine;
using UnityEngine.Purchasing; //библиотека с покупками, будет доступна когда активируем сервисы
using TMPro;

public class IAPCore : MonoBehaviour, IStoreListener //для получения сообщений из Unity Purchasing
{
  //  [SerializeField] private GameObject panelAds;
   // [SerializeField] private GameObject panelVIP;

   // [SerializeField] private GameObject panelAds_Done;
   // [SerializeField] private GameObject panelVIP_Done;

    private static IStoreController m_StoreController;          //доступ к системе Unity Purchasing
    private static IExtensionProvider m_StoreExtensionProvider; // подсистемы закупок для конкретных магазинов

   // public static string noads = "noads"; //одноразовые - nonconsumable
    public static string vip = "com.truthordare.cardgames.subscription.weekly";
    // public static string coins151 = "coins151"; //многоразовые - consumable

    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textPrice2;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject SubActive;
    [SerializeField] private ThemesWindow themesWindow;


    void Start()
    {
        if (m_StoreController == null) //если еще не инициализаровали систему Unity Purchasing, тогда инициализируем
        {
            InitializePurchasing();
        }
        LocalizationManager.OnLanguageChange += OnLanguageChange;

        var subscriptionProduct = m_StoreController.products.WithID(vip);

        try
        {
            var isSubscribed = IsSubscribedTo(subscriptionProduct);
            gameManager.isSubscribe = isSubscribed;

        }
        catch (StoreSubscriptionInfoNotSupportedException)
        {

        }
    }

    private void OnLanguageChange()
    {
        TextPrice();
    }

    private void OnDestroy()
    {
        LocalizationManager.OnLanguageChange -= OnLanguageChange;
    }

    public void InitializePurchasing()
    {
        if (IsInitialized()) //если уже подключены к системе - выходим из функции
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Прописываем свои товары для добавления в билдер
     //   builder.AddProduct(noads, ProductType.NonConsumable);
        builder.AddProduct(vip, ProductType.Subscription); //или ProductType.Subscription
       // builder.AddProduct(coins151, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void Buy_vip()
    {
        BuyProductID(vip);
    }

    public void SetSubscribtion(bool value)
    {
        gameManager.isSubscribe = value; 
    }

    public void TextInvoke()
    {
        Invoke("TextPrice", 0.2f);
    }

    public void TextPrice()
    {
        Product product = m_StoreController.products.WithID(vip);
        decimal price = product.metadata.localizedPrice == 0 ? 0.99M : product.metadata.localizedPrice;
        textPrice.text = textPrice.text.Replace("[price]", price.ToString());

        decimal price2 = product.metadata.localizedPrice == 0 ? 0.99M : product.metadata.localizedPrice;
        textPrice2.text = textPrice2.text.Replace("[price]", price.ToString());
        Debug.Log("TextPrice");
    }



    bool IsSubscribedTo(Product subscription)
    {
        // If the product doesn't have a receipt, then it wasn't purchased and the user is therefore not subscribed.
        if (subscription.receipt == null)
        {
            SetSubscribtion(false);
            return false;
        }

        //The intro_json parameter is optional and is only used for the App Store to get introductory information.
        var subscriptionManager = new SubscriptionManager(subscription, null);

        // The SubscriptionInfo contains all of the information about the subscription.
        // Find out more: https://docs.unity3d.com/Packages/com.unity.purchasing@3.1/manual/UnityIAPSubscriptionProducts.html
        var info = subscriptionManager.getSubscriptionInfo();

        return info.isSubscribed() == Result.True;
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized()) //если покупка инициализирована 
        {
            Product product = m_StoreController.products.WithID(productId); //находим продукт покупки 

            if (product != null && product.availableToPurchase) //если продукт найдет и готов для продажи
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product); //покупаем
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) //контроль покупок
    {
       if (String.Equals(args.purchasedProduct.definition.id, vip, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            if (!PlayerPrefs.HasKey("windowShowSub"))
            {
                PlayerPrefs.SetInt("windowShowSub", 1);
                SubActive.SetActive(true);
            }
            SetSubscribtion(true);
            themesWindow.DisplayThemes();

        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            SetSubscribtion(false);
            themesWindow.DisplayThemes();
        }

        return PurchaseProcessingResult.Complete;
    }

    public void RestorePurchases() //Восстановление покупок (только для Apple). У гугл это автоматический процесс.
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer) //если запущенно на эпл устройстве
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;


        var subscriptionProduct = m_StoreController.products.WithID(vip);

        try
        {
            var isSubscribed = IsSubscribedTo(subscriptionProduct);
            gameManager.isSubscribe = isSubscribed; 

        }
        catch (StoreSubscriptionInfoNotSupportedException)
        {

        }
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }



}