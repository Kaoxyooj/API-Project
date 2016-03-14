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
    public class OrdersController : Controller
    {
        const string _OrdersPostBinding = "Id,OriginalOrderId, AccountId, Name,TotalAmount";

        public async Task<ActionResult> Index()
        {
            IEnumerable<Order> selectedOrders = Enumerable.Empty<Order>();
            try
            {
                selectedOrders = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Order> orders =
                            await client.QueryAsync<Order>("SELECT Id, AccountId ,TotalAmount From Order");
                        return orders.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Orders";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedOrders);
        }

        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<Order> selectedOrders = Enumerable.Empty<Order>();
            try
            {
                selectedOrders = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Order> orders =
                            await client.QueryAsync<Order>("SELECT Id, AccountId,TotalAmount From Order Where Id = '" + id + "'");
                        return orders.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce Orders Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedOrders.FirstOrDefault());
        }

        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<Order> selectedOrders = Enumerable.Empty<Order>();
            try
            {
                selectedOrders = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Order> orders =
                            await client.QueryAsync<Order>("SELECT Id, AccountId,TotalAmount From Order Where Id= '" + id + "'");
                        return orders.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Orders";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedOrders.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = _OrdersPostBinding)] Order order)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("Order", order.Id, order);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Order";
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
                return View(order);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Order> selectedOrders = Enumerable.Empty<Order>();
            try
            {
                selectedOrders = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Order
                    QueryResult<Order> orders =
                        await client.QueryAsync<Order>(string.Format("SELECT Id, AccountId,TotalAmount From Order Where Id='{0}'", id));
                    return orders.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Orders";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedOrders.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedOrders.FirstOrDefault());
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
                        success = await client.DeleteAsync("Order", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce Orders";
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
        public async Task<ActionResult> Create([Bind(Include = _OrdersPostBinding)] Order order)
        {
            var id = new SuccessResponse();
            try
            {
                id = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                     async (client) =>
                     {
                         return await client.CreateAsync("Order", order);
                     }
                     );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce Order";
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
                return View(order);
            }
        }
    }
}
