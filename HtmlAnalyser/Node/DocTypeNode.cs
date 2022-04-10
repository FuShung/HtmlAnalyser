using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// document type node
    /// </summary>
    public class DocTypeNode : IHtmlNode
    {
        /// <summary>
        /// 
        /// </summary>
        public DocTypeNode()
        {
            this.Data = "";
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get => "DOCTYPE"; }
        /// <summary>
        /// 
        /// </summary>
        public string Data { get; set; }
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
            return string.Format("<!DOCTYPE HTML{0}>\r\n", string.IsNullOrEmpty(this.Data) ? "" : (" " + this.Data));
        }
    }
}
