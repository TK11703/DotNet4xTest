<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="authenticationDisplay.ascx.cs" Inherits="DotNet4xTestWeb.authenticationDisplay" %>
<div class="row mb-4 mt-4">
    <div class="col">
        <div class="d-flex">
            <div class="p-2 flex-grow-1 lead">Authentication Results</div>
            <div class="p-2">
                <% if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                %>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Logout" OnClick="LogoutButton_Click" />
                <%
                }
                else
                {
                %>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Login" OnClick="LoginButton_Click" />
                <%
                    }
                %>
            </div>
        </div>
        <ul class="list-group list-group-horizontal mb-1">
            <li class="list-group-item col-4">Is Authenticated</li>
            <li class="list-group-item col-8"><%= HttpContext.Current.User.Identity.IsAuthenticated %></li>
        </ul>
        <ul class="list-group list-group-horizontal mb-1">
            <li class="list-group-item col-4">Authentication Type</li>
            <li class="list-group-item col-8"><%= HttpContext.Current.User.Identity.AuthenticationType %></li>
        </ul>
        <ul class="list-group list-group-horizontal mb-1">
            <li class="list-group-item col-4">Name</li>
            <li class="list-group-item col-8"><%= HttpContext.Current.User.Identity.Name %></li>
        </ul>
    </div>
</div>