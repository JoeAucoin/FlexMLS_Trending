using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using GIBS.Modules.FlexMLS_Trending.Components;
//using FlexMLS_Trending.Components;
using GIBS.Modules.FlexMLS.Components;
using DotNetNuke.Common;
using System.Net;
using System.IO;

namespace GIBS.Modules.FlexMLS_Trending
{
    public partial class ViewFlexMLS_Trending : PortalModuleBase, IActionable
    {

        static string _FlexMLSPage = "";
        static string _numberOfRecords = "0";
        public static string _maxThumbSize = "80";
        static string _imageAlign = "left";
        static string _MLSImagesURL = "";
        public string _ItemCssClass = "col-md-12 col-sm-6 col-xs-6";
        
        public int CurrentPage;



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Settings.Contains("ItemCssClass"))
                {
                    _ItemCssClass = Settings["ItemCssClass"].ToString();
                }
                
                if (!IsPostBack)
                {
                    hidPage.Value = "0";
                    CurrentPage = 0;
                    LoadSettings();

                    if (_FlexMLSPage.Length > 0)
                    {
                        LoadTrending();
                    }
                    else
                    {
                        lblDebug.Visible = true;
                        lblDebug.Text = "Update Module Settings";
                    }
                    


                    //var mc = new ModuleController();
                    //var mi = mc.GetTabModule(1223);
                    //var tSettings = mi.TabModuleSettings;
                    //var sValue = tSettings["MLSImagesUrl"].ToString();

                    //lblDebug.Text = sValue.ToString();
                }
                else
                {
                    CurrentPage = Int32.Parse(hidPage.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void LoadTrending()
        {

            try
            {
                //   string year = DateTime.Now.Year.ToString();


                List<FlexMLSInfo> items;
                FlexMLSController controller = new FlexMLSController();

                items = controller.FlexMLS_ListingViews_Get(100);

                //creating the PagedDataSource instance....
                PagedDataSource pg = new PagedDataSource();
                pg.DataSource = items;
                pg.AllowPaging = true;
                pg.PageSize = Int32.Parse(_numberOfRecords.ToString());

                pg.CurrentPageIndex = CurrentPage;

                lblCurrentPage.Text = "Page: " + (CurrentPage + 1).ToString() + " of " + pg.PageCount.ToString();

                // Disable Prev or Next buttons if necessary

                lbPrev.Enabled = !pg.IsFirstPage;
                lbNext.Enabled = !pg.IsLastPage;



                //Binding pg to datalist


                //bind the data
                lstContent.DataSource = pg;
                lstContent.DataBind();



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void LoadSettings()
        {

            try
            {

                if (Settings.Contains("ThumbImageAlign"))
                {
                    _imageAlign = Settings["ThumbImageAlign"].ToString();
                }
                if (Settings.Contains("MaxThumbSize"))
                {
                    _maxThumbSize = Settings["MaxThumbSize"].ToString();
                }
                if (Settings.Contains("MLSImagesUrl"))
                {
                    _MLSImagesURL = Settings["MLSImagesUrl"].ToString();
                }

                if (Settings.Contains("FlexMLSPage"))
                {
                    _FlexMLSPage = Settings["FlexMLSPage"].ToString();
                }
                if (Settings.Contains("NumberOfRecords"))
                {
                    _numberOfRecords = Settings["NumberOfRecords"].ToString();
                }

                if (Settings.Contains("ShowPaging"))
                {
                    if (Convert.ToBoolean(Settings["ShowPaging"].ToString()) == false)
                    {
                        Paging_Data.Visible = false;
                    }
                }
                


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void lstContent_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {

            HyperLink Line1 = (HyperLink)e.Item.FindControl("HyperLink1");
            HyperLink Line2 = (HyperLink)e.Item.FindControl("HyperLink2");

            string _content = Server.HtmlDecode(DataBinder.Eval(e.Item.DataItem, "Content").ToString());
            string _ListingNumber = DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString();
            string _listingprice = DataBinder.Eval(e.Item.DataItem, "ListingPrice").ToString();
            _listingprice = String.Format("{0:C0}", double.Parse(_listingprice)); // FORMAT CURRENCY


            string _pageName = _content.ToString().Replace(" ", "_").ToString().Replace("&", "").ToString().Replace(",","").ToString() + ".aspx";
            string vLink = Globals.NavigateURL(Int32.Parse(_FlexMLSPage.ToString()));
            var result = vLink.Substring(vLink.LastIndexOf('/') + 1);
            // DISABLE ADDING OF NEW RECORD IF COMING FROM THIS MODULE BY QUERYSTRING ADDITION OF . . . /t/f
            vLink = vLink.ToString().Replace(result.ToString(), "tabid/" + _FlexMLSPage.ToString() + "/pg/v/t/f/MLS/" + _ListingNumber.ToString() + "/" + _pageName.ToString());
            Line1.NavigateUrl = vLink.ToString();
            Line2.NavigateUrl = vLink.ToString();

            Line1.Text = _content.ToString();
            Line2.Text = "MLS# " + _ListingNumber.ToString() + " - " + _listingprice.ToString();

            Image ListingImage = (Image)e.Item.FindControl("imgListingImage");

            string checkImage = _MLSImagesURL.ToString() + _ListingNumber.ToString() + ".jpg";

            if (UrlExists(checkImage.ToString()) == true)
            {
                // ListingImage.ImageUrl = checkImage.ToString();
                ListingImage.ImageUrl = _MLSImagesURL.ToString() + _ListingNumber.ToString() + ".jpg";

            }
            else if (UrlExists(_MLSImagesURL.ToString() + _ListingNumber.ToString() + "_1.jpg") == true)
            {
                //
                ListingImage.ImageUrl = _MLSImagesURL.ToString() + _ListingNumber.ToString() + "_1.jpg";

            }
            else
            {

                ListingImage.ImageUrl = _MLSImagesURL.ToString() + "NoImage.jpg";

              //  ImageNeeded(_ListingNumber.ToString());
            }



         //   ListingImage.ImageUrl = "~/DesktopModules/GIBS/FlexMLS/ImageHandler.ashx?MlsNumber=" + _ListingNumber.ToString() + "&MaxSize=" + _maxThumbSize.ToString();        //checkImage.ToString();
            
            ListingImage.AlternateText = "MLS Listing " + _ListingNumber.ToString() + " - " + _content.ToString();
            ListingImage.ToolTip = "MLS " + _ListingNumber.ToString() + " - " + _content.ToString();
            


        }


        private static bool UrlExists(string url)
        {
            try
            {
                new System.Net.WebClient().DownloadData(url);
                return true;
            }
            catch (System.Net.WebException e)
            {
                if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }

        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                //actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                //    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                //     true, false);

                return actions;
            }
        }

        #endregion

        protected void lbPrev_Click(object sender, EventArgs e)
        {
            // Set viewstate variable to the previous page
          //  CurrentPage = (Int32.Parse(hidPage.Value.ToString()) - 1);
            CurrentPage -= 1;


            hidPage.Value = CurrentPage.ToString();
            // Reload control
            LoadTrending();
        }

        protected void lbNext_Click(object sender, EventArgs e)
        {
            // Set viewstate variable to the next page
            //CurrentPage = (Int32.Parse(hidPage.Value.ToString()) + 1);
            CurrentPage += 1;

            hidPage.Value = CurrentPage.ToString();
            // Reload control
            LoadTrending();
        }





    }
}