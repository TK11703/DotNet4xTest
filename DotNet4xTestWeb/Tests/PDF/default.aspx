<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DotNet4xTestWeb.Tests.PDF._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    <%=this.SiteTitle %> - PDF
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <li class="breadcrumb-item"><a href="/default.aspx">Home</a></li>
    <li class="breadcrumb-item active" aria-current="page">PDF Testing Options</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">This section will assist engineers and developers in testing a new site space. It will allow for the testing of PDF or XDP files in a few scenarios. It will assist in confirming or identifying configuration issues.</p>
    <div class="row row-cols-1 row-cols-md-3 mt-3 mb-3 text-center">
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">ASPX Page (PDF)</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>Select this option to view a PDF file with a basic ASPX page. The PDF will be loaded in the page load and sent to the user.</li>
                    </ul>
                    <a href="/Tests/PDF/DisplayPDF.aspx" target="_blank" class="w-100 btn btn-lg btn-primary">Test</a>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">PDF Handler</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>Select this option to view a PDF file via an Http Handler. A specially crafted URL is used to handle this PDf request.</li>
                    </ul>
                    <a href="/PdfViewer" target="_blank" class="w-100 btn btn-lg btn-primary">Test</a>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">ASPX Page (XDP)</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>Select this option to view a XDP file with a basic ASPX page. The XDP will be loaded in the page load and sent to the user.</li>
                    </ul>
                    <a href="/Tests/PDF/DisplayXDP.aspx" target="_blank" class="w-100 btn btn-lg btn-primary">Test</a>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">XDP Handler</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>Select this option to view an XDP file (which internally references a PDF for inclusion into its content) via an Http Handler. A specially crafted URL is used to handle this XDP request.</li>
                    </ul>
                    <a href="/XdpViewer" target="_blank" class="w-100 btn btn-lg btn-primary">Test</a>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div>
        <a class="btn btn-secondary d-inline-flex align-items-center" href="/default.aspx" title="Go back to the available tests.">
            <svg height="16" width="16" fill="currentColor" class="me-1">
                <use href="#arrow-left-circle"></use>
            </svg>
            <span>Back to Testing Options</span>
        </a>
    </div>
</asp:Content>
