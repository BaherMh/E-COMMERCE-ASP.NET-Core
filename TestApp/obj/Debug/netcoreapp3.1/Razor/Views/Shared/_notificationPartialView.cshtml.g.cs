#pragma checksum "C:\Users\ALI\Desktop\Worked\Third\TestApp\Views\Shared\_notificationPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64d389892a8aef605d2fa07d8732f578f75303df"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__notificationPartialView), @"mvc.1.0.view", @"/Views/Shared/_notificationPartialView.cshtml")]
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
#line 1 "C:\Users\ALI\Desktop\Worked\Third\TestApp\Views\_ViewImports.cshtml"
using TestApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ALI\Desktop\Worked\Third\TestApp\Views\_ViewImports.cshtml"
using TestApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64d389892a8aef605d2fa07d8732f578f75303df", @"/Views/Shared/_notificationPartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e25db771d30abaf29d3599c25e6deb12289bab6e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__notificationPartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<script>\r\n    var msg = ");
#nullable restore
#line 2 "C:\Users\ALI\Desktop\Worked\Third\TestApp\Views\Shared\_notificationPartialView.cshtml"
          Write(TempData["Message"]!=null? Html.Raw(TempData["Message"]) : Html.Raw("undefined"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
    //msg.provider = ""sweetAlert"";
    console.log(msg)
    if (msg) {
        if (msg.provider == ""sweetAlert"") {
            swal.fire({
                title: msg.title,
                text: msg.message,
                icon: msg.icon
            });
        } else {
            toastr[msg.type](msg.message, msg.title);
        }
    }
</script>");
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
