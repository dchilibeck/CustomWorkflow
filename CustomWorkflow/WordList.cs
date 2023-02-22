using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace CustomWorkflow
{
    public class WordList : CodeActivity
    {
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("List of Words")]
        public OutArgument<string> WordListing { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            string[] result;
            string parsedResult;
            parsedResult = "";

            result = this.InputText.Get<string>(context).Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in result)
            {
                parsedResult = parsedResult + ":" + item;
            }

            this.WordListing.Set(context, parsedResult);
        }
    }
}
