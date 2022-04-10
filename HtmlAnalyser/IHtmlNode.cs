using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser
{
    /// <summary>
    /// html node interface
    /// </summary>
    public interface IHtmlNode
    {
        /// <summary>
        /// node name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        string ToHtml(string title);
        /// <summary>
        /// parent node
        /// </summary>
        IHtmlNode Parent { get; set; }
        /// <summary>
        /// childs node
        /// </summary>
        List<IHtmlNode> ChildNodes { get; }
    }
}
