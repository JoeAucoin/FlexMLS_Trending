using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.Modules.FlexMLS_Trending.Components;
using GIBS.Modules.FlexMLS.Components;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Tabs;
using System.Collections;
using DotNetNuke.Services.Localization;

namespace GIBS.Modules.FlexMLS_Trending
{
    public partial class Settings : FlexMLS_TrendingSettings
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {
                    BindModules();
         
                    if (Settings.Contains("Template"))
                    {
                        txtTemplate.Text = Settings["Template"].ToString();
                    }

                    if (Settings.Contains("ShowPaging"))
                    {
                        if (Settings["ShowPaging"].ToString().Length > 0)
                        cbxShowPaging.Checked = Convert.ToBoolean(Settings["ShowPaging"].ToString());
                    }

                    if (Settings.Contains("NumberOfRecords"))
                    {
                        txtNumberOfRecords.Text = Settings["NumberOfRecords"].ToString();
                    }
                                        
                    if (Settings.Contains("FlexMLSPage"))
                    {
                        ddlViewListing.SelectedValue = Settings["FlexMLSPage"].ToString();
                    }

                    if (Settings.Contains("MLSImagesUrl"))
                    {
                        txtMLSImagesUrl.Text = Settings["MLSImagesUrl"].ToString();
                    }

                    if (Settings.Contains("ItemCssClass"))
                    {
                      txtItemCssClass.Text = Settings["ItemCssClass"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {

                Template = txtTemplate.Text.ToString();
                ItemCssClass = txtItemCssClass.Text.ToString();
                MLSImagesUrl = txtMLSImagesUrl.Text.ToString();
                FlexMLSPage = ddlViewListing.SelectedValue.ToString();
                NumberOfRecords = txtNumberOfRecords.Text.ToString();
                ShowPaging = cbxShowPaging.Checked.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        private void BindModules()

        {

            DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
            ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, "GIBS - FlexMLS");

            foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)

            {
                if (!mi.IsDeleted)
                {
                    DotNetNuke.Entities.Tabs.TabController tabController = new DotNetNuke.Entities.Tabs.TabController();
                    DotNetNuke.Entities.Tabs.TabInfo tabInfo = tabController.GetTab(mi.TabID, this.PortalId);

                    string strPath = tabInfo.TabName.ToString();

                    ListItem objListItem = new ListItem();

                    objListItem.Value = mi.TabID.ToString();     // TabID & ModuleID      + "-" + mi.ModuleID.ToString()
                    objListItem.Text = strPath + " -> " + mi.ModuleTitle.ToString();
                                       
                    ddlViewListing.Items.Add(objListItem);
                }
            }

            ddlViewListing.Items.Insert(0, new ListItem(Localization.GetString("SelectModule", this.LocalResourceFile), "-1"));

        }









    }
}