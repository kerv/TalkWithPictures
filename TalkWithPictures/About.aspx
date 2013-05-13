<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="TalkWithPictures.About" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="CPH1" runat="server">
                <li><a href="default.aspx">Home</a></li>
                <li class="active"><a href="about.aspx">About</a></li>
                <li><a href="#">Contact</a></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH2" runat="server">

      <div class="jumbotron">
        <h1>About</h1>
        <p class="lead">A web API for returning a dyanmic image</p>
      </div>

      <hr>

      <div class="row-fluid marketing">
        <div class="span6">
          <h4>Subheading</h4>
          <p>Donec id elit non mi porta gravida at eget metus. Maecenas faucibus mollis interdum.</p>

          <h4>Subheading</h4>
          <p>Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Cras mattis consectetur purus sit amet fermentum.</p>

          <h4>Subheading</h4>
          <p>Maecenas sed diam eget risus varius blandit sit amet non magna.</p>
        </div>

        <div class="span6">
          <h4>Subheading</h4>
          <p>Donec id elit non mi porta gravida at eget metus. Maecenas faucibus mollis interdum.</p>

          <h4>Subheading</h4>
          <p>Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Cras mattis consectetur purus sit amet fermentum.</p>

          <h4>Subheading</h4>
          <p>Maecenas sed diam eget risus varius blandit sit amet non magna.</p>
        </div>
      </div>

</asp:Content>
