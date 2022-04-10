using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// the html node when index only text
    /// </summary>
    public class TextNode : IHtmlNode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public TextNode(string format, params object[] args)
        {
            this.Value = string.Format(format, args);
            this.ChildNodes = null;
        }
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
            return title + this.Value + "\r\n";
        }
    }
}
