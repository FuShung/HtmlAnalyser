using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAnalyser.Node
{
    /// <summary>
    /// html element when element value is text
    /// </summary>
    public class HtmlTextElement : HtmlElement
    {
        private List<string> datas = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public HtmlTextElement()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Add(string data)
        {
            var temps = data.Split(';');
            foreach(var text in temps)
            {
                var temp = text;
                while(temp.Length > 0 &&temp[0] == ' ')
                {
                    temp = temp.Substring(1);
                }
                if (temp.Length > 0) this.datas.Add(temp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.datas.Count == 0) return this.Name;
            return string.Format("{0}=\"{1}\"", this.Name, string.Join("; ", datas));
        }
    }
}
