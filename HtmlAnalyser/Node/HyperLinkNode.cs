using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// hyper link node
    /// </summary>
    public class HyperLinkNode : HtmlNode
    {
        const string link_attribute = "href";
        /// <summary>
        /// 
        /// </summary>
        public HyperLinkNode()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override string Name { get => "a"; }
        /// <summary>
        /// 
        /// </summary>
        public string HyperLink
        {
            get 
            {
                var element = this.GetElement<HtmlTextElement>(link_attribute);
                return element == null ? "" : element.Value;
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    this.RemoveElement(link_attribute);
                }
                else
                {
                    this.SetElement(new HtmlTextElement()
                    {
                        Name = link_attribute,
                        Value = value
                    });
                }
            }
        }
    }
}
