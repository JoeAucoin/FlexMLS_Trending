<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.FlexMLS_Trending.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>


<div class="dnnForm" id="form-settings">


    <fieldset>


<dnn:sectionhead id="sectMainSettings" cssclass="Head" runat="server" text="Module Settings" section="MainSection"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="MainSection" runat="server">
            <div class="dnnFormItem">
    <dnn:label id="lblItemCssClass" runat="server" controlname="txtItemCssClass" suffix=":" />
            <asp:textbox id="txtItemCssClass" runat="server" Text="col-md-offset-1 col-md-1 col-sm-2 col-xs-3" />		
        </div>

            <div class="dnnFormItem">
    <dnn:label id="lblMLSImagesUrl" runat="server" controlname="txtMLSImagesUrl" suffix=":" />
            <asp:textbox id="txtMLSImagesUrl" runat="server" Text="https://gibs.com/Images/" />		
        </div>

        <div class="dnnFormItem">
            <dnn:label id="lblViewListing" runat="server" suffix=":" controlname="ddlViewListing" />
           <asp:dropdownlist id="ddlViewListing" Runat="server"></asp:dropdownlist>
            
        </div>

	     <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblNumberOfRecords" ControlName="txtNumberOfRecords" ResourceKey="lblNumberOfRecords" Suffix=":" />
            <asp:Textbox ID="txtNumberOfRecords" runat="server" />
        </div>	      
        
 	     <div class="dnnFormItem">
            <dnn:label id="lblShowPaging" runat="server" controlname="cbxShowPaging" suffix=":" />
            <asp:CheckBox ID="cbxShowPaging" runat="server" />

        </div>	  
        
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblTemplate" ControlName="txtTemplate" ResourceKey="lblTemplate" Suffix=":" />
            <asp:Textbox ID="txtTemplate" runat="server" TextMode="MultiLine"><div class="text-center">
<a href="[ViewLink]"><b>[Address]</b><br />
[Town], MA [ZipCode]</a>
<br />MLS# [MLS#] - [ListingPrice]
</div></asp:Textbox>
        </div>	       


</div>





    </fieldset>

</div>