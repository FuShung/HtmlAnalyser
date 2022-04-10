using HtmlAnalyser.Node;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser
{
    /// <summary>
    /// Analysys the html file to tree node
    /// </summary>
    public static class Analyser
    {
        /// <summary>
        /// load html file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static HtmlNode Load(string path)
        {
            return HtmlBuilder(File.ReadAllText(path));
        }
        /// <summary>
        /// append node in child
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public static void Append(this IHtmlNode node, IHtmlNode value)
        {
            if (node.ChildNodes == null) return;
            node.ChildNodes.Add(value);
            value.Parent = node;
        }
        /// <summary>
        /// append text in node child
        /// </summary>
        /// <param name="node"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Append(this IHtmlNode node, string format, params object[] args)
        {
            node.Append(new TextNode(format, args));
        }

        private static HtmlNode HtmlBuilder(string data)
        {
            return HtmlNodeBuilder("", "", ref data);
        }
        private static HtmlNode HtmlNodeBuilder(string name, string attribute, ref string data)
        {
            var node = SingleNodeBuilder(name, attribute);
            int start_index = 0, end_index = 0, sub_name_split;
            string sub_title, sub_name, sub_attribute, now_inner;
            do
            {
                start_index = data.IndexOf("<");
                now_inner = data.Remove(start_index).RemoveEmpty();
                if (!string.IsNullOrEmpty(now_inner))
                {
                    node.Append(now_inner);
                }
                end_index = data.IndexOf(">");
                sub_title = data.Substring(start_index + 1, end_index - start_index - 1);
                sub_name_split = sub_title.IndexOf(" ");
                sub_name = sub_name_split >= 0 ? sub_title.Remove(sub_name_split) : sub_title;
                sub_attribute = sub_name_split >= 0 ? sub_title.Substring(sub_name_split + 1) : "";
                data = data.Substring(end_index + 1);
                IHtmlNode item_node = null;
                switch (sub_name)
                {
                    case "!DOCTYPE":
                        {
                            node.Append(DocTypeBuilder(sub_name, sub_name_split >= 0 ? sub_attribute : ""));
                        }
                        break;
                    case "!--":
                        {
                            var end_check = sub_attribute.Substring(sub_attribute.Length - 2);
                            if(end_check != "--")
                            {
                                end_index = data.IndexOf("-->");
                                sub_attribute += data.Remove(end_index);
                                data = data.Substring(end_index + 1);
                            }
                            else
                            {
                                sub_attribute = sub_attribute.Remove(sub_attribute.Length - 2);
                            }
                            item_node = new DescriptNode() { Value = sub_attribute };
                        }
                        break;
                    case "script":
                        {
                            var check = "</script>";
                            end_index = data.IndexOf(check);
                            now_inner = data.Remove(end_index).RemoveEmpty();
                            item_node = ScriptBuilder(sub_name, sub_attribute, now_inner);
                            data = data.Substring(end_index + check.Length + 1);
                        }
                        break;
                    case "area":
                    case "frame":
                    case "link":
                    case "base":
                    case "hr":
                    case "meta":
                    case "basefont":
                    case "img":
                    case "param":
                    case "br":
                    case "input":
                    case "col":
                    case "isindex":
                        {
                            if(!string.IsNullOrEmpty(sub_attribute) && sub_attribute[sub_attribute.Length - 1] == '/')
                            {
                                sub_attribute = sub_attribute.Remove(sub_attribute.Length - 1);
                            }
                            item_node = UnEndBuilder(sub_name, sub_attribute);
                        }
                        break;
                    default:
                        if (sub_name[0] == '/')
                        {
                            return node;
                        }
                        else if (!string.IsNullOrEmpty(sub_attribute) && sub_attribute[sub_attribute.Length - 1] == '/')
                        {
                            sub_attribute = sub_attribute.Remove(sub_attribute.Length - 1);
                            item_node = SingleNodeBuilder(sub_name, sub_attribute);
                        }
                        else
                        {
                            item_node = HtmlNodeBuilder(sub_name, sub_attribute, ref data);
                        }
                        break;

                }
                if(item_node != null)
                {
                    node.Append(item_node);
                }
            }
            while (!string.IsNullOrEmpty(data));
            return node;
        }

        private static void AppenElement(this HtmlNode node, string attribute)
        {
            if (string.IsNullOrEmpty(attribute)) return;
            
            var attr_index = attribute.IndexOf('=');
            var attr_space = attribute.IndexOf(' ');
            while (attr_index >= 0 || attr_space > 0)
            {
                if (attr_index < 0)
                {
                    var element = new HtmlElement()
                    { 
                        Name = attribute
                    };
                    node.SetElement(element);
                }
                else
                {
                    var attr_name = attribute.Remove(attr_index);
                    attribute = attribute.Substring(attr_index + 1);
                    var check = attribute[0];
                    switch (check)
                    {
                        case '\'':
                        case '\"':
                            {
                                attribute = attribute.Substring(1);
                                attr_index = attribute.IndexOf(check);
                                var attr_value = attribute.Remove(attr_index);
                                var element = new HtmlTextElement()
                                {
                                    Name = attr_name
                                };
                                element.Add(attr_value);
                                node.SetElement(element);
                                var remove = attr_index + 2;
                                attribute = remove >= attribute.Length ? "" : attribute.Substring(remove);
                            }
                            break;
                        default:
                            {
                                var element = new HtmlElement()
                                {
                                    Name = attr_name,
                                    Value = attribute
                                };
                                node.SetElement(element);
                            }
                            break;
                    }
                }
                attr_index = attribute.IndexOf('=');
                attr_space = attribute.IndexOf(' ');
            }
        }

        private static HtmlNode SingleNodeBuilder(string name, string attribute)
        {
            HtmlNode node = null;
            switch (name)
            {
                case "a":
                    node = new HyperLinkNode();
                    break;
                default:
                    node = new HtmlNode()
                    {
                        Name = name
                    };
                    break;
            }
            node.AppenElement(attribute);
            return node;
        }

        private static UnEndNode UnEndBuilder(string name, string attribute)
        {
            var node = new UnEndNode()
            {
                Name = name
            };
            node.AppenElement(attribute);
            return node;
        }

        private static DocTypeNode DocTypeBuilder(string name, string attribute)
        {
            var node = new DocTypeNode() 
            {
                Data = attribute
            };
            return node;
        }

        private static ScriptNode ScriptBuilder(string name, string attribute, string data)
        {
            var node = new ScriptNode();
            node.AppenElement(attribute);
            node.Append(data);
            return node;
        }

        private static string RemoveEmpty(this string data)
        {
            return data.Replace("\r", "").Replace("\t", "").Replace("\n", "").Replace(" ", "");
        }
    }
}
