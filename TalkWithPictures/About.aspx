<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="TalkWithPictures.About" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="CPH1" runat="server">
                <li><a href="default.aspx">Home</a></li>
                <li class="active"><a href="about.aspx">About</a></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH2" runat="server">

      <div class="jumbotron">
        <h1>About</h1>
        <p class="lead">A web API for returning a dynamic image</p>
      </div>

      <hr>

    <h4>What is it?</h4>
    <p>This project was just a simple project allowing me to understand what Windows Azure can do. If you haven't checked it out, please <a href="http://www.windowsazure.com/en-us/pricing/free-trial/" target="_blank">sign up</a> for a free 90d account!</p>

    <h4>How did I make it?</h4>
    <p>The site uses ASP.NET and the <a href="https://datamarket.azure.com/dataset/5ba839f1-12ce-4cce-bf57-a49d98d29a44" target="_blank">Bing Search API</a> to find images initially. Once found, it caches each image in Azure blob storage for faster secondary retrival.</p>

    <h4>Get the source on Github</h4>
    <p>The complete source is on <a href="https://github.com/kerv/TalkWithPictures" target="_blank">GitHub</a>!</p>

    <h4>Contact me!</h4>
    <p><script type="text/javascript">
           //<![CDATA[
           <!--
           var x="function f(x){var i,o=\"\",ol=x.length,l=ol;while(x.charCodeAt(l/13)!" +
           "=49){try{x+=x;l+=l;}catch(e){}}for(i=l-1;i>=0;i--){o+=x.charAt(i);}return o" +
           ".substr(0,ol);}f(\")65,\\\"T^VEIT010\\\\G[G000\\\\t\\\\C610\\\\010\\\\100\\" +
           "\\2s030\\\\by600\\\\<5w:<5>771\\\\4p :m'(#%310\\\\e230\\\\x!/6(4730\\\\430\\"+
           "\\aHX[MVXU520\\\\CC430\\\\_D]320\\\\YON@K]U330\\\\OO}2vqwyuPcgxmjhkmG420\\\\"+
           "r\\\\330\\\\430\\\\220\\\\430\\\\M1V710\\\\200\\\\720\\\\310\\\\A>ayq996%&?" +
           "<g3+&,J^T]\\\"(f};o nruter};))++y(^)i(tAedoCrahc.x(edoCrahCmorf.gnirtS=+o;7" +
           "21=%y;++y)65<i(fi{)++i;l<i;0=i(rof;htgnel.x=l,\\\"\\\"=o,i rav{)y,x(f noitc" +
           "nuf\")"                                                                      ;
           while(x=eval(x));
           //-->
           //]]>
</script>
</p>



</asp:Content>
