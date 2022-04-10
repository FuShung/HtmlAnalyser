using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// the html node when the nod not has end mark
    /// </summary>
    public class UnEndNode : HtmlNode
    {
        /// <summary>
        /// 
        /// </summary>
        public UnEndNode()
        {
            this.ChildNodes = null;
        }
        /// <summary>
        /// 
        /// </summary>
        public override string Name { get; set; }
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
            return text + ">\r\n";
        }
    }
}
