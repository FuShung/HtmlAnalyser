using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// default html element
    /// </summary>
    public class HtmlElement : IHtmlElement
    {
        /// <summary>
        /// 
        /// </summary>
        public HtmlElement()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(this.Value) ? this.Name : string.Format("{0}={1}", this.Name, this.Value);
        }
    }
}
