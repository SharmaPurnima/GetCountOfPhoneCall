using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCountOfPhoneCallActivity
{
    internal class EntityNameUtility
    {
        
        internal const string CaseEntity = "incident";
        internal const string AccountEntity = "account";
        internal const string PhoneCallEntity = "phonecall";
        
    }
    internal class PhoneActivityUtility
    {
        internal const string CaseLkp = "regardingobjectid";
        internal const string CreatedOn = "createdon";

    }
    internal class CaseEntityUtility
    {
        internal const string AccountLkp = "customerid";
        
    }
    internal class AccountUtility
    {
        internal const string PhoneCount = "new_phonecount";
     }
}
