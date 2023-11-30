using LB5.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;


namespace LB5.Controllers;

public class CheckOutController : Controller
{
    public IActionResult Index()
    {
        List<ProductEntity> productList = new List<ProductEntity>();
        productList = new List<ProductEntity>
        {
            new ProductEntity
            {
                Product = "Headphones",
                Rate = 200,
                Quantity = 5,
                ImagePath = "img/Image1.jpg"
            },
            new ProductEntity
            {
                Product = "Phone",
                Rate = 600,
                Quantity = 8,
                ImagePath = "img/Image2.jpg"
            }
        };
        return View(productList);
    }

    public IActionResult OrderConfirmation()
    {
        var service = new SessionService();
        Session session = service.Get(TempData["Session"].ToString());
        if (session.PaymentStatus == "paid")
        {
            var ytansaction = session.PaymentIntentId.ToString();
            return View("Success");
        }

        return View("Cancel");
    }

    public IActionResult Success()
    {
        return View();
    }

    public IActionResult Cancel()
    {
        return View();
    }

    public IActionResult CheckOut()
    {
        List<ProductEntity> productList = new List<ProductEntity>();
        productList = new List<ProductEntity>
        {
            new ProductEntity
            {
                Product = "Headphones",
                Rate = 200,
                Quantity = 5,
                ImagePath = "img/Image1.jpg"
            },
            new ProductEntity
            {
                Product = "Phone",
                Rate = 600,
                Quantity = 8,
                ImagePath = "img/Image2.jpg"
            }
        };
        var domain = "https://localhost:7228/";
        var options = new SessionCreateOptions
        {
            SuccessUrl = domain + $"CheckOut/OrderConfirmation",
            CancelUrl = domain + $"CheckOut/Cancel",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            CustomerEmail = "digital@yahoo.com"
        };
        foreach (var item in productList)
        {
            var sessionListItems = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Rate * item.Quantity),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.ToString(),
                    }
                },
                Quantity = item.Quantity
            };
            options.LineItems.Add(sessionListItems);
        }

        var service = new SessionService();
        Session session = service.Create(options);
        TempData["Session"] = session.Id;
        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}