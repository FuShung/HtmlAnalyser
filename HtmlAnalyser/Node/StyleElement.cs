using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// style element
    /// </summary>
    public class StyleElement : IHtmlElement
    {
        Dictionary<string, string> styles = new Dictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        public string Name { get => "style"; }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void Add(string name, string data)
        {
            if(styles.ContainsKey(name))
            {
                styles[name] = data;
            }
            else
            {
                styles.Add(name, data);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.styles.Count == 0) return "";
            return this.Name + "=\"" + string.Join(" ", (from pair in styles select pair.Key + ": " + pair.Value + ";")) + "\"";
        }
    }
}
