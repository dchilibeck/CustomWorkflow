using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;

namespace ELOCustomWorkflow
{
    public class VirtualEntityReference : CodeActivity
    {
        [RequiredArgument]
        [Input("Input Email")]
        public InArgument<string> InputEmail { get; set; }

        [ReferenceTarget("test_employee")]
        [Output("Employee Reference")]
        public OutArgument<EntityReference> EmployeeToAssociate { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var userContext = context.GetExtension<IWorkflowContext>();
            ITracingService traceLog = context.GetExtension<ITracingService>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(userContext.UserId);

            var EmailFilter = this.InputEmail.Get<string>(context);

            var QueryEmployee = new QueryExpression("test_employee")
            {
                ColumnSet = new ColumnSet(new string[] { "test_employeeid","test_name","test_email" })
            };

            var EmployeeFilter = new FilterExpression();
            EmployeeFilter.AddCondition("test_email", ConditionOperator.Equal, EmailFilter);
            QueryEmployee.Criteria = EmployeeFilter;

            var Employees = service.RetrieveMultiple(QueryEmployee);

            if (Employees != null && Employees.Entities.Count > 0)
            {
                var theEmployee = Employees[0];

                traceLog.Trace(EmailFilter);
                traceLog.Trace(Employees.Entities.Count.ToString());

                traceLog.Trace(theEmployee.LogicalName);
                traceLog.Trace(theEmployee.Id.ToString());
                traceLog.Trace(theEmployee["test_employeeid"].ToString());
                traceLog.Trace(theEmployee.ToString());
                traceLog.Trace(theEmployee.Attributes.ToString());

                var theId = theEmployee["test_employeeid"];

                this.EmployeeToAssociate.Set(context, new EntityReference(theEmployee.LogicalName, (Guid)theId));
            }

        }
    }
}
