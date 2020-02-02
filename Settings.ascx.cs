using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.Modules.FlexMLS_Trending.Components;
using GIBS.Modules.FlexMLS.Components;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Tabs;

namespace GIBS.Modules.FlexMLS_Trending
{
    public partial class Settings : ModuleSettingsBase
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

                    GetTowns();
                    GetMyTabs();

                 //   FlexMLS_TrendingSettings settingsData = new FlexMLS_TrendingSettings(this.TabModuleId);

                    //settingsData.ThumbImageAlign = rblThumbPlacement.SelectedValue.ToString();

                    if (Settings.Contains("ThumbImageAlign"))
                    {
                        rblThumbPlacement.SelectedValue = Settings["ThumbImageAlign"].ToString();
                    }

                    if (Settings.Contains("MaxThumbSize"))
                    {
                        txtThumbSize.Text = Settings["MaxThumbSize"].ToString();
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

                    if (Settings.Contains("PropertyType"))
                    {
                        ddlPropertyType.SelectedValue = Settings["PropertyType"].ToString();
                    }

                    if (Settings.Contains("Town"))
                    {
                        ddl_Town.SelectedValue = Settings["Town"].ToString();
                    }

                    if (Settings.Contains("Village"))
                    {
                        ddl_Village.SelectedValue = Settings["Village"].ToString();
                    }

                    if (Settings.Contains("Bedrooms"))
                    {
                        ddlBedRooms.SelectedValue = Settings["Bedrooms"].ToString();
                    }

                    if (Settings.Contains("Bathrooms"))
                    {
                        ddlBathRooms.SelectedValue = Settings["Bathrooms"].ToString();
                    }

                    if (Settings.Contains("PriceLow"))
                    {
                        ddlPriceLow.SelectedValue = Settings["PriceLow"].ToString();
                    }

                    if (Settings.Contains("PriceHigh"))
                    {
                        ddlPriceHigh.SelectedValue = Settings["PriceHigh"].ToString();
                    }

                    if (Settings.Contains("WaterFront"))
                    {
                        if (Settings["WaterFront"].ToString().Length > 0)
                        cbxWaterFront.Checked = Convert.ToBoolean(Settings["WaterFront"].ToString());
                    }

                    if (Settings.Contains("Waterview"))
                    {
                        if (Settings["Waterview"].ToString().Length > 0)
                        cbxWaterView.Checked = Convert.ToBoolean(Settings["Waterview"].ToString());
                    }

                    if (Settings.Contains("Complex"))
                    {
                        ddlComplex.SelectedValue = Settings["Complex"].ToString();
                    }

                    if (Settings.Contains("DOM"))
                    {
                        ddlDOM.SelectedValue = Settings["DOM"].ToString();
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
                var modules = new ModuleController();

                modules.UpdateTabModuleSetting(TabModuleId, "MLSImagesUrl", txtMLSImagesUrl.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "ThumbImageAlign", rblThumbPlacement.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "MaxThumbSize", txtThumbSize.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "ShowPaging", cbxShowPaging.Checked.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "NumberOfRecords", txtNumberOfRecords.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "PropertyType", ddlPropertyType.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "Town", ddl_Town.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "Village", ddl_Village.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "Bedrooms", ddlBedRooms.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "Bathrooms", ddlBathRooms.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "PriceLow", ddlPriceLow.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "PriceHigh", ddlPriceHigh.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "WaterFront", cbxWaterFront.Checked.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "Waterview", cbxWaterView.Checked.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "Complex", ddlComplex.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "DOM", ddlDOM.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "FlexMLSPage", ddlViewListing.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "ItemCssClass", txtItemCssClass.Text.ToString());
                //txtMLSImagesUrl


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void GetMyTabs()
        {

            try
            {


                ddlViewListing.DataSource = TabController.GetPortalTabs(this.PortalId, this.TabId, true, false);
                ddlViewListing.DataBind();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetTowns()
        {

            try
            {

                List<FlexMLSInfo> items;

                FlexMLSController controller = new FlexMLSController();
                items = controller.FlexMLS_Lookup_Town();


                ddl_Town.DataSource = items;
                ddl_Town.DataTextField = "Town";
                ddl_Town.DataValueField = "Town";
                ddl_Town.DataBind();

                ddl_Town.Items.Insert(0, new ListItem("--Select--", ""));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ddl_Town_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<FlexMLSInfo> items;
            FlexMLSController controller = new FlexMLSController();

            items = controller.FlexMLS_Lookup_Village(ddl_Town.SelectedValue.ToString());

            ddl_Village.DataSource = items;
            ddl_Village.DataTextField = "Village";
            ddl_Village.DataValueField = "Village";
            ddl_Village.DataBind();

            ddl_Village.Items.Insert(0, new ListItem("--Optionally Select--", ""));


        }

    }
}