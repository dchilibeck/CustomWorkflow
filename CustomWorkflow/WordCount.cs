using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace CustomWorkflow
{
    public class WordCount : CodeActivity
    {
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("Word Count")]
        public OutArgument<int> CountofWords { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            this.CountofWords.Set(context, this.InputText.Get<string>(context).Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length);
        }
    }
}
