using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;
namespace PostForum.Modules
{
    public class NodeType
    {
        public const string InnerHtml = "innerhtml";
        public const string InnerText = "innertext";
        public const string OuterHtml = "outerhtml";
    }

    public class FilterNodes
    {
        public static string NodeText(string _nodetype, HtmlNodeCollection _node)
        {
            switch (_nodetype.ToLower())
            {
                case "innerhtml":
                    return _node[0].InnerHtml;
                case "innertext":
                    return _node[0].InnerText;
                case "outerhtml":
                    return _node[0].OuterHtml;
                default:
                    return _node[0].InnerText;
            }
        }

        public static List<string> NodeTexts(string _nodetype, HtmlNodeCollection _node)
        {
            List<string> output = new List<string>();
            for (int i = 0; i < _node.Count; i++)
            {
                switch (_nodetype.ToLower())
                {
                    case "innerhtml":
                        output.Add(_node[i].InnerHtml);
                        break;
                    case "innertext":
                        output.Add(_node[i].InnerText);
                        break;
                    case "outerhtml":
                        output.Add(_node[i].OuterHtml);
                        break;
                    default:
                        output.Add(_node[i].InnerText);
                        break;
                }
            }
            return output;
        }

        public static string XPathBasic(string _Input, string _XPath, string _NodeType)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(_Input);
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes(_XPath);

            if (nodes != null)
                return NodeText(_NodeType, nodes);
            return "";
        }

        public static ErrorType GetDataNode(out string _Output, string _Input, string _XPath, string _NodeTextType, bool _IsRemoveHtml, string _RegPattern, int _MatchIndex, int _GroupIndex, string _RegReplace, string _RegReplaceString)
        {
            string output = "";
            try
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(_Input);
                HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes(_XPath);

                if (nodes == null)
                {
                    _Output = string.Empty;
                    return ErrorType.XPath;
                }

                output = NodeText(_NodeTextType, nodes);

                if (_IsRemoveHtml)
                {
                    output = FilterHtml.Instant().RemoveSpecialCharacter(output);
                }

                try
                {
                    if (!string.IsNullOrEmpty(_RegPattern))
                    {
                        output = Filter.GetTextByRegex(_RegPattern, output, true, _MatchIndex, _GroupIndex);
                    }
                }
                catch (Exception ex)
                {
                    _Output = string.Empty;
                    return ErrorType.Regular;
                }

                try
                {
                    if (!string.IsNullOrEmpty(_RegReplace))
                    {
                        output = Filter.RegularReplace(output, _RegReplace, _RegReplaceString);
                    }
                }
                catch (Exception ex)
                {
                    _Output = string.Empty;
                    return ErrorType.Regular;
                }
            }
            catch (Exception ex)
            {
                _Output = string.Empty;
                return ErrorType.Coding;
            }

            _Output = output;
            return ErrorType.Nothing;
        }

        public static ErrorType GetDataNodes(out List<string> _Output, string _Input, string _XPath, string _NodeTextType, bool _IsRemoveHtml, string _RegPattern, int _MatchIndex, int _GroupIndex, string _RegReplace, string _RegReplaceString)
        {
            List<string> output = new List<string>();
            try
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(_Input);
                HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes(_XPath);

                if (nodes == null)
                {
                    _Output = new List<string>();
                    return ErrorType.XPath;
                }


                output = NodeTexts(_NodeTextType, nodes);

                if (_IsRemoveHtml)
                {
                    for (int i = 0; i < output.Count; i++)
                        output[i] = FilterHtml.Instant().RemoveSpecialCharacter(output[i]);
                }

                try
                {
                    if (!string.IsNullOrEmpty(_RegPattern))
                    {
                        for (int i = 0; i < output.Count; i++)
                            output[i] = Filter.GetTextByRegex(_RegPattern, output[i], true, _MatchIndex, _GroupIndex);
                    }
                }
                catch (Exception ex)
                {
                    _Output = new List<string>();
                    return ErrorType.Regular;
                }

                try
                {
                    if (!string.IsNullOrEmpty(_RegReplace))
                    {
                        for (int i = 0; i < output.Count; i++)
                            output[i] = Filter.RegularReplace(output[i], _RegReplace, _RegReplaceString);
                    }
                }
                catch (Exception ex)
                {
                    _Output = new List<string>();
                    return ErrorType.Regular;
                }
            }
            catch (Exception ex)
            {
                _Output = new List<string>();
                return ErrorType.Coding;
            }

            _Output = output;
            return ErrorType.Nothing;
        }

        public static HtmlNodeCollection GetNodes(string html, string XPath)
        {
            try
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                HtmlNode root = htmlDoc.DocumentNode;
                return root.SelectNodes(XPath);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<string> GetAttributes(string html, string xpath, string attributeName)
        {
            try
            {
                List<string> lst = new List<string>();

                HtmlNodeCollection nodes = GetNodes(html, xpath);
                if (nodes != null)
                {
                    for (int i = 0; i < nodes.Count; i++)
                        lst.Add(nodes[i].Attributes[attributeName].Value);
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
    }
}
