using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sitecore.Analytics;
using Sitecore.Analytics.Automation;
using Sitecore.Analytics.Automation.Data;
using Sitecore.Analytics.Automation.Data.Items;
using Sitecore.Analytics.Tracking;
using Sitecore.Commerce.Automation.MarketingAutomation;
using Sitecore.Commerce.Data;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Multishop;
using Sitecore.Data;
using Sitecore.Diagnostics;

namespace SitecoreCoffee.Feature.Commerce.Repositories
{
    public class EaStateCartRepository : Sitecore.Commerce.Data.Carts.ICartRepository, IEntityRepository<Cart>
    {
        private readonly IEntityFactory _entityFactory;

        private readonly IEaPlanProvider _eaPlanProvider;

        private string _lastAcessedUserName;

        public EaStateCartRepository(IEntityFactory entityFactory, IEaPlanProvider eaPlanProvider)
        {
            Assert.ArgumentNotNull(entityFactory, "entityFactory");
            Assert.ArgumentNotNull(eaPlanProvider, "eaPlanProvider");

            _entityFactory = entityFactory;
            _eaPlanProvider = eaPlanProvider;
        }

        public virtual void Create(Cart entity)
        {
            Log.Info("EaStateCartRepository.Create", this);
            ProcessCart(entity, new CartBase[] { entity });
        }

        public virtual void Update(Cart entity)
        {
            Log.Info("EaStateCartRepository.Update", this);
            ProcessCart(entity, new CartBase[] { entity });
        }

        public virtual void Delete(Cart entity)
        {
            Assert.ArgumentNotNull(entity, "entity");

            if (string.IsNullOrWhiteSpace(entity.UserId))
            {
                Log.Error("Failed to process cart - the UserId property must be specified on the Cart entity.", this);
                throw new InvalidOperationException("Failed to process cart.");
            }

            if (string.IsNullOrWhiteSpace(entity.ShopName))
            {
                Log.Error("Failed to process cart - the ShopName property must be specified on the Cart entity.", this);
                throw new InvalidOperationException("Failed to process cart.");
            }

            try
            {
                Log.Info($"EaStateCartRepository.Delete: UserId '{entity.UserId}'", this);
                ProcessCart(entity, Enumerable.Empty<CartBase>());
            }

            catch (InvalidOperationException)
            {
                bool flag = false;
                Guid contactId;

                if (Guid.TryParse(entity.UserId, out contactId))
                {
                    ID item = this._eaPlanProvider.GetEaPlanId(entity.ShopName).Item1;
                    foreach (EngagementPlanStateItem current in Tracker.DefinitionDatabase.Automation().EngagementPlans[item].States)
                    {
                        flag = (flag || AutomationContactManager.RemoveContact(contactId, current.ID));
                        if (flag)
                        {
                            break;
                        }
                    }
                }

                if (!flag)
                {
                    throw;
                }
            }
        }

        public virtual IEnumerable<CartBase> GetAll(string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(shopName, "shopName");
            _lastAcessedUserName = null;
            Type type = _entityFactory.Create("CartBase").GetType();

            return GetAll(shopName, type);
        }

        public virtual IEnumerable<CartBase> GetByUserName(string userName, string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, "userName");
            Assert.ArgumentNotNullOrEmpty(shopName, "shopName");

            _lastAcessedUserName = userName;
            Type type = _entityFactory.Create("CartBase").GetType();

            return GetByUserName(userName, shopName, type);
        }

        public virtual Cart GetByCartId(string cartId, string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(cartId, "cartId");
            Assert.ArgumentNotNullOrEmpty(shopName, "shopName");

            Log.Info($"EaStateCartRepository.GetByCartId: CartId = '{cartId}'", this);

            Type type = _entityFactory.Create("Cart").GetType();
            Cart cart = null;

            if (!string.IsNullOrEmpty(this._lastAcessedUserName))
            {
                cart = (Cart)GetByUserName(this._lastAcessedUserName, shopName, type).SingleOrDefault((CartBase c) => c.ExternalId == cartId);
            }

            //return cart ?? ((Cart)this.GetAll(shopName, type).SingleOrDefault((CartBase cart) => cart.ExternalId == cartId));
            return cart ?? GetCartByIdFallback(cartId, shopName, type);
        }

        private void ProcessCart(Cart entity, IEnumerable<CartBase> cartsToStore)
        {
            Assert.ArgumentNotNull(entity, "entity");
            Assert.ArgumentNotNull(cartsToStore, "cartsToStore");

            Assert.IsNotNull(entity.ShopName, "Property '{0}' must be set.", "ShopName");
            Assert.IsNotNull(entity.UserId, "Property '{0}' must be set.", "UserId");

            if (!CommerceAutomationHelper.EngagementPlansEnabled)
            {
                return;
            }

            ID item = this._eaPlanProvider.GetEaPlanId(entity.ShopName).Item1;
            Contact contact = CommerceAutomationHelper.GetContact(entity.UserId);

            if (contact == null)
            {
                Log.Error($"Failed to process cart - the contact '{entity.UserId}' does not exist.", this);
                throw new InvalidOperationException("Failed to process cart.");
            }

            AutomationStateContext currentStateInPlan = AutomationStateManager.Create(contact).GetCurrentStateInPlan(item);

            if (currentStateInPlan == null)
            {
                Log.Error($"Failed to process cart - the contact '{entity.UserId}' is not in the engagement plan '{item}'.", this);
                throw new InvalidOperationException("Failed to process cart.");
            }

            Log.Info($"currentStateInPlan: planID: {currentStateInPlan.PlanId}, stateID: {currentStateInPlan.StateId}", this);

            IEnumerable<CartBase> customDataOrNull = currentStateInPlan.GetCustomDataOrNull<IEnumerable<CartBase>>("commerce.carts");
            if (customDataOrNull != null)
            {
                cartsToStore = cartsToStore.Concat(from c in customDataOrNull
                                                   where c.ExternalId != entity.ExternalId
                                                   select c);
            }

            Log.Info($"EaStateCartRepository.ProcessCart: Carts to store'", this);

            foreach (var cartToStore in cartsToStore)
            {
                Log.Info($"EaStateCartRepository.ProcessCart: Cart to store: ExternalId = '{cartToStore.ExternalId}'", this);
            }

            currentStateInPlan.SafeUpdateData("commerce.carts", cartsToStore.ToList<CartBase>());
            _lastAcessedUserName = entity.UserId;

            Log.Info($"EaStateCartRepository.ProcessCart: _lastAcessedUserName = '{_lastAcessedUserName}'", this);
        }

        private IEnumerable<CartBase> GetAll(string shopName, Type cartType)
        {
            Assert.ArgumentNotNullOrEmpty(shopName, "shopName");
            if (!CommerceAutomationHelper.EngagementPlansEnabled)
            {
                return Enumerable.Empty<CartBase>();
            }

            ID item = _eaPlanProvider.GetEaPlanId(shopName).Item1;

            EngagementPlanItem engagementPlanItem = new EngagementPlanItem(Tracker.DefinitionDatabase.GetItem(item));
            ContactManager contactManager = CommerceAutomationHelper.GetContactManager();
            List<CartBase> list = new List<CartBase>();

            using (IEnumerator<EngagementPlanStateItem> enumerator = engagementPlanItem.States.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    foreach (ID current in AutomationContactManager.GetStateContactsIdsPaged(enumerator.Current.ID))
                    {
                        Contact contact = contactManager.LoadContactReadOnly(current.Guid);
                        if (contact == null)
                        {
                            if (!(Tracker.Current.Contact.ContactId == current.Guid))
                            {
                                continue;
                            }
                            contact = Tracker.Current.Contact;
                        }
                        IEnumerable<CartBase> customDataOrNull = AutomationStateManager.Create(contact).GetCurrentStateInPlan(item).GetCustomDataOrNull<IEnumerable<CartBase>>("commerce.carts");
                        if (customDataOrNull != null)
                        {
                            list.AddRange(customDataOrNull);
                        }
                    }
                }
            }
            
            Log.Info($"EaStateCartRepository.GetAll: shopName: '{shopName}', carts num: {list.Count}, carts users ids: {String.Join(",", list.Select(x => x.UserId))}" , this);

            return list;
        }

        private IEnumerable<CartBase> GetByUserName(string userName, string shopName, Type cartType)
        {
            ID item = _eaPlanProvider.GetEaPlanId(shopName).Item1;
            List<CartBase> list = new List<CartBase>();

            Log.Info($"EaStateCartRepository.GetByUserName: planId: {item}", this);
            
            IEnumerable<CartBase> customDataOrNull = CommerceAutomationHelper.GetAutomationStateContextReadOnly(userName, item)
                .GetCustomDataOrNull<IEnumerable<CartBase>>("commerce.carts");

            Log.Info($"EaStateCartRepository.GetByUserName: customDataOrNull: {customDataOrNull?.ToList().Count}", this);

            if (customDataOrNull != null)
            {
                list.AddRange(customDataOrNull);
            }

            Log.Info($"EaStateCartRepository.GetByUserName: Getting cart for user name = '{userName}' and shop name = '{shopName}'. Count: {list.Count}", this);

            foreach (var cart in list)
            {
                Log.Info($"EaStateCartRepository.GetByUserName: Cart '{cart.ExternalId}': UserId = '{cart.UserId}'", this);
            }

            return list;
        }


        private Cart GetCartByIdFallback(string cartId, string shopName, Type cartType)
        {
            if (Tracker.Current.Contact == null)
            {
                return null;
            }

            Log.Info($"EaStateCartRepository.GetCartByIdFallback: CartId = '{cartId}'", this);

            string contactId = Tracker.Current.Contact.ContactId.ToString();

            List<CartBase> carts = this.GetByUserName(contactId, shopName, cartType).ToList();
            Cart cart1 = (Cart)null;

            if (carts.Any())
            {
                cart1 = (Cart)carts.SingleOrDefault(cart => cart.ExternalId == cartId);

                if (cart1 != null)
                {
                    return cart1;
                }
            }

            return null;
        }
    }
}