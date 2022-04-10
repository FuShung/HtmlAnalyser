using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser
{

    /// <summary>
    /// html element interface
    /// </summary>
    public interface IHtmlElement
    {
        /// <summary>
        /// element name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// element value
        /// </summary>
        string Value { get; set; }
    }
}
