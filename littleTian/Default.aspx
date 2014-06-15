<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="littleTian._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <asp:ListBox ID="ConversationDisplay" runat="server" Height="400px" Width="80%"></asp:ListBox>
    
    <br />
    <br />
    <asp:TextBox ID="ConversationSubmit" runat="server" Height="30" Width="74%"></asp:TextBox>
    
    &nbsp;<asp:Button ID="submitConv" runat="server" Text="发送" Font-Size ="10" Width="5%" Height="30" OnClick="submitConv_Click"/>
    
</asp:Content>
