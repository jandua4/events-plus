#pragma checksum "C:\Users\Aman\Desktop\Folders\Productivity and Work\University\Database & Web Applications\workspace\EventsPlus\Views\Schedule\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7361f3b057e9739b7fcbc39825eafbf0d7562964"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Schedule_Index), @"mvc.1.0.view", @"/Views/Schedule/Index.cshtml")]
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
#line 1 "C:\Users\Aman\Desktop\Folders\Productivity and Work\University\Database & Web Applications\workspace\EventsPlus\Views\_ViewImports.cshtml"
using EventsPlus;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Aman\Desktop\Folders\Productivity and Work\University\Database & Web Applications\workspace\EventsPlus\Views\_ViewImports.cshtml"
using EventsPlus.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7361f3b057e9739b7fcbc39825eafbf0d7562964", @"/Views/Schedule/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e60b99fa1b683a9f641e1377eb06f2cdddc4b732", @"/Views/_ViewImports.cshtml")]
    public class Views_Schedule_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Event>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Users\Aman\Desktop\Folders\Productivity and Work\University\Database & Web Applications\workspace\EventsPlus\Views\Schedule\Index.cshtml"
  
    ViewData["Title"] = "Events Schedule";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center main-heading"">
    <h1>Event Schedule</h1>
</div>

<p>Hello there! Can't see the whole table? Try scrolling the table side-to-side!</p>

<div class=""table-container"">
    <table class=""table-borderless table-striped"">
        <tr>
            <th>Event Name</th>
            <th>Event Type</th>
            <th>Description</th>
            <th>Date/Time</th>
            <th>Duration</th>
        </tr>
        <tr>
            <td>Name</td>
            <td>EventTypeID</td>
            <td>Description </td>
            <td>DateTime</td>
            <td>Duration</td>
        </tr>
    </table>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Event> Html { get; private set; }
    }
}
#pragma warning restore 1591