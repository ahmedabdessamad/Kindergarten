using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CloudKids.Domain.Entities;
using CloudKids.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using CloudKids.Service;
using System.IO;
using System.Net;
using CloudKids.Data;
using System.Data.Entity;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Configuration;
using PayPal.Api;

namespace CloudKids.Web.Controllers
{

    public class ProductController : Controller
    {
        CloudKidsContext db = new CloudKidsContext();
        IProductService MyProductService;
        IProductCategoryService MyProductCategoryService;
        ICartLineService MyCartLineService;
        ICartService MyCartService;
        ApplicationUser user;


        // upload on s3 variables
        private const string keyName = "updatedtestfile.txt";
        private const string filePath = null;
        // Specify your bucket region (an example region is shown).  
        private static readonly string bucketName = ConfigurationManager.AppSettings["BucketName"];
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest1;
        private static readonly string accesskey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string secretkey = ConfigurationManager.AppSettings["AWSSecretKey"];



        public ProductController()
        {
            MyProductService = new ProductService();
            MyProductCategoryService = new ProductCategoryService();
            MyCartLineService = new CartLineService();
            MyCartService = new CartService();
            user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                 .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId<int>());



        }



        // GET: Product
        public ActionResult Index(string search)
        {
            if (search == null)
                return View(MyProductService.GetAll());
            else
                return View(MyProductService.GetAll().Where(x => x.Name.StartsWith(search) || search == null));
        }

        // GET: MyProducts
        public ActionResult MyProducts()
        {
            var products = MyProductService.GetMyProduct(user.Id);
            return View(products);
        }







        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Price,DateAddition,StockStatus,Description,CategoryId,DirectorId,Quantity,imagePath,imagePath1,imagePath2,imagePath3,imageFile,imageFile1,imageFile2,imageFile3")] Product product)
        {
            //ApplicationUser current_user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                //product.Name = "haha";
                //product.Price = 55;
                product.DateAddition = DateTime.UtcNow;


                product.StockStatus = 0;
                product.Description = "ggogogo";
                //product.CategoryId = 1;
                product.DirectorId = user.Id;
                //product.Quantity = 55;


                string filename = Path.GetFileNameWithoutExtension(product.imageFile.FileName);
                string extension = Path.GetExtension(product.imageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string s3filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                product.imagePath = "/UploadedProductImages/" + filename;
                filename = Path.Combine(Server.MapPath("/UploadedProductImages/"), filename);
                product.imageFile.SaveAs(filename);

                string filename1 = Path.GetFileNameWithoutExtension(product.imageFile1.FileName);
                string extension1 = Path.GetExtension(product.imageFile1.FileName);
                filename1 = filename1 + DateTime.Now.ToString("yymmssfff") + extension1;
                product.imagePath1 = "/UploadedProductImages/" + filename1;
                filename1 = Path.Combine(Server.MapPath("/UploadedProductImages/"), filename1);
                product.imageFile1.SaveAs(filename1);

                string filename2 = Path.GetFileNameWithoutExtension(product.imageFile2.FileName);
                string extension2 = Path.GetExtension(product.imageFile2.FileName);
                filename2 = filename2 + DateTime.Now.ToString("yymmssfff") + extension2;
                product.imagePath2 = "/UploadedProductImages/" + filename2;
                filename2 = Path.Combine(Server.MapPath("/UploadedProductImages/"), filename2);
                product.imageFile2.SaveAs(filename2);

                //string filename3 = Path.GetFileNameWithoutExtension(product.imageFile3.FileName);
                //string extension3 = Path.GetExtension(product.imageFile3.FileName);
                //filename3 = filename3 + DateTime.Now.ToString("yymmssfff") + extension3;
                //product.imagePath = "/UploadedProductImages/" + filename3;
                //filename3 = Path.Combine(Server.MapPath("/UploadedProductImages/"), filename3);
                //product.imageFile3.SaveAs(filename3);



                // upload on s3

                var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);

                var fileTransferUtility = new TransferUtility(s3Client);
                try
                {

                    var filePath = filename;
                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        FilePath = filePath,
                        StorageClass = S3StorageClass.StandardInfrequentAccess,
                        PartSize = 6291456, // 6 MB.  
                        Key = s3filename,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                    fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
                    fileTransferUtility.Upload(fileTransferUtilityRequest);
                    fileTransferUtility.Dispose();

                    ViewBag.Message = "File Uploaded Successfully!!";
                }

                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        ViewBag.Message = "Check the provided AWS Credentials.";
                    }
                    else
                    {
                        ViewBag.Message = "Error occurred: " + amazonS3Exception.Message;
                    }
                }




                //end upload on s3





                if (ModelState.IsValid)
                {
                    MyProductService.Add(product);
                    MyProductService.Commit();
                }
                return RedirectToAction("Index");
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = MyProductService.GetById(id);
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult EditCreate([Bind(Include = "Name,Price,DateAddition,StockStatus,Description,CategoryId,DirectorId,Quantity,imagePath,imagePath1,imagePath2,imagePath3,imageFile,imageFile1,imageFile2,imageFile3")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View(product);
        }



        //// POST: Product/Delete/5
        //[HttpPost, ActionName("Delete")]
        //public ActionResult Delete(int id)
        //{
        //    Product product = MyProductService.GetById(id);
        //    MyProductService.Delete(product);
        //    MyProductService.Commit();
        //    return RedirectToAction("MyProducts");
        //}



        // GET: Products/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        public ActionResult Delete(int id)
        {

            Product product = MyProductService.GetById(id);
            MyProductService.Delete(product);
            MyProductService.Commit();
            return RedirectToAction("MyProducts");

        }



        // GET: Products/Details/5
        public ActionResult Details(int id)
        {

            Product product = MyProductService.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        //********************************** CART ***********************************************//
        // GET: Cart
        public ActionResult DetailsCart()
        {
           
            Cart cart = MyCartService.GetCartByParentId(user.Id);
            var cartLines = MyCartLineService.GetCartLinesByCartId(cart.Id);
            var objList = new List<ComplexProductObject>();

            foreach (var cartLine in cartLines)
            {
                var product = MyProductService.GetById(cartLine.ProductId);
                ComplexProductObject obj = new ComplexProductObject();
                obj.ProductName = product.Name;
                obj.ProductPrice = product.Price;
                obj.imagePath = product.imagePath;
                obj.ChoosedQuantity = cartLine.Quantity;
                obj.CartDateAdded = cartLine.DateAdded;
                obj.TotalCartLinePrice = cartLine.Quantity * product.Price;
                obj.availableQuantity = product.Quantity;
                objList.Add(obj);
            }



            return View(objList);
        }

        [HttpPost]
        public ActionResult AddToCart()
        {
            int choosedQuantity = Int16.Parse(Request["quant"].ToString());
            int productId = Int16.Parse(Request["productId"].ToString());
            try
            {
               
                Cart cart = MyCartService.GetCartByParentId(user.Id);
                Product product = MyProductService.GetById(productId);
                if (cart == null)
                {
                    Cart newCart = new Cart();
                    newCart.ParentId = user.Id;
                    newCart.Price = product.Price * choosedQuantity;
                    newCart.Quantity = 0;
                    MyCartService.Add(newCart);
                    MyCartService.Commit();


                    CartLine cartLine = new CartLine();

                    cartLine.DateAdded = DateTime.UtcNow;
                    cartLine.CartId = newCart.Id;
                    cartLine.ProductId = productId;
                    cartLine.Quantity = choosedQuantity;
                    cartLine.Price = product.Price;
                    MyCartLineService.Add(cartLine);
                    MyCartLineService.Commit();


                }
                else
                {

                    CartLine cartLine = new CartLine();
                    cartLine.DateAdded = DateTime.UtcNow;
                    cartLine.CartId = cart.Id;
                    cartLine.ProductId = productId;
                    cartLine.Quantity = choosedQuantity;
                    cartLine.Price += product.Price;
                    MyCartLineService.Add(cartLine);
                    MyCartLineService.Commit();
                }
            }

            catch (IOException e)
            {
                throw e;
            }

            return RedirectToAction("DetailsCart");



        }


        public ActionResult ConfirmPurchase()
        {
            try
            {
                Cart myCart = MyCartService.GetCartByParentId(user.Id);
                var cartLines = MyCartLineService.GetCartLinesByCartId(myCart.Id);
                foreach (CartLine cartLine in cartLines)
                {
                    MyCartLineService.Delete(cartLine);
                    MyCartLineService.Commit();
                }

                MyCartService.Delete(myCart);
                MyCartService.Commit();


            }

            catch (IOException e)
            {
                throw e;
            }


            return RedirectToAction("Index");
        }



        // Work with Paypal Payment
        private Payment payment;

        //Create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            Cart myCart = MyCartService.GetCartByParentId(user.Id);
            var cartLines = MyCartLineService.GetCartLinesByCartId(myCart.Id);

            var listItems = new ItemList()
            {
                items = new List<Item>()
            };

            foreach (var cart in cartLines)
            {
                listItems.items.Add(new Item()
                {
                    name = cart.MyProduct.Name,
                    currency = "USD",
                    price = cart.MyProduct.Price.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku"
                });

            }
            var payer = new Payer() { payment_method = "paypal" };
            // Do the configuration RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = cartLines.Sum(x => x.Quantity * x.MyProduct.Price).ToString()

            };

            //Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),
                details = details

                // tax + shipping + subtotal
            };

            //Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = " Testring transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            return payment.Create(apiContext);
        }

        //Create Execute Payment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        // Create PaymentWithPaypal method
        public ActionResult PaymentWithPaypal()
        {
            //Gettings context from the paypal bases on clientId and clientSecret for payment
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Creating a payment
                    string baseURL = Request.Url.Scheme + "://" + Request.Url.Authority + "/Product/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURL + "guid=" + guid);
                    // Get links returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // this one will be executed when we have received all the payment params for previous call
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }


            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                return View("Failure");


            }
            return View("Success");
        }



    }
}
