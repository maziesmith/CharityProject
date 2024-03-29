﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using BusinessObjects;

namespace CharityProject_rbro752.WebForms
{
    public static class Globals
    {        
        public static Users signedInUser;
        public static Donors signedInDonor;
        public static Charities signedInCharity;
    }

    public partial class SignIn : System.Web.UI.Page
    {
        static Donors SignedInDonor = new Donors();
        protected void Page_Load(object sender, EventArgs e)
        {
            Globals.signedInCharity = null;
            Globals.signedInUser = null;
            Globals.signedInDonor = null;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Users objUserInput = new Users();
            Users objUserOutput = new Users();

            objUserInput.Email = emailSignIn.Value;
            objUserInput.Password = passwordSignIn.Value;

            BLL objBLL = new BLL();
            objUserOutput = objBLL.SignInBLL(objUserInput);

            if (objUserOutput != null)
            {
                Globals.signedInUser = objUserOutput;
                if (objUserOutput.UserType == "Donor")
                {
                    Globals.signedInDonor = objBLL.FindDonor(objUserOutput.Email, objUserOutput.Password);
                    Response.Redirect("RequestPickup.aspx");
                }
                else if (objUserOutput.UserType == "Charity")
                {
                    Globals.signedInCharity = objBLL.FindCharity(objUserOutput.Email, objUserOutput.Password);
                    Response.Redirect("DonationManagement.aspx");
                }
                else if (objUserOutput.UserType == "Admin")
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alertwindow", "alert('User Not Found'); window.location.href = 'SignIn.aspx';", true);
            }

        }
    }
}