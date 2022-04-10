using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// description node
    /// </summary>
    public class DescriptNode : IHtmlNode
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get => ""; }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IHtmlNode Parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<IHtmlNode> ChildNodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string ToHtml(string title)
        {
            return title + "<!-- " + this.Value + "-->\r\n";
        }
    }
}
