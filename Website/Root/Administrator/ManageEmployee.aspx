<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageEmployee.aspx.cs" Inherits="Root_Admin_ManageEmployee" ValidateRequest="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    //parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<section class="content-header">
                <h1>
                    <asp:Label ID="lblAddEdit" runat="server"></asp:Label>
                    <small>Admin Panel</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Settings</a></li>
                    <li class="active">Manage Employees</li>
                </ol>
            </section>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
            <div class="loading-overlay">
                <div class="wrapper">
                    <div class="ajax-loader-outer">
                        Loading...
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <section class="content mydash">
                <table class="table table-bordered table-hover ">
                    <tr>
                        <td colspan="3" class="aleft">
                            <strong class="star">Note : Fields with <span class="red">*</span> are mandatory fields.</strong>
                        </td>
                        <td rowspan="8" style="text-align: left">
                            <div style="overflow: auto; width: 300px; height: 400px; border: 2px solid Black;" class="mtdDIV">
                                <asp:TreeView runat="server" ID="TreeMenu" OnTreeNodePopulate="TreeMenu_TreeNodePopulate"
                                    ShowCheckBoxes="All" OnSelectedNodeChanged="TreeMenu_SelectedNodeChanged" OnTreeNodeCheckChanged="TreeMenu_TreeNodeCheckChanged"
                                    Height="400px" ImageSet="XPFileExplorer" NodeIndent="15" ShowLines="True" AfterClientCheck="CheckChildNodes();"
                                    PopulateNodesFromClient="true" onclick="OnTreeClick(event)">
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                        NodeSpacing="0px" VerticalPadding="2px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px"
                                        BackColor="#B5B5B5" />
                                </asp:TreeView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">
                            <span class="red">*</span> Employee Type
                        </td>
                        <td class="td2">
                            :
                        </td>
                        <td class="td3">
                            <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEmployeeType" runat="server" ControlToValidate="ddlEmployeeType"
                                Display="Dynamic" ErrorMessage="Please Select Employee Type !" SetFocusOnError="True"
                                ValidationGroup="v" InitialValue="0"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span> Employee Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ControlToValidate="txtEmployeeName"
                                Display="Dynamic" ErrorMessage="Please Enter EmployeeName !" SetFocusOnError="True"
                                ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span> Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Email is not valid !"
                                ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="v"><img src="../images/warning.png"/></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Email !"
                                ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Age
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAge" runat="server" MaxLength="2" CssClass="form-control">0</asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtAge_FilteredTextBoxExtender" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txtAge">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span> LoginID
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtLoginID" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLoginID" runat="server" ErrorMessage="Please Enter LoginID !"
                                ControlToValidate="txtLoginID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="red">*</span> Password
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please Enter Password !"
                                ControlToValidate="txtPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="v"><img src="../images/warning.png"/></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Mobile
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtMobile">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            STDCode
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtSTDCode" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtSTDCode_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtSTDCode">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Ladline
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtLadline" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtLadline_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtLadline">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Address
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="50px" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Employee Image
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUploadEmployeeImage" runat="server" CssClass="form-control" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Employee Description
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <CKEditor:CKEditorControl Toolbar="Basic" ID="ckEmployeeDesc" runat="server" BasePath="~/Root/CKEditor/"
                                Height="150px" CssClass="form-control">
                            </CKEditor:CKEditorControl>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="v" OnClick="btnSubmit_Click" class="btn btn-primary" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary" />
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ClientIDMode="Static"
                                ValidationGroup="v" />
                        </td>
                    </tr>
                </table>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
