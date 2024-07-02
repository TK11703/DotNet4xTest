<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="CurrentUserDetail.aspx.cs" Inherits="DotNet4xTestWeb.Tests.Graph.CurrentUserDetail" %>

<%@ Register Src="~/authenticationDisplay.ascx" TagPrefix="uc1" TagName="authenticationDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <li class="breadcrumb-item"><a href="/default.aspx">Home</a></li>
    <li class="breadcrumb-item"><a href="/Tests/Graph/default.aspx">Graph Testing Options</a></li>
    <li class="breadcrumb-item active" aria-current="page">Current User Details</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">This test performs a GraphClient request to pull data for the currently logged in user. If a user is not yet logged in, then an "Unknown User" will display.</p>
    <div class="row mt-3 mb-3 text-center">
        <div class="col-6 offset-3">
            <div class="card">
                <% if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                %>
                <img src="ImageViewer?fn=images/genericprofile.png" class="card-img-top" alt="profile image">
                <div class="card-body">
                    <h5 class="card-title">Unknown User</h5>
                    <p class="card-text">You have not authenticated yet, so the details from Graph cannot be displayed yet. Please login to view your information.</p>
                </div>
                <%
                    }
                    else if (authenticatedUserData != null)
                    {
                %>
                <img src="data:image/jpeg;base64,<%=Convert.ToBase64String(authenticatedUserData.Avatar) %>" class="card-img-top" alt="profile image" />
                <div class="card-body">
                    <address>
                        <strong><%=authenticatedUserData.DisplayName %></strong>
                        <br />
                        <a href="mailto:<%=authenticatedUserData.Email %>" title="Click to email me."><%=authenticatedUserData.Email %></a>
                    </address>
                    <address>
                        <strong><%=authenticatedUserData.CompanyName %></strong>
                        <br />
                        <%=authenticatedUserData.JobTitle %>
                        <br />
                        <%=authenticatedUserData.Address %>
                        <br />
                        <%=authenticatedUserData.City %>, <%=authenticatedUserData.StateProvince %>
                        <br />
                        <%=authenticatedUserData.CountryRegion %>
                    </address>
                </div>
                <div class="card-footer text-body-secondary">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Refresh User Data" OnClick="RefreshUserDataButton_Click" />
                </div>
                <%
                    }
                %>
            </div>
        </div>
    </div>
    <br />
    <div>
        <asp:Label ID="serverVariableResultsLabel" runat="server" Text="Server Variables:"></asp:Label>
        <asp:Literal ID="serverVariableResults" runat="server"></asp:Literal>
        <a href="ServerVariables.aspx" title="Click to view all server variable information.">View all server variables...</a>
    </div>
    <br />
    <div>
        <a class="btn btn-secondary d-inline-flex align-items-center" href="default.aspx" title="Go back to the available tests.">
            <svg height="16" width="16" fill="currentColor" class="me-1">
                <use href="#arrow-left-circle"></use>
            </svg>
            <span>Back to Tests</span>
        </a>
    </div>
    <uc1:authenticationDisplay runat="server" ID="authenticationDisplay" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
