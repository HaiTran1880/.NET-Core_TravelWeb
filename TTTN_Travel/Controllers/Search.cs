using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models;

namespace TTTN_Travel.Controllers
{
    public class Search : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public Search(TourReContext context)
        {
            this.tourReContext = context;
        }
        public double convertToNum(string txt)
        {

            string gia = "";
            try
            {

                string[] words = txt.Split('.');

                foreach (var word in words)
                {
                    gia += word;
                }
                return Convert.ToDouble(gia);
            }
            catch(Exception e)
            {
                Console.Write(e);
            }
            return Convert.ToDouble(gia);
        }
        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("name");
            try
            {
                ViewBag.User = HttpContext.Session.GetString("name");
                string input = Request.Form["s"];
                string radio = Request.Form["radio"];
                string diemden = Request.Form["diemden"];
                string giatour = Request.Form["giatour"];  
                if (input == "")
                {
                    if (radio == "11")
                    {
                        if(diemden!="")
                        {
                            switch (diemden)
                            {
                                case "0":
                                    switch (giatour)
                                    {
                                        case "1":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 500000 && convertToNum(x.Gia) < 5000000 && x.Tentuor.Contains("Ninh Bình") && x.Lichtrinh.Contains("Ninh Bình"));


                                            break;
                                        case "2":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 5000000 && convertToNum(x.Gia) < 10000000 && x.Tentuor.Contains("Ninh Bình") && x.Lichtrinh.Contains("Ninh Bình"));


                                            break;
                                        case "3":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 10000000 && convertToNum(x.Gia) < 20000000 && x.Tentuor.Contains("Ninh Bình") && x.Lichtrinh.Contains("Ninh Bình"));


                                            break;
                                        case "4":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 20000000 && convertToNum(x.Gia) < 40000000 && x.Tentuor.Contains("Ninh Bình") && x.Lichtrinh.Contains("Ninh Bình"));

                                            break;
                                        default:
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains("Ninh Bình") || x.Mota.Contains("Ninh Bình") || x.Lichtrinh.Contains("Ninh Bình")).Take(3);


                                            break;
                                    }

                                    break;
                                case "1":
                                    switch (giatour)
                                    {
                                        case "1":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 500000 && convertToNum(x.Gia) < 5000000 && x.Tentuor.Contains("Sapa") && x.Lichtrinh.Contains("Sapa"));


                                            break;
                                        case "2":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 5000000 && convertToNum(x.Gia) < 10000000 && x.Tentuor.Contains("Sapa") && x.Lichtrinh.Contains("Sapa"));


                                            break;
                                        case "3":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 10000000 && convertToNum(x.Gia) < 20000000 && x.Tentuor.Contains("Sapa") && x.Lichtrinh.Contains("Sapa"));


                                            break;
                                        case "4":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 20000000 && convertToNum(x.Gia) < 40000000 && x.Tentuor.Contains("Sapa") && x.Lichtrinh.Contains("Sapa"));

                                            break;
                                        default:
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains("Sapa") || x.Mota.Contains("Sapa") || x.Lichtrinh.Contains("Sapa")).Take(3);


                                            break;
                                    }
                                    break;
                                case "2":
                                    switch (giatour)
                                    {
                                        case "1":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 500000 && convertToNum(x.Gia) < 5000000 && x.Tentuor.Contains("Hà Giang") && x.Lichtrinh.Contains("Hà Giang"));


                                            break;
                                        case "2":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 5000000 && convertToNum(x.Gia) < 10000000 && x.Tentuor.Contains("Hà Giang") && x.Lichtrinh.Contains("Hà Giang"));


                                            break;
                                        case "3":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 10000000 && convertToNum(x.Gia) < 20000000 && x.Tentuor.Contains("Hà Giang") && x.Lichtrinh.Contains("Hà Giang"));


                                            break;
                                        case "4":
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Trongnuoc == true && convertToNum(x.Gia) > 20000000 && convertToNum(x.Gia) < 40000000 && x.Tentuor.Contains("Hà Giang") && x.Lichtrinh.Contains("Hà Giang"));

                                            break;
                                        default:
                                            ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains("Hà Giang") || x.Mota.Contains("Hà Giang") || x.Lichtrinh.Contains("Hà Giang")).Take(3);


                                            break;
                                    }
                                    break;
                                default:
                                    //ViewBag.TourTN = this.tourReContext.Tour.Take(6);
                                    break;

                            }
                        }
                        else
                        {
                            ViewBag.TourTN = this.tourReContext.Tour.Take(6);  
                        }    
                    }
                    else if (radio == "22")
                    {

                       if(diemden!="")
                        {
                            switch (diemden)
                            {
                                case "3":
                                    switch (giatour)
                                    {
                                        case "1":
                                          
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Dubai") && convertToNum(x.Giakm) > 500000 && convertToNum(x.Giakm) < 5000000).Take(3);

                                            break;
                                        case "2":
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Dubai") && convertToNum(x.Giakm) > 5000000 && convertToNum(x.Giakm) < 10000000 ).Take(3);

                                            break;
                                        case "3":
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Dubai") && convertToNum(x.Giakm) > 10000000 && convertToNum(x.Giakm) < 20000000).Take(3);

                                            break;
                                        case "4":
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Dubai") && convertToNum(x.Giakm) > 20000000 && convertToNum(x.Giakm) < 40000000).Take(3);

                                            break;
                                        default:
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Dubai") || x.Lichtrinh.Contains("Dubai"));

                                            break;
                                    }
                                    break;
                                case "4":
                                    switch (giatour)
                                    {
                                        case "1":
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Phuket") && convertToNum(x.Giakm) > 500000 && convertToNum(x.Giakm) < 5000000 );

                                            break;
                                        case "2":
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Phuket") && convertToNum(x.Giakm) > 5000000 && convertToNum(x.Giakm) < 10000000);

                                            break;
                                        case "3":
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Phuket") && convertToNum(x.Giakm) > 10000000 && convertToNum(x.Giakm) < 20000000);

                                            break;
                                        case "4":
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Phuket") && convertToNum(x.Giakm) > 20000000 && convertToNum(x.Giakm) < 40000000);

                                            break;
                                        default:
                                            ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains("Phuket") || x.Lichtrinh.Contains("Phuket"));
                                            break;
                                    }
                                    break;
                                default:
                                    ViewBag.TourNN = this.tourReContext.Tout.Take(6);

                                    break;

                            }
                        }
                        else
                        {
                            ViewBag.TourNN = this.tourReContext.Tout.Take(6);
                        }    
                    }
                    else
                    {
                        ViewBag.TourTN = this.tourReContext.Tour.Take(3);
                        ViewBag.TourNN = this.tourReContext.Tout.Take(3);
                    }
                }
                else
                {

                   if(giatour!="0")
                    {
                        switch (giatour)
                        {
                            case "1":
                                ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains(input) && convertToNum(x.Gia) > 500000 && convertToNum(x.Gia) < 5000000).Take(3);
                                ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains(input) && convertToNum(x.Giakm)>500000 && convertToNum(x.Giakm) < 5000000).Take(3);
                                ViewBag.TinTuc = this.tourReContext.Tintuc.Where(x => x.Tentintuc.Contains(input) || x.Chitiet.Contains(input)).Take(3);
                                break;
                            case "2":
                                ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains(input)&& convertToNum(x.Gia) > 5000000 && convertToNum(x.Gia) < 10000000).Take(3);
                                ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains(input) && convertToNum(x.Giakm) > 5000000 && convertToNum(x.Giakm) < 10000000).Take(3);
                                ViewBag.TinTuc = this.tourReContext.Tintuc.Where(x => x.Tentintuc.Contains(input) || x.Chitiet.Contains(input)).Take(3);
                                break;
                            case "3":
                                ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains(input)&& convertToNum(x.Gia) > 10000000 && convertToNum(x.Gia) < 20000000).Take(3);
                                ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains(input) && convertToNum(x.Giakm) > 10000000 && convertToNum(x.Giakm) < 20000000).Take(3);
                                ViewBag.TinTuc = this.tourReContext.Tintuc.Where(x => x.Tentintuc.Contains(input) || x.Chitiet.Contains(input)).Take(3);
                                break;
                            case "4":
                                ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains(input)&& convertToNum(x.Gia) > 20000000 && convertToNum(x.Gia) < 40000000).Take(3);
                                ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains(input) && convertToNum(x.Giakm) > 20000000 && convertToNum(x.Giakm) < 40000000).Take(3);
                                ViewBag.TinTuc = this.tourReContext.Tintuc.Where(x => x.Tentintuc.Contains(input) || x.Chitiet.Contains(input)).Take(3);
                                break;
                            default:
                                /*ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains(input) || x.Mota.Contains(input)).Take(3);
                                ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains(input) || x.Chitiet.Contains(input)).Take(3);
                                ViewBag.TinTuc = this.tourReContext.Tintuc.Where(x => x.Tentintuc.Contains(input) || x.Chitiet.Contains(input)).Take(3);*/
                                break;
                        }

                    }
                    else
                    {
                        ViewBag.TourTN = this.tourReContext.Tour.Where(x => x.Tentuor.Contains(input) || x.Lichtrinh.Contains(input) || x.Mota.Contains(input)).Take(3);
                        ViewBag.TourNN = this.tourReContext.Tout.Where(x => x.Ten.Contains(input) || x.Lichtrinh.Contains(input) || x.Chitiet.Contains(input)).Take(3);
                        ViewBag.TinTuc = this.tourReContext.Tintuc.Where(x => x.Tentintuc.Contains(input) || x.Chitiet.Contains(input)).Take(3);
                    }

                }

            }
            catch (Exception e)
            {
                ViewBag.Eerr = e;
            }

            return View();
        }
        public void timtheogia(string gia)
        {
            if(gia!="")
            {
                
            }    
        }
    }
}
