#pragma checksum "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b96bea92acb3f1f23ff4d1893242d0835f76c61"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Dattours_Delete), @"mvc.1.0.view", @"/Areas/Admin/Views/Dattours/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/Dattours/Delete.cshtml", typeof(AspNetCore.Areas_Admin_Views_Dattours_Delete))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b96bea92acb3f1f23ff4d1893242d0835f76c61", @"/Areas/Admin/Views/Dattours/Delete.cshtml")]
    public class Areas_Admin_Views_Dattours_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TTTN_Travel.Models.Dattour>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
  
    ViewData["Title"] = "Delete";
    Layout = "~/Areas/Admin/Views/Shared/_Layout_Admin.cshtml";

#line default
#line hidden
            BeginContext(196, 168, true);
            WriteLiteral("\r\n<h2>Delete</h2>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>Dattour</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(365, 41, false);
#line 16 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Hoten));

#line default
#line hidden
            EndContext();
            BeginContext(406, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(450, 37, false);
#line 19 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Hoten));

#line default
#line hidden
            EndContext();
            BeginContext(487, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(531, 43, false);
#line 22 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Tentuor));

#line default
#line hidden
            EndContext();
            BeginContext(574, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(618, 39, false);
#line 25 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Tentuor));

#line default
#line hidden
            EndContext();
            BeginContext(657, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(701, 39, false);
#line 28 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Sdt));

#line default
#line hidden
            EndContext();
            BeginContext(740, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(784, 35, false);
#line 31 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Sdt));

#line default
#line hidden
            EndContext();
            BeginContext(819, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(863, 38, false);
#line 34 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Dc));

#line default
#line hidden
            EndContext();
            BeginContext(901, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(945, 34, false);
#line 37 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Dc));

#line default
#line hidden
            EndContext();
            BeginContext(979, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1023, 41, false);
#line 40 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1064, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1108, 37, false);
#line 43 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1145, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1189, 40, false);
#line 46 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Date));

#line default
#line hidden
            EndContext();
            BeginContext(1229, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1273, 36, false);
#line 49 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Date));

#line default
#line hidden
            EndContext();
            BeginContext(1309, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1353, 43, false);
#line 52 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Songuoi));

#line default
#line hidden
            EndContext();
            BeginContext(1396, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1440, 39, false);
#line 55 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Songuoi));

#line default
#line hidden
            EndContext();
            BeginContext(1479, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1523, 41, false);
#line 58 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Treem));

#line default
#line hidden
            EndContext();
            BeginContext(1564, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1608, 37, false);
#line 61 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Treem));

#line default
#line hidden
            EndContext();
            BeginContext(1645, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1689, 42, false);
#line 64 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Ghichu));

#line default
#line hidden
            EndContext();
            BeginContext(1731, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1775, 38, false);
#line 67 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Ghichu));

#line default
#line hidden
            EndContext();
            BeginContext(1813, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1857, 45, false);
#line 70 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Thanhtien));

#line default
#line hidden
            EndContext();
            BeginContext(1902, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1946, 41, false);
#line 73 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Thanhtien));

#line default
#line hidden
            EndContext();
            BeginContext(1987, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2031, 45, false);
#line 76 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Trangthai));

#line default
#line hidden
            EndContext();
            BeginContext(2076, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2120, 41, false);
#line 79 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Trangthai));

#line default
#line hidden
            EndContext();
            BeginContext(2161, 34, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n\r\n    ");
            EndContext();
            BeginContext(2195, 228, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "041b7b9c57904a05a31c1ecbe5687f58", async() => {
                BeginContext(2221, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(2231, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "16fc7e1697a245fcab6ac80535bb1185", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 84 "F:\TTTN_Travel\TTTN_Travel\Areas\Admin\Views\Dattours\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2267, 105, true);
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger btn-sm btn-block mt-2\" /> |\r\n        ");
                EndContext();
                BeginContext(2372, 38, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3af5b63cb58f493499b8f21a341e016a", async() => {
                    BeginContext(2394, 12, true);
                    WriteLiteral("Back to List");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2410, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2423, 10, true);
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TTTN_Travel.Models.Dattour> Html { get; private set; }
    }
}
#pragma warning restore 1591
