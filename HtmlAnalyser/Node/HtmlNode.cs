using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// default html node
    /// </summary>
    public class HtmlNode : IHtmlNode
    {
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, IHtmlElement> elements = new Dictionary<string, IHtmlElement>();
        /// <summary>
        /// 
        /// </summary>
        public HtmlNode()
        {
            this.ChildNodes = new List<IHtmlNode>();
        }

        #region - Property -
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IHtmlNode Parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<IHtmlNode> ChildNodes { get; set; } 
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetElement<T>(string name) where T :IHtmlElement
        {
            if (this.elements.ContainsKey(name))
            {
                var element = this.elements[name];
                return element is T ? (T)element : default(T);
            }
            return default(T);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetSingleNode<T>(string name) where T : IHtmlNode
        {
            var node = this.ChildNodes.Find(x => x.Name == name && x is T);
            return node == null ? default(T) : (T)node;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<T> GetNodes<T>(string name) where T : IHtmlNode
        {
            return (from node in this.ChildNodes where node.Name == name && node is T select (T)node).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void SetElement(IHtmlElement element)
        {
            if (this.elements.ContainsKey(element.Name))
            {
                this.elements[element.Name] = element;
            }
            else
            {
                this.elements.Add(element.Name, element);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void RemoveElement(string name)
        {
            if(this.elements.ContainsKey(name))
            {
                this.elements.Remove(name);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public virtual string ToHtml(string title = "")
        {
            var text = "";
            if(string.IsNullOrEmpty(this.Name))
            {
                if (this.ChildNodes.Count > 0)
                {
                    foreach (var node in this.ChildNodes)
                    {
                        text += node.ToHtml("");
                    }
                }
                return text;
            }
            text = title + "<" + this.Name;
            if(this.elements.Count > 0)
            {
                foreach(var pair in elements)
                {
                    text += " " + pair.Value.ToString();
                }
            }
            if(this.ChildNodes.Count > 0)
            {
                text += ">\r\n";
                var sub_title = title + "\t";
                foreach (var node in this.ChildNodes)
                {
                    text += node.ToHtml(sub_title);
                }
                text += title + "</" + this.Name + ">\r\n";
            }
            else
            {
                text += "/>\r\n";
            }
            return text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToHtml();
        }

    }
}
