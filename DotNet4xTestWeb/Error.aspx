<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DotNet4xTestWeb.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteTitleContent" runat="server">
    <%=this.SiteTitle %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadCrumbContent" runat="server">
    <li class="breadcrumb-item"><a href="/default.aspx">Home</a></li>
    <li class="breadcrumb-item active" aria-current="page">Error Message</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageBodyContent" runat="server">
    <p class="lead">This page has been shown to you, because an error occurred in the typical processing of the last request. Please see the details on this page for more information.</p>
    <div class="row">
        <div class="col">
            <asp:Repeater runat="server" ID="AlertRepeater">
                <ItemTemplate>
                    <div class="alert alert-danger" role="alert">
                        <p class="mb-3"><%# Eval("Message") %></p>
                        <asp:Literal runat="server" Text='<%# Eval("Debug") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <br />
    <div>
        <a class="btn btn-secondary d-inline-flex align-items-center" href="/default.aspx" title="Go back to Home.">
            <svg height="16" width="16" fill="currentColor" class="me-1">
                <use href="#arrow-left-circle"></use>
            </svg>
            <span>Back Home</span>
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
