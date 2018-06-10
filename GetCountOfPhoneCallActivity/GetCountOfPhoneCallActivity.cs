using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;


namespace GetCountOfPhoneCallActivity
{
    public class GetCountOfPhoneCallActivity:IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            
            try
            {
                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = factory.CreateOrganizationService(context.UserId);
                int PhoneCount = 0;

                QueryExpression queryCase = new QueryExpression(EntityNameUtility.CaseEntity);
                queryCase.ColumnSet = new ColumnSet(true);
                queryCase.Criteria.AddCondition(CaseEntityUtility.AccountLkp,ConditionOperator.Equal,context.PrimaryEntityId);
                EntityCollection CaseCollection = service.RetrieveMultiple(queryCase);
                foreach (Entity CaseObj in CaseCollection.Entities)
                {
                    QueryExpression queryPhone = new QueryExpression(EntityNameUtility.PhoneCallEntity);
                    queryPhone.ColumnSet = new ColumnSet(PhoneActivityUtility.CaseLkp);
                    queryPhone.Criteria.AddCondition(PhoneActivityUtility.CaseLkp,ConditionOperator.Equal,CaseObj.Id);
                    queryPhone.Criteria.AddCondition(PhoneActivityUtility.CreatedOn,ConditionOperator.LastXDays,90);
                    EntityCollection PhoneObj = service.RetrieveMultiple(queryPhone);
                    PhoneCount += PhoneObj.Entities.Count;
                }


                Entity ObjAccount = service.Retrieve(context.PrimaryEntityName, context.PrimaryEntityId, new ColumnSet(AccountUtility.PhoneCount));
                ObjAccount[AccountUtility.PhoneCount] = PhoneCount;
                service.Update(ObjAccount);


            }
            catch (Exception ex) { throw new InvalidPluginExecutionException("Exception occured in GetCountOfPhoneCallActivity Plugin" + ex.StackTrace); }

        }
    }
}
