using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesForceAPI.Models;
using SalesForceAPI.Models.Salesforce1;
using System.Threading.Tasks;
using SalesForceAPI.Salesforce1;
using Salesforce.Common.Models;

namespace SalesForceAPI.Controllers
{
    public class AccountsForceController : Controller
    {
        const string _AccountsPostBinding = "Id, Name, BillingStreet, BillingCity, BillingState, BillingPostalCode, BillingCountry";
        public async Task<ActionResult> Index()
        {
            IEnumerable<Account> selectedAccounts = Enumerable.Empty<Account>();
            try
            {
                selectedAccounts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Account> accounts =
                            await client.QueryAsync<Account>("SELECT Id, Name, BillingStreet, BillingCity, BillingState, BillingPostalCode, BillingCountry From Account");
                        return accounts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Accounts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedAccounts);
        }

        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<Account> selectedAccounts = Enumerable.Empty<Account>();
            try
            {
                selectedAccounts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Account> accounts =
                            await client.QueryAsync<Account>("SELECT Id, Name, BillingStreet, BillingCity, BillingState, BillingPostalCode, BillingCountry From Account Where Id = '" + id + "'");
                        return accounts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce Accounts Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedAccounts.FirstOrDefault());
        }

        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<Account> selectedAccounts = Enumerable.Empty<Account>();
            try
            {
                selectedAccounts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Account> accounts =
                            await client.QueryAsync<Account>("SELECT Id, Name, BillingStreet, BillingCity, BillingState, BillingPostalCode, BillingCountry From Account Where Id= '" + id + "'");
                        return accounts.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Accounts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedAccounts.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = _AccountsPostBinding)] Account account)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("Account", account.Id, account);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Account";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(account);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Account> selectedAccounts = Enumerable.Empty<Account>();
            try
            {
                selectedAccounts = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Account
                    QueryResult<Account> accounts =
                        await client.QueryAsync<Account>(string.Format("SELECT Id, Name, BillingStreet, BillingCity, BillingState, BillingPostalCode, BillingCountry From Account Where Id='{0}'", id));
                    return accounts.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Accounts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedAccounts.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedAccounts.FirstOrDefault());
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            bool success = false;
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.DeleteAsync("Account", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce Accounts";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = _AccountsPostBinding)] Account account)
        {
            var id = new SuccessResponse();
            try
            {
                id = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                     async (client) =>
                     {
                         return await client.CreateAsync("Account", account);
                     }
                     );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce Account";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (this.ViewBag.ErrorMessage == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(account);
            }
        }
    }
}
