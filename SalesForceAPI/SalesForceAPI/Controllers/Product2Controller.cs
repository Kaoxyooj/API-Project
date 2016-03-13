using Salesforce.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SalesForceAPI.Models.Salesforce1;
using SalesForceAPI.Salesforce1;
namespace SalesForceAPI.Controllers
{
    public class Product2Controller : Controller
    {
        // Note: the SOQL Field list, and Binding Property list have subtle differences as custom properties may be mapped with the JsonProperty attribute to remove __c
        const string _Product2sPostBinding = "Id,Name,ProductCode,Description,IsActive,CreatedDate,CreatedById,LastModifiedDate,LastModifiedById,SystemModstamp,Family,IsDeleted,LastViewedDate,LastReferencedDate";
        // GET: Product2s
        public async Task<ActionResult> Index()
        {
            IEnumerable<Product2> selectedProduct2s = Enumerable.Empty<Product2>();
            try
            {
                selectedProduct2s = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Product2> product2s =
                            await client.QueryAsync<Product2>("SELECT Id,Name,ProductCode,Description,IsActive,CreatedDate,CreatedById,LastModifiedDate,LastModifiedById,SystemModstamp,Family,IsDeleted,LastViewedDate,LastReferencedDate From Product2");
                        return product2s.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Product2s";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedProduct2s);
        }

        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<Product2> selectedProduct2s = Enumerable.Empty<Product2>();
            try
            {
                selectedProduct2s = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Product2> product2s =
                            await client.QueryAsync<Product2>("SELECT Id,Name,ProductCode,Description,IsActive,CreatedDate,CreatedById,LastModifiedDate,LastModifiedById,SystemModstamp,Family,IsDeleted,LastViewedDate,LastReferencedDate From Product2 Where Id = '" + id + "'");
                        return product2s.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce Product2s Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedProduct2s.FirstOrDefault());
        }

        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<Product2> selectedProduct2s = Enumerable.Empty<Product2>();
            try
            {
                selectedProduct2s = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Product2> product2s =
                            await client.QueryAsync<Product2>("SELECT Id,Name,ProductCode,Description,IsActive,CreatedDate,CreatedById,LastModifiedDate,LastModifiedById,SystemModstamp,Family,IsDeleted,LastViewedDate,LastReferencedDate From Product2 Where Id= '" + id + "'");
                        return product2s.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Product2s";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedProduct2s.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = _Product2sPostBinding)] Product2 product2)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("Product2", product2.Id, product2);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Product2";
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
                return View(product2);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Product2> selectedProduct2s = Enumerable.Empty<Product2>();
            try
            {
                selectedProduct2s = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Product2
                    QueryResult<Product2> product2s =
                        await client.QueryAsync<Product2>(string.Format("SELECT Id,Name,ProductCode,Description,IsActive,CreatedDate,CreatedById,LastModifiedDate,LastModifiedById,SystemModstamp,Family,IsDeleted,LastViewedDate,LastReferencedDate From Product2 Where Id='{0}'", id));
                    return product2s.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Product2s";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedProduct2s.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedProduct2s.FirstOrDefault());
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
                        success = await client.DeleteAsync("Product2", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce Product2s";
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
        public async Task<ActionResult> Create([Bind(Include = _Product2sPostBinding)] Product2 product2)
        {
            var id = new SuccessResponse();
            try
            {
                id = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                     async (client) =>
                     {
                         return await client.CreateAsync("Product2", product2);
                     }
                     );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce Product2";
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
                return View(product2);
            }
        }
    }
}