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
    public class OrderItemsController : Controller
    {
        const string _OrderItemsPostBinding = "Id, OrderId,Description, Quantity, UnitPrice, OriginalOrderItemId, OrderItemNumber, LastModifiedDate";

        public async Task<ActionResult> Index()
        {
            IEnumerable<OrderItem> selectedOrderItems = Enumerable.Empty<OrderItem>();
            try
            {
                selectedOrderItems = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<OrderItem> orderItems =
                            await client.QueryAsync<OrderItem>("SELECT Id, OrderId,Description, Quantity, UnitPrice, OriginalOrderItemId, OrderItemNumber, LastModifiedDate From OrderItem");

                        return orderItems.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce OrderItems";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedOrderItems);
        }

        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<OrderItem> selectedOrderItems = Enumerable.Empty<OrderItem>();
            try
            {
                selectedOrderItems = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<OrderItem> orderItems =
                            await client.QueryAsync<OrderItem>("SELECT Id, OrderId,Description, Quantity, UnitPrice, OriginalOrderItemId, OrderItemNumber, LastModifiedDate From OrderItem Where Id = '" + id + "'");
                        return orderItems.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce OrderItems Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedOrderItems.FirstOrDefault());
        }

        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<OrderItem> selectedOrderItems = Enumerable.Empty<OrderItem>();
            try
            {
                selectedOrderItems = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<OrderItem> orderItems =
                            await client.QueryAsync<OrderItem>("SELECT Id, OrderId,Description, Quantity, UnitPrice, OriginalOrderItemId, OrderItemNumber, LastModifiedDate From OrderItem Where Id= '" + id + "'");
                        return orderItems.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce OrderItems";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedOrderItems.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = _OrderItemsPostBinding)] OrderItem orderItem)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("OrderItem", orderItem.Id, orderItem);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce OrderItem";
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
                return View(orderItem);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<OrderItem> selectedOrderItems = Enumerable.Empty<OrderItem>();
            try
            {
                selectedOrderItems = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this OrderItem
                    QueryResult<OrderItem> orderItems =
                        await client.QueryAsync<OrderItem>(string.Format("SELECT Id, OrderId,Description, Quantity, UnitPrice, OriginalOrderItemId, OrderItemNumber, LastModifiedDate From OrderItem Where Id='{0}'", id));
                    return orderItems.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce OrderItems";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedOrderItems.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedOrderItems.FirstOrDefault());
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
                        success = await client.DeleteAsync("OrderItem", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce OrderItems";
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
        public async Task<ActionResult> Create([Bind(Include = _OrderItemsPostBinding)] OrderItem orderItem)
        {

            var id = new SuccessResponse();
            try
            {
                id = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                     async (client) =>
                     {
                         return await client.CreateAsync("OrderItem", orderItem);
                     }
                     );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce OrderItem";
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
                return View(orderItem);
            }
        }
    }
}
