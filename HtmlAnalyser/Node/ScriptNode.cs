using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// script node
    /// </summary>
    public class ScriptNode : HtmlNode
    {
        /// <summary>
        /// 
        /// </summary>
        public ScriptNode()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override string Name { get => "script"; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public override string ToHtml(string title)
        {
            var text = title + "<" + this.Name;
            if (this.elements.Count > 0)
            {
                foreach (var pair in elements)
                {
                    text += " " + pair.Value.ToString();
                }
            }
            if (this.ChildNodes.Count > 0)
            {
                text += ">\r\n";
                var sub_title = title + "\t";
                foreach (var node in this.ChildNodes)
                {
                    text += node.ToHtml(title);
                }
            }
            else
            {
                text += "/>\r\n";
            }
            return text;
        }
    }
}
