<%@ Page Title="" Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DotNet4xTestWeb._default" %>

<%@ Register Src="~/authenticationDisplay.ascx" TagPrefix="uc1" TagName="authenticationDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <li class="breadcrumb-item active" aria-current="page">Home</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">This application will assist engineers and developers in testing a new site space. It should test the processing, interoperability, and connectivity of the site. It will assist in confirming or identifying configuration issues.</p>
    <div class="row row-cols-1 row-cols-md-3 mt-3 mb-3 text-center">
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">Database</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>DB Connectivity</li>
                        <li>Master & Specific DB</li>
                        <li>System/User Identity</li>
                        <li>Empty or Hydrated DBs</li>
                    </ul>
                    <a href="/Tests/DB/default.aspx" class="w-100 btn btn-lg btn-primary">Get started</a>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">PDF</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>Browser Integration</li>
                        <li>W/O Http Handlers</li>
                        <li>XDP referencing PDF</li>
                    </ul>
                    <a href="/Tests/PDF/default.aspx" class="w-100 btn btn-lg btn-primary">Get started</a>
                </div>
            </div>
        </div>
        <div class="col">
            <div class="card mb-4 rounded-3 shadow-sm">
                <div class="card-header py-3">
                    <h4 class="my-0 fw-normal">Graph</h4>
                </div>
                <div class="card-body">
                    <ul class="list-unstyled mt-3 mb-4">
                        <li>EntraID Authenticated</li>
                        <li>User Properties</li>
                        <li>Claims</li>
                        <li>EntraID Groups</li>
                    </ul>
                    <a href="/Tests/Graph/default.aspx" class="w-100 btn btn-lg btn-primary">Get started</a>
                </div>
            </div>
        </div>
    </div>
    <br />
    <uc1:authenticationDisplay runat="server" id="authenticationDisplay" />
    <div class="row">
        <div class="col">
            <p class="lead">Would you like to view the server variables?</p>
            <p>Select the following link to view the server variables associated with the requests to the web server.</p>
            <p><a href="ServerVariables.aspx" title="Click to view the server variables.">View all server variables...</a></p>
        </div>
    </div>
</asp:Content>
