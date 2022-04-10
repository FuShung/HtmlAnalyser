using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// image node
    /// </summary>
    public class ImageNode : UnEndNode
    {
        const string src_attribute = "src";
        /// <summary>
        /// 
        /// </summary>
        public ImageNode()
        {
            this.Name = "img";
        }
        /// <summary>
        /// 
        /// </summary>
        public override string Name { get => "img"; }
        /// <summary>
        /// 
        /// </summary>
        public string Source
        {
            get
            {
                var element = this.GetElement<HtmlTextElement>(src_attribute);
                return element == null ? "" : element.Value;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.RemoveElement(src_attribute);
                }
                else
                {
                    this.SetElement(new HtmlTextElement()
                    {
                        Name = src_attribute,
                        Value = value
                    });
                }
            }
        }
    }
}
