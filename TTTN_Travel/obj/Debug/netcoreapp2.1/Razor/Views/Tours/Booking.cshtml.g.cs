#pragma checksum "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc2e79c27f6b84aeb540e0eaa6a7e88ddb06058e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tours_Booking), @"mvc.1.0.view", @"/Views/Tours/Booking.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Tours/Booking.cshtml", typeof(AspNetCore.Views_Tours_Booking))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "F:\TTTN_Travel\TTTN_Travel\Views\_ViewImports.cshtml"
using TTTN_Travel;

#line default
#line hidden
#line 2 "F:\TTTN_Travel\TTTN_Travel\Views\_ViewImports.cshtml"
using TTTN_Travel.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc2e79c27f6b84aeb540e0eaa6a7e88ddb06058e", @"/Views/Tours/Booking.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bfeaa89f9c33738f47d5b4e675891302441d09b", @"/Views/_ViewImports.cshtml")]
    public class Views_Tours_Booking : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
  
    ViewData["Title"] = "Booking";
    Layout = "~/Views/Shared/_Layout_Page.cshtml";

#line default
#line hidden
            BeginContext(149, 765, true);
            WriteLiteral(@"<div class=""box-mobile-tab"">
    <div class=""baogia"">
        <div class=""h3"">Bảng báo giá dịch vụ</div>
        <p>Lựa chọn dịch vụ bạn muốn sử dụng.</p>
    </div>
    <div class=""box-one"">

        <div class=""col-md-6 dv-one"">
            Loại dịch vụ
        </div>
        <div class=""col-md-2 dv-one"">
            Số lượng
        </div>
        <div class=""col-md-2 dv-one"">
            Chi phí
        </div>
        <div class=""col-md-2 dv-one"">
            Tổng chi phí
        </div>
    </div>

    <div class=""box-two"">
        <b>Tour mặc định</b><br>
        <div class=""col-md-6 dv-two"">
            <p>
                <input class=""abc"" id=""myCheckbox"" name=""name"" type=""checkbox"" value=""3690000"" checked="""" disabled=""""> ");
            EndContext();
            BeginContext(915, 12, false);
#line 31 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
                                                                                                                  Write(ViewBag.TenT);

#line default
#line hidden
            EndContext();
            BeginContext(927, 107, true);
            WriteLiteral("\r\n            </p>\r\n        </div>\r\n        <div class=\"col-md-2 dv-two\">\r\n            <p class=\"dv-text\"> ");
            EndContext();
            BeginContext(1035, 12, false);
#line 35 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
                           Write(ViewBag.SoNg);

#line default
#line hidden
            EndContext();
            BeginContext(1047, 129, true);
            WriteLiteral(" Người lớn</p>\r\n        </div>\r\n        <div class=\"col-md-2 dv-two two-1-mb\">\r\n            <p class=\"dv-text\">\r\n                ");
            EndContext();
            BeginContext(1177, 12, false);
#line 39 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
           Write(ViewBag.GiaT);

#line default
#line hidden
            EndContext();
            BeginContext(1189, 122, true);
            WriteLiteral(" đ\r\n            </p>\r\n        </div>\r\n        <div class=\"col-md-2 dv-two\">\r\n            <p class=\"dv-text active-price\"> ");
            EndContext();
            BeginContext(1312, 13, false);
#line 43 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
                                        Write(ViewBag.TongT);

#line default
#line hidden
            EndContext();
            BeginContext(1325, 203, true);
            WriteLiteral(" đ</p>\r\n        </div>\r\n        <div class=\"col-md-6 dv-two\">\r\n            <p>\r\n                \r\n            </p>\r\n        </div>\r\n        <div class=\"col-md-2 dv-two\">\r\n            <p class=\"dv-text\"> ");
            EndContext();
            BeginContext(1529, 13, false);
#line 51 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
                           Write(ViewBag.TreEM);

#line default
#line hidden
            EndContext();
            BeginContext(1542, 126, true);
            WriteLiteral(" Trẻ em</p>\r\n        </div>\r\n        <div class=\"col-md-2 dv-two two-1-mb\">\r\n            <p class=\"dv-text\">\r\n                ");
            EndContext();
            BeginContext(1669, 13, false);
#line 55 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
           Write(ViewBag.GiaTr);

#line default
#line hidden
            EndContext();
            BeginContext(1682, 122, true);
            WriteLiteral(" đ\r\n            </p>\r\n        </div>\r\n        <div class=\"col-md-2 dv-two\">\r\n            <p class=\"dv-text active-price\"> ");
            EndContext();
            BeginContext(1805, 14, false);
#line 59 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
                                        Write(ViewBag.TongTr);

#line default
#line hidden
            EndContext();
            BeginContext(1819, 1515, true);
            WriteLiteral(@" đ</p>
        </div>
    </div>
    <!--

    <div class=""box-two"">
        <b>Dịch vụ tùy chọn</b><br>
        <div class=""col-md-6 dv-two"">
            <p>
                <input class=""checkbox"" id=""1"" type=""checkbox"" data-price=""1200000""> Phụ phí phòng đơn
            </p>
        </div>
        <div class=""col-md-2 dv-two"">
            <p class=""dv-text"">   người </p>
        </div>
        <div class=""col-md-2 dv-two two-2-mb"">
            <p class=""dv-text""> 1.200.000đ </p>
        </div>
        <div class=""col-md-2 dv-two"">
            <p id=""tong-tien1"" class=""dv-text"">  1.200.000đ </p>
        </div>
    </div>

     <div class=""box-two"" style=""display: none"">
        <b>Dịch vụ thêm</b><br>
        <div class=""col-md-6 dv-two"">
            <p>
                <input class=""checkbox dv-them"" id=""4"" type=""checkbox"" data-price=""500000""> Luxury Limousine Bus Round Trip HN-HL-HN
            </p>

        </div>
        <div class=""col-md-2 dv-two"">
            <div clas");
            WriteLiteral(@"s=""form-group"">
                <input class=""checkbox"" id=""number"" type=""number"" value=""1"" readonly="""">
            </div>
        </div>
        <div class=""col-md-2 dv-two"">
            <p class=""dv-text""> <span class=""price3"">0</span> </p>

        </div>
        <div class=""col-md-2 dv-two"">
            <p id=""them-moi1"" class=""dv-text""> 500.000đ </p>

        </div>
    </div>
    -->

    <div class=""total-price"">
        <b>Tổng chi phí</b>: <span class=""price2"">");
            EndContext();
            BeginContext(3335, 15, false);
#line 107 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
                                             Write(ViewBag.TongAll);

#line default
#line hidden
            EndContext();
            BeginContext(3350, 224, true);
            WriteLiteral(" đ</span>\r\n    </div>\r\n\r\n</div>\r\n<span style=\"float:right;display:inline;\">\r\n    <input style=\"background-color:green;\" class=\"btn btn-info btn-block\" data-toggle=\"modal\" data-target=\"#myModal\" type=\"button\" value=\"Xác Nhận\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 3574, "\"", 3715, 4);
#line 112 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
WriteAttributeValue("", 3584, " window.location.href='" + @Url.Action("Index", "Home") + "'", 3584, 65, false);

#line default
#line hidden
            WriteAttributeValue("", 3649, ";", 3649, 1, true);
#line 112 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
WriteAttributeValue("", 3650, "window.alert('Đặt tour thành công, Cảm ơn quý khách hàng!')", 3650, 64, false);

#line default
#line hidden
            WriteAttributeValue("", 3714, ";", 3714, 1, true);
            EndWriteAttribute();
            BeginContext(3716, 144, true);
            WriteLiteral(" />\r\n    <input style=\"background-color:red\" class=\"btn btn-info btn-block\" data-toggle=\"modal\" data-target=\"#myModal\" type=\"button\" value=\"Hủy\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 3860, "\"", 4030, 4);
#line 113 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
WriteAttributeValue("", 3870, "window.location.href='" + @Url.Action("CancelBook", "Tours") + "'", 3870, 70, false);

#line default
#line hidden
            WriteAttributeValue("", 3940, ";", 3940, 1, true);
#line 113 "F:\TTTN_Travel\TTTN_Travel\Views\Tours\Booking.cshtml"
WriteAttributeValue(" ", 3941, "window.alert('Hủy tour thành công, mời quý khách tiếp tục dịch vụ của chúng tôi!')", 3942, 87, false);

#line default
#line hidden
            WriteAttributeValue("", 4029, ";", 4029, 1, true);
            EndWriteAttribute();
            BeginContext(4031, 16, true);
            WriteLiteral(" />\r\n</span>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
