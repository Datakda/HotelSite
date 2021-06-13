using HotelSite.Models;
using HotelSite.Service;
using HotelSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelSite.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                 
                FileInfo fileInf = new FileInfo("privateKey.xml");
                TimeSpan time = DateTime.Now - fileInf.CreationTime;
                if (time.Days >= 1) 
                {
                    RSAservice rsaSend = new RSAservice();

                }






                model.UserName = User.Identity.Name;
                string json = JsonSerializer.Serialize<OrderViewModel>(model);


               
                 RSAParameters _privateKey;
                 RSAParameters _publicKey;
                using (StreamReader _public = new StreamReader("publicKey.xml"))
                {
                    _publicKey = ParsXmlToRSAP.GetPubKeyFromXmlString(_public.ReadToEnd());
                }


                using (StreamReader _private = new StreamReader("privateKey.xml"))
                {
                    _privateKey = ParsXmlToRSAP.GetPrivKeyFromXmlString(_private.ReadToEnd());
                }

                

                RSAservice RSA = new RSAservice(_publicKey, _privateKey);
                Crypt cr = new Crypt();
                cr.code = RSA.Encrypt(json, _publicKey);
                string crypto = JsonSerializer.Serialize<Crypt>(cr);

                var getResult =  await SendOrder.SendOrderServer(crypto);

                ViewBag.Message = RSA.Decrypt(getResult);
                return View();
            }
            return View(model);
        }

        public IActionResult List()
        {
            return View();
        }

    }
}
