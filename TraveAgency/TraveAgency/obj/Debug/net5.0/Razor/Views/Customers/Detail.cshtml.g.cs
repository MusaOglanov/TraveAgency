#pragma checksum "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c2ee2f0fc2c9f95ddbd9558cbaa2a0b532855e8e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customers_Detail), @"mvc.1.0.view", @"/Views/Customers/Detail.cshtml")]
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
#nullable restore
#line 1 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\_ViewImports.cshtml"
using TraveAgency;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\_ViewImports.cshtml"
using TraveAgency.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\_ViewImports.cshtml"
using TraveAgency.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c2ee2f0fc2c9f95ddbd9558cbaa2a0b532855e8e", @"/Views/Customers/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8afea7c6044ba1376380f3ea0199a3b11064089", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Customers_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Customer>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"card-header d-flex justify-content-between align-items-center\">\r\n    <h5 class=\"mb-0\">Customer Detail</h5>\r\n</div>\r\n\r\n<div class=\"form-row\">\r\n    <div class=\"form-group col-md-4\">\r\n        <h6> Name</h6>\r\n        <h4>");
#nullable restore
#line 9 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
       Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"form-group col-md-4\">\r\n        <h6>Surname</h6>\r\n        <h4>");
#nullable restore
#line 13 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
       Write(Model.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"form-group col-md-4\">\r\n        <h6>Registration Date</h6>\r\n        <h5>");
#nullable restore
#line 17 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
       Write(Model.RegistrationDate.ToString("dd/MMM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    </div>\r\n</div>\r\n<div class=\"form-row \">\r\n    <div class=\"form-group col-md-4\">\r\n        <h6>Mobile</h6>\r\n        <h4>");
#nullable restore
#line 23 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
       Write(Model.Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"form-group col-md-4\">\r\n        <h6> Email </h6>\r\n        <h5>");
#nullable restore
#line 27 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
       Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    </div>\r\n</div>\r\n<div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n        <h6> City</h6>\r\n        <h4>");
#nullable restore
#line 33 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
       Write(Model.City);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h4>\r\n    </div>\r\n    <div class=\"form-row col-md-3\">\r\n        <div class=\"form-group col-md-3\" style=\"text-align:center\">\r\n            <h6 class=\"mb-4 text-md  \">Country</h6>\r\n            <h5>");
#nullable restore
#line 38 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
           Write(Model.Country);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        </div>\r\n    </div>\r\n    <div class=\"form-row col-md-3\">\r\n        <div class=\"form-group col-md-3\" style=\"text-align:center\">\r\n            <h6 class=\"mb-4 text-md  \">Adress</h6>\r\n            <h5>");
#nullable restore
#line 44 "C:\Users\musac\Desktop\TraveAgency\TraveAgency\TraveAgency\Views\Customers\Detail.cshtml"
           Write(Model.Adress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c2ee2f0fc2c9f95ddbd9558cbaa2a0b532855e8e7258", async() => {
                WriteLiteral("Go Back");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Customer> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591