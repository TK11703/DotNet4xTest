<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DotNet4xTestWeb.Tests.Graph._default" %>

<%@ Register Src="~/authenticationDisplay.ascx" TagPrefix="uc1" TagName="authenticationDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    <%=this.SiteTitle %> - Graph
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <li class="breadcrumb-item"><a href="/default.aspx">Home</a></li>
    <li class="breadcrumb-item active" aria-current="page">Graph Testing Options</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">This section will assist engineers and developers in testing a new site space. It should test the ability to communicate with the Microsoft Graph API and verify scope permissions. It will assist in confirming or identifying configuration issues.</p>
    <div class="row row-cols-1 row-cols-md-3 mt-3 mb-3 text-center">
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">Current User</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>For the current user, the system will request various user details from the Graph and display the values.</li>
                    </ul>
                    <a href="/Tests/Graph/CurrentUserDetail.aspx" class="w-100 btn btn-lg btn-primary">Get started</a>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">Entra Users</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>For the current user, the system will request a list of users within this Entra tenant.</li>
                    </ul>
                    <a href="/Tests/Graph/EntraUsers.aspx" class="w-100 btn btn-lg btn-primary">Get started</a>
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
    <uc1:authenticationDisplay runat="server" ID="authenticationDisplay" />
</asp:Content>
